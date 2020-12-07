namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Cache;
    using Extensions;
    using Logger;
    using PriorityQueue;


    // TODO this component gets to big, where can we split?
    public class CachedCsvFileService
    {
        private readonly PriorityQueue<PageInfo> pageQueue = new PriorityQueue<PageInfo>();
        private readonly PaginationService paginationService;
        private readonly string fileName;
        
        private string cachedTitle;
        private bool dequeuingPoolIsRunning;


        public CachedCsvFileService(string fileName, 
            PageCacheSettings cacheSettings, 
            PaginationService paginationService)
        {
            this.Cache = new Cache<int, IList<string>>();

            this.paginationService = paginationService;
            this.CacheSettings     = cacheSettings;
            this.fileName          = fileName;

            this.InitializeEstimatedFileLength();

            ////Task.Run(this.InitializeMaxPage);
        }

        // i think we will get DI after this...
        public Cache<int, IList<string>> Cache { get; }
        public PageCacheSettings CacheSettings { get; }
        public string ReadLocation { get; private set; }


        public async Task<IList<string>> GetPageAsync(int pageNo)
        {
            IList<string> lines;

            if (await this.Cache.IsCached(pageNo))
            {
                Log.Add($"Get cached page {pageNo}");
                this.ReadLocation = "from cache";
                lines = await this.Cache.GetPageCacheAsync(pageNo);
            }
            else
            {
                this.ReadLocation = "from file";

                this.AddPageToQueue(pageNo, 1);

                while (!await this.Cache.IsCached(pageNo)) 
                    await Task.Delay(50);

                lines = await this.Cache.GetPageCacheAsync(pageNo);
            }

            return lines;
        }

        public async Task ReadAheadSurroundingPagesAsync(int pageNo)
        {
            var t1 = this.ReadAheadNextPagesAsync(pageNo);
            var t2 = this.ReadAheadPrevPagesAsync(pageNo);
            await t1.ConfigureAwait(false);
            await t2.ConfigureAwait(false);
        }

        public async Task<string> GetTitleAsync()
        {
            if (!this.cachedTitle.IsNullOrEmpty())
            {
                Log.Add("return cached title");
                return this.cachedTitle;
            }

            Log.Add("read title from file");

            var titleLine = await this.ReadFileAsync(0, 1);
            this.cachedTitle = titleLine.FirstOrDefault();

            return this.cachedTitle;
        }

        private void AddPageToQueue(int pageNo, int priority) =>
            Task.Run(() =>
            {
                var page = GetNewPageInfo(pageNo);
                Log.Add($"Add page {page} with priority {priority} to pool");

                this.pageQueue.Enqueue(page, priority);
                this.ProcessQueue();
            });

        private void ProcessQueue()
        {
            Task.Run(() =>
            {
                if (this.dequeuingPoolIsRunning) return;

                this.dequeuingPoolIsRunning = true;
                while (this.pageQueue.HasItems)
                {
                    if (!this.pageQueue.TryDequeue(out var page)) break;

                    Log.Add($"Dequeue page {page} from pool for reading from file");
                    _ = this.ReadAheadAsync(page.PageNo).Result;
                }
                this.dequeuingPoolIsRunning = false;
            });
        }

        private static PageInfo GetNewPageInfo(in int pageNo) =>
            new PageInfo(pageNo);


        private async Task<IList<string>> GetPageFromFileAsync(int pageNo)
        {
            Log.Add($"read page {pageNo} from file");
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
            var length = this.CacheSettings.PageLength;
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
            // add job to pool with prio 2
            Log.Add("ReadAheadFirstPagesAsync");
            var start = 2;
            var end = start + this.CacheSettings.ReadAheadNextPages -1;
            return await this.ReadAheadNextPagesAsync(start, end);
        }

        public async Task<bool> ReadAheadLastPagesAsync()
        {
            // add job to pool with prio 2
            Log.Add("ReadAheadLastPagesAsync");
            var max = this.paginationService.PageRange.Max;
            var min = max - this.CacheSettings.ReadAheadPrevPages + 1;
            return await this.ReadAheadPrevPagesAsync(max, min);
        }

        public async Task<bool> ReadAheadNextPagesAsync(int pageNo)
        {
            var pageRange = this.paginationService.PageRange;
            var min = pageNo + 1;
            var max = min + this.CacheSettings.ReadAheadNextPages;

            if (!min.IsBetween(pageRange) && !max.IsBetween(pageRange))
                return false;

            Log.Add("ReadAheadNextPagesAsync");
            return await this.ReadAheadNextPagesAsync(min, max);
        }

        public async Task<bool> ReadAheadPrevPagesAsync(int pageNo)
        {
            var pageRange = this.paginationService.PageRange;
            var max = pageNo - 1;
            var min = max - this.CacheSettings.ReadAheadPrevPages;

            if (!min.IsBetween(pageRange) && !max.IsBetween(pageRange))
                return false;

            Log.Add("ReadAheadPrevPagesAsync");
            return await this.ReadAheadPrevPagesAsync(max, min);
        }

        private async Task<bool> ReadAheadNextPagesAsync(int min, int max)
        {
            Log.Add($"ReadAheadNextPagesAsync {min}-{max}");
            var priority = 2;
            for (var i = min; i <= max; i++)
                this.AddPageToQueue(i, priority++);

            return true;
        }

        private async Task<bool> ReadAheadPrevPagesAsync(int max, int min)
        {
            Log.Add($"ReadAheadPrevPagesAsync {max}-{min}");
            var priority = 2;
            for (var i = max; i >= min; i--)
                this.AddPageToQueue(i, priority++);

            return true;
        }


        private async Task<bool> ReadAheadAsync(int pageNo)
        {
            if (await this.Cache.IsCached(pageNo))
            {
                Log.Add($"ReadAheadAsync page {pageNo} was cached before");
                return false;
            }

            Log.Add($"ReadAheadAsync page {pageNo}");
            var lines = await this.GetPageFromFileAsync(pageNo).ConfigureAwait(false);
            await this.Cache.SetPageCacheAsync(pageNo, lines);
            
            return true;
        }


        public async Task<int> GetFileLengthAsync() =>
            await Task.Run(() => 
                File.ReadLines(this.fileName).Count()
            ).ConfigureAwait(false);



        private void InitializeEstimatedFileLength()
        {
            var fileInfo = new System.IO.FileInfo(this.fileName);
            var length = fileInfo.Length / 500;
            Log.Add($"Calculate estimated file length to {length}");
            this.paginationService.InitializePageRange(length, this.CacheSettings.PageLength);
        }

        public async Task<bool> InitializeMaxPage()
        {
            var length = await this.GetFileLengthAsync().ConfigureAwait(false);
            this.paginationService.InitializePageRange(length, this.CacheSettings.PageLength);

            _ = this.ReadAheadPrevPagesAsync(this.paginationService.PageRange.Max);

            Log.Add($"Initialized MaxPage to {this.paginationService.PageRange.Max}");
            return true;
        }

        private async Task<IList<string>> ReadFileAsync(int start, int length) =>
            await Task.Run(() =>
                File.ReadLines(this.fileName).Skip(start).Take(length).ToList()
            ).ConfigureAwait(false);
    }
}