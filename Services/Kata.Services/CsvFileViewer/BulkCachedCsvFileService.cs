﻿namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Cache;
    using Extensions;
    using Logger;
    using PriorityQueue;


    public class BulkCachedCsvFileService
    {
        private readonly PriorityQueue<int> pageQueue = new PriorityQueue<int>();
        private readonly PaginationService paginationService;
        ////private readonly ReadAheadService readAhead;
        private readonly string fileName;
        
        private string cachedTitle;
        private bool dequeuingPoolIsRunning;
        private int lastPage;


        public BulkCachedCsvFileService(string fileName, 
            PageCacheSettings cacheSettings, 
            PaginationService paginationService)
        {
            this.Cache = new Cache<(int min, int max), IList<string>>();

            this.paginationService = paginationService;
            this.CacheSettings     = cacheSettings;
            this.fileName          = fileName;
            ////this.readAhead         = new ReadAheadService(paginationService, this.CacheSettings.ReadAheadPages);
            ////this.readAhead.EnqueuePage += this.OnReadAheadEnqueuePage;
            this.SetEstimatedFileLength();
        }

        // i think we will get DI after this...
        public Cache<(int min, int max), IList<string>> Cache { get; }
        public PageCacheSettings CacheSettings { get; }
        public string ReadLocation { get; private set; }




        private bool CacheContains(int pageNo) =>
            this.Cache.Items.Any(x =>
                pageNo.IsBetween(x.Key.Key.min, x.Key.Key.max));

        private IList<string> CacheGetPage(int pageNo)
        {
            var (start, end, offset, length) = this.CacheSettings.GetBulkBlockInfo(pageNo);

            var records = this.Cache.Items.FirstOrDefault(x =>
                    pageNo.IsBetween(x.Key.Key.min, x.Key.Key.max)).Value
                .Skip(offset).Take(this.CacheSettings.PageLength).ToList();

            var title = this.GetTitleAsync().Result;
            records.Insert(0, title);

            return records;
        }


        public async Task<IList<string>> GetPageAsync(int pageNo)
        {
            IList<string> lines;

            if (this.CacheContains(pageNo))
            {
                Log.Add($"Get cached page {pageNo}");
                this.ReadLocation = "from cache";
                lines = this.CacheGetPage(pageNo);
            }
            else
            {
                this.ReadLocation = "from file";

                this.AddPageToQueue(pageNo, 1);

                while (!this.CacheContains(pageNo)) 
                    await Task.Delay(50);

                lines = this.CacheGetPage(pageNo);
            }

            this.ReadSurroundingPages(pageNo, pageNo > this.lastPage);
            this.lastPage = pageNo;

            return lines;
        }

        private void ReadSurroundingPages(int pageNo, bool favorNext)
        {
            var bulkPages = this.CacheSettings.BulkReadPages;
            this.AddPageToQueue(pageNo + bulkPages, favorNext ? 10 : 20);
            this.AddPageToQueue(pageNo - bulkPages, favorNext ? 20 : 10);
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

            this.AddPageToQueue(this.paginationService.PageRange.Max-1, 10);
            ////this.readAhead.LastPages();
            ////this.readAhead.AllPages();

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

                    ////Console.Write($"\r{page} was read from file     ");
                }
                this.dequeuingPoolIsRunning = false;
            });
        }

        private (int start, int length) GetReadRange(int pageNo)
        {
            var bulk = this.CacheSettings.GetBulkBlockInfo(pageNo);

            var page   = bulk.start - 1;
            var length = this.CacheSettings.PageLength;
            var start  = page * length + 1;

            return (start, bulk.length);
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

            return await this.ReadFileAsync(start, length).ConfigureAwait(false);
        }

        private async Task<IList<string>> ReadFileAsync(int start, int length) =>
            await Task.Run(() =>
                File.ReadLines(this.fileName).Skip(start).Take(length).ToList()
            ).ConfigureAwait(false);


        private async Task<bool> ReadAheadAsync(int pageNo)
        {
            if (this.CacheContains(pageNo))
            {
                Log.Add($"ReadAheadAsync page {pageNo} was cached before");
                return false;
            }

            var bulk = this.CacheSettings.GetBulkBlockInfo(pageNo);

            Log.Add($"ReadAheadAsync page {pageNo}");
            var lines = await this.GetPageFromFileAsync(pageNo).ConfigureAwait(false);
            await this.Cache.SetAsync((bulk.start, bulk.end), lines);

            return true;
        }
    }
}