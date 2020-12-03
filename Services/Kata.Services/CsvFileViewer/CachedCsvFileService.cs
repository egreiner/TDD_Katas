namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Extensions;

    public class CachedCsvFileService
    {
        private readonly PageCache pageCache = new PageCache();
        private readonly PageCacheSettings cacheSettings;
        private readonly PaginationService paginationService;
        private readonly string fileName;
        
        private string cachedTitle;


        public CachedCsvFileService(string fileName, 
            PageCacheSettings cacheSettings, 
            PaginationService paginationService)
        {
            this.paginationService = paginationService;
            this.cacheSettings     = cacheSettings;
            this.fileName          = fileName;

            Task.Run(this.InitializeMaxPage);
        }


        public async Task<string> GetTitleAsync()
        {
            if (!string.IsNullOrEmpty(this.cachedTitle))
                return this.cachedTitle;

            var titleLine = await this.ReadFileAsync(0, 1);
            this.cachedTitle = titleLine.FirstOrDefault();

            return this.cachedTitle;
        }


        public async Task<IList<string>> GetPageAsync(int pageNo)
        {
            IList<string> lines;

            if (await this.pageCache.IsCached(pageNo))
            {
                lines = await this.pageCache.GetPageCacheAsync(pageNo);
            }
            else
            {
                lines = await this.GetPageFromFileAsync(pageNo);
                await this.pageCache.SetPageCacheAsync(pageNo, lines);
            }

            // Initialize reading ahead...

            return lines;
        }

        private async Task<IList<string>> GetPageFromFileAsync(int pageNo)
        {
            var min = this.paginationService.PageRange.Min.LimitToMin(1);

            // at the start there is no knowledge about maxPage...
            var max = this.paginationService.PageRange.Max.LimitToMin(2);
            var page = pageNo.LimitTo(min, max) - 1;

            var length = this.cacheSettings.PageLength;
            var start = page * length + 1;

            var records = await this.ReadFileAsync(start, length)
                .ConfigureAwait(false);

            var title = await this.GetTitleAsync().ConfigureAwait(false);
            records.Insert(0, title);

            return records;
        }

        // Is this TDD?
        // not really
        // this is more a design session
        private async Task<bool> ReadAheadFirstPagesAsync()
        {
            var max = this.cacheSettings.ReadAheadNextPages;
            return await this.ReadAheadPagesAsync(2, max);
        }

        private async Task<bool> ReadAheadLastPagesAsync()
        {
            var max = this.paginationService.PageRange.Max;
            var min = max - this.cacheSettings.ReadAheadPrevPages;
            return await this.ReadAheadPagesAsync(min, max);
        }

        private async Task<bool> ReadAheadPagesAsync(int min, int max)
        {
            for (int i = min; i <= max; i++)
                await this.ReadAheadAsync(i);

            return true;
        }


        private async Task<bool> ReadAheadAsync(int pageNo)
        {
            if (await this.pageCache.IsCached(pageNo)) return false;

            var page = await this.GetPageFromFileAsync(pageNo).ConfigureAwait(false);
            await this.pageCache.SetPageCacheAsync(pageNo, page);
            
            return true;
        }


        public async Task<int> GetFileLengthAsync() =>
            await Task.Run(() => 
                File.ReadLines(this.fileName).Count()
            ).ConfigureAwait(false);


        private async Task InitializeMaxPage()
        {
            var length = await this.GetFileLengthAsync().ConfigureAwait(false);
            this.paginationService.InitializePageRange(length, this.cacheSettings.PageLength);
        }

        private async Task<IList<string>> ReadFileAsync(int start, int length) =>
            await Task.Run(() =>
                File.ReadLines(this.fileName).Skip(start).Take(length).ToList()
            ).ConfigureAwait(false);
    }
}