namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Extensions;

    public class CachedCsvFileService
    {
        private readonly PageCacheSettings cacheSettings;
        private readonly PaginationService paginationService;
        private readonly string fileName;
        private string cachedTitle;


        public CachedCsvFileService(string fileName, PageCacheSettings cacheSettings, PaginationService paginationService)
        {
            this.paginationService = paginationService;
            this.cacheSettings     = cacheSettings;
            this.fileName          = fileName;
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
            // read from cache if available

            return await this.GetPageFromFileAsync(pageNo);
        }

        private async Task<IList<string>> GetPageFromFileAsync(int pageNo)
        {
            var min = this.paginationService.PageRange.Min.LimitToMin(1);
            var max = this.paginationService.PageRange.Max.LimitToMin(1);
            var page = pageNo.LimitTo(min, max) - 1;

            var length = this.cacheSettings.PageLength;
            var start  = page * length + 1;

            var title = await this.GetTitleAsync();
            var records = await Task.Run(() =>
                File.ReadLines(this.fileName).Skip(start).Take(length).ToList()
                ).ConfigureAwait(false);

            records.Insert(0, title);

            return records;
        }


        public async Task<IEnumerable<string>> ReadFileAsync(int start, int length) =>
            await Task.Run(() =>
                File.ReadLines(this.fileName).Skip(start).Take(length)
            ).ConfigureAwait(false);


        public async Task<int> GetFileLengthAsync() =>
            await Task.Run(() => 
                File.ReadLines(this.fileName).Count()
            ).ConfigureAwait(false);
    }
}