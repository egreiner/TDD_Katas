namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Extensions;

    // TODO this component gets to big, where can we split?
    public class CachedCsvFileService
    {
        private readonly PageCacheSettings cacheSettings;
        private readonly PaginationService paginationService;
        private readonly string fileName;
        
        private string cachedTitle;


        public CachedCsvFileService(string fileName, 
            PageCacheSettings cacheSettings, 
            PaginationService paginationService)
        {
            this.PageCache = new PageCache(this.Log);

            this.paginationService = paginationService;
            this.cacheSettings     = cacheSettings;
            this.fileName          = fileName;

            ////Task.Run(this.InitializeMaxPage);
        }

        // i think we will get DI after this...
        public ILog Log            { get; } = new Log();
        public PageCache PageCache { get; }
        public string ReadLocation { get; private set; }


        public async Task<string> GetTitleAsync()
        {
            if (!this.cachedTitle.IsNullOrEmpty())
            {
                this.Log.Add("return cached title");
                return this.cachedTitle;
            }

            this.Log.Add("read title from file");

            var titleLine    = await this.ReadFileAsync(0, 1);
            this.cachedTitle = titleLine.FirstOrDefault();

            return this.cachedTitle;
        }


        public async Task<IList<string>> GetPageAsync(int pageNo)
        {
            IList<string> lines;

            if (await this.PageCache.IsCached(pageNo))
            {
                this.Log.Add($"Get cached page {pageNo}.");
                this.ReadLocation = "from cache";
                lines = await this.PageCache.GetPageCacheAsync(pageNo);
            }
            else
            {
                lines = await this.GetPageFromFileAsync(pageNo);
                this.ReadLocation = "from file";
                await this.PageCache.SetPageCacheAsync(pageNo, lines);
            }

            _ = this.ReadAheadNextPagesAsync(pageNo);
            _ = this.ReadAheadPrevPagesAsync(pageNo);

            return lines;
        }

        private async Task<IList<string>> GetPageFromFileAsync(int pageNo)
        {
            this.Log.Add($"read page {pageNo} from file");
            var (start, length) = this.GetReadRange(pageNo);

            var recordsTask = this.ReadFileAsync(start, length);
            var titleTask   = this.GetTitleAsync();

            var records = await recordsTask.ConfigureAwait(false);
            var title   = await titleTask.ConfigureAwait(false);
            records.Insert(0, title);

            return records;
        }

        private (int start, int length) GetReadRange(int pageNo)
        {
            var page   = this.GetLimitedPageNo(pageNo) - 1;
            var length = this.cacheSettings.PageLength;
            var start  = page * length + 1;

            return (start, length);
        }

        private int GetLimitedPageNo(int pageNo)
        {
            var min = this.paginationService.PageRange.Min.LimitToMin(1);

            // TODO at the start there is no knowledge about maxPage...
            var max = this.paginationService.PageRange.Max.LimitToMin(10);

            return pageNo.LimitTo(min, max);
        }

        public async Task<bool> ReadAheadFirstPagesAsync()
        {
            this.Log.Add("ReadAheadFirstPagesAsync");
            var start = 2;
            var end = start + this.cacheSettings.ReadAheadNextPages -1;
            return await this.ReadAheadNextPagesAsync(start, end);
        }

        public async Task<bool> ReadAheadLastPagesAsync()
        {
            this.Log.Add("ReadAheadLastPagesAsync");
            var max = this.paginationService.PageRange.Max;
            var min = max - this.cacheSettings.ReadAheadPrevPages + 1;
            return await this.ReadAheadPrevPagesAsync(max, min);
        }

        public async Task<bool> ReadAheadNextPagesAsync(int pageNo)
        {
            this.Log.Add("ReadAheadNextPagesAsync");
            var min = pageNo + 1;
            var max = min + this.cacheSettings.ReadAheadNextPages;
            return await this.ReadAheadNextPagesAsync(min, max);
        }

        public async Task<bool> ReadAheadPrevPagesAsync(int pageNo)
        {
            this.Log.Add("ReadAheadPrevPagesAsync");
            var max = pageNo - 1;
            var min = max - this.cacheSettings.ReadAheadPrevPages;
            return await this.ReadAheadPrevPagesAsync(max, min);
        }

        private async Task<bool> ReadAheadNextPagesAsync(int min, int max)
        {
            this.Log.Add($"ReadAheadNextPagesAsync {min}-{max}");
            for (var i = min; i <= max; i++)
                await this.ReadAheadAsync(this.GetLimitedPageNo(i));

            return true;
        }

        private async Task<bool> ReadAheadPrevPagesAsync(int max, int min)
        {
            this.Log.Add($"ReadAheadPrevPagesAsync {max}-{min}");
            for (var i = max; i >= min; i--)
                await this.ReadAheadAsync(this.GetLimitedPageNo(i));

            return true;
        }


        private async Task<bool> ReadAheadAsync(int pageNo)
        {
            if (await this.PageCache.IsCached(pageNo))
            {
                this.Log.Add($"ReadAheadAsync page {pageNo} was cached before");
                return false;
            }

            this.Log.Add($"ReadAheadAsync page {pageNo}.");
            var page = await this.GetPageFromFileAsync(pageNo).ConfigureAwait(false);
            await this.PageCache.SetPageCacheAsync(pageNo, page);
            
            return true;
        }


        public async Task<int> GetFileLengthAsync() =>
            await Task.Run(() => 
                File.ReadLines(this.fileName).Count()
            ).ConfigureAwait(false);


        public async Task<bool> InitializeMaxPage()
        {
            var length = await this.GetFileLengthAsync().ConfigureAwait(false);
            this.paginationService.InitializePageRange(length, this.cacheSettings.PageLength);

            _ = this.ReadAheadPrevPagesAsync(this.paginationService.PageRange.Max);

            this.Log.Add($"Initialized MaxPage to {length}.");
            return true;
        }

        private async Task<IList<string>> ReadFileAsync(int start, int length) =>
            await Task.Run(() =>
                File.ReadLines(this.fileName).Skip(start).Take(length).ToList()
            ).ConfigureAwait(false);
    }
}