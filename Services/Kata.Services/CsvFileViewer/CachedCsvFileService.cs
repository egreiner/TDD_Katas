namespace Kata.Services.CsvFileViewer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Cache;
    using Extensions;
    using Logger;
    using PriorityQueue;


    public class CachedCsvFileService
    {
        private readonly PriorityQueue<int> pageQueue = new PriorityQueue<int>();
        private readonly PaginationService paginationService;
        private readonly ReadAheadService readAhead;
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
            this.readAhead         = new ReadAheadService(paginationService, this.CacheSettings.ReadAheadPages);
            this.readAhead.EnqueuePage += this.OnReadAheadEnqueuePage;
            this.SetEstimatedFileLength();
        }

        // i think we will get DI after this...
        public Cache<int, IList<string>> Cache { get; }
        public PageCacheSettings CacheSettings { get; }
        public string ReadLocation { get; private set; }


        public async Task<IList<string>> GetPageAsync(int pageNo)
        {
            IList<string> lines;

            if (await this.Cache.ContainsAsync(pageNo))
            {
                Log.Add($"Get cached page {pageNo}");
                this.ReadLocation = "from cache";
                lines = await this.Cache.GetAsync(pageNo);
            }
            else
            {
                this.ReadLocation = "from file";

                this.AddPageToQueue(pageNo, 1);

                while (!await this.Cache.ContainsAsync(pageNo)) 
                    await Task.Delay(50);

                lines = await this.Cache.GetAsync(pageNo);
            }

            this.readAhead.SurroundingPages(pageNo);

            return lines;
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

        public async Task<bool> SetRealFileLength()
        {
            var lines = await this.GetFileLengthAsync().ConfigureAwait(false);
            this.paginationService.SetRealPageRange(lines, this.CacheSettings.PageLength);
            Log.Add($"Initialized MaxPage to {this.paginationService.PageRange.Max}");

            this.readAhead.LastPages();
            this.readAhead.AllPages();

            return true;
        }

        public async Task<int> GetFileLengthAsync() =>
            await Task.Run(() =>
                File.ReadLines(this.fileName).Count()
            ).ConfigureAwait(false);


        private void AddPageToQueue(int pageNo, int priority) =>
            Task.Run(() =>
            {
                Log.Add($"Add page {pageNo} with priority {priority} to pool");

                this.pageQueue.Enqueue(pageNo, priority);
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
                    _ = this.ReadAheadAsync(page).Result;

                    ////ClearCurrentConsoleLine();
                    Console.Write($"\r{page} was read from file     ");

                }
                this.dequeuingPoolIsRunning = false;
            });
        }

        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private (int start, int length) GetReadRange(int pageNo)
        {
            var page   = this.paginationService.GetLimitedPageNo(pageNo) - 1;
            var length = this.CacheSettings.PageLength;
            var start  = page * length + 1;

            return (start, length);
        }

        private void SetEstimatedFileLength()
        {
            var fileInfo = new FileInfo(this.fileName);
            var lines = fileInfo.Length / 250;
            this.paginationService.SetPageRangeEstimated(lines, this.CacheSettings.PageLength);
            Log.Add($"Initialized MaxPage to estimated {this.paginationService.PageRange.Max}");
        }

        private async Task<IList<string>> GetPageFromFileAsync(int pageNo)
        {
            Log.Add($"read page {pageNo} from file");
            var (start, length) = this.GetReadRange(pageNo);

            var recordsTask = this.ReadFileAsync(start, length);
            var titleTask = this.GetTitleAsync();

            var records = await recordsTask.ConfigureAwait(false);
            var title = await titleTask.ConfigureAwait(false);
            records.Insert(0, title);

            return records;
        }

        private async Task<IList<string>> ReadFileAsync(int start, int length) =>
            await Task.Run(() =>
                File.ReadLines(this.fileName).Skip(start).Take(length).ToList()
            ).ConfigureAwait(false);


        private async Task<bool> ReadAheadAsync(int pageNo)
        {
            if (await this.Cache.ContainsAsync(pageNo))
            {
                Log.Add($"ReadAheadAsync page {pageNo} was cached before");
                return false;
            }

            Log.Add($"ReadAheadAsync page {pageNo}");
            var lines = await this.GetPageFromFileAsync(pageNo).ConfigureAwait(false);
            await this.Cache.SetAsync(pageNo, lines);

            return true;
        }

        private void OnReadAheadEnqueuePage(object sender, EnqueueItemEventArgs<int> e) =>
            this.AddPageToQueue(e.Item, e.Priority);
    }
}