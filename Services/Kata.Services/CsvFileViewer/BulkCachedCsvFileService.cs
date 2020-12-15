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


    public class BulkCachedCsvFileService
    {
        private readonly PriorityQueue<int> pageQueue = new PriorityQueue<int>();
        private readonly PaginationService pagination;
        
        private string cachedTitle;
        private bool dequeuingPoolIsRunning;
        private int lastPage;


        public BulkCachedCsvFileService(CsvFileViewerSettings settings, PaginationService pagination)
        {
            this.Cache = new LFUKeyWeightedCache<int, IList<string>>(settings.MaxCachedPages);

            this.pagination = pagination;
            this.Settings   = settings;
            this.SetEstimatedFileLength();
        }


        public LFUKeyWeightedCache<int , IList<string>> Cache { get; }
        public CsvFileViewerSettings Settings { get; }

        public string ReadLocation { get; private set; }


        public async Task<IList<string>> GetPageAsync(int pageNo)
        {
            IList<string> lines;

            var bulk = BulkInfo.Create(pageNo, this.Settings);

            if (this.Cache.Contains(bulk.BulkId))
            {
                Log.Add($"Get cached page {pageNo}");
                this.ReadLocation = "from cache";
                lines = this.GetPageFromCache(pageNo);
            }
            else
            {
                this.ReadLocation = "from file";

                this.AddPageToQueue(pageNo, 1);

                while (!this.Cache.Contains(bulk.BulkId)) 
                    await Task.Delay(50);

                lines = this.GetPageFromCache(pageNo);
            }

            this.AddSurroundingPagesToQueue(pageNo, pageNo > this.lastPage);
            this.lastPage = pageNo;

            return lines;
        }


        #region queue

        private void AddSurroundingPagesToQueue(int pageNo, bool favorNext)
        {
            var bulkPages = this.Settings.BulkReadPages;
            this.AddPageToQueue(pageNo + bulkPages, favorNext ? 10 : 20);
            this.AddPageToQueue(pageNo - bulkPages, favorNext ? 20 : 10);
        }

        private void AddPagesToCache(int startPage, int endPage, int priority)
        {
            for (int i = startPage; i < endPage; i += this.Settings.BulkReadPages)
                this.AddPageToQueue(i, priority);
        }

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

                    page = page.LimitToMin(1);
                    Log.Add($"Dequeue page {page} from pool for reading from file");
                    _ = this.GetPageFromFileAsync(page).Result;
                }
                this.dequeuingPoolIsRunning = false;
            });
        }

        #endregion queue


        #region file

        public async Task<int> GetFileLengthAsync() =>
            await Task.Run(() =>
                File.ReadLines(this.Settings.FileName).Count()
            ).ConfigureAwait(false);

        public async Task<bool> SetRealFileLength()
        {
            var lines = await this.GetFileLengthAsync().ConfigureAwait(false);
            this.pagination.SetRealPageRange(lines - 1, this.Settings.RecordsPerPage);

            var maxPage = this.pagination.PageRange.Max;
            Log.Add($"Initialized MaxPage to {maxPage}");

            this.AddPageToQueue(maxPage, 10);

            this.AddPagesToCache(1, maxPage, 100);

            return true;
        }


        // merge this with ReadAheadAsync
        ////private async Task<IList<string>> GetPageFromFileAsync(int pageNo)
        ////{
        ////    Log.Add($"read page {pageNo} from file");
        ////    var bulk = BulkInfo.Create(pageNo, this.Settings);

        ////    return await this.GetPageFromFileAsync(bulk.FileStartLine, bulk.LinesPerBulk).ConfigureAwait(false);
        ////}

        // merge this with GetPageFromFileAsync
        private async Task<IList<string>> GetPageFromFileAsync(int pageNo)
        {
            var bulk = BulkInfo.Create(pageNo, this.Settings);

            if (this.Cache.Contains(bulk.BulkId))
            {
                Log.Add($"ReadAheadAsync page {pageNo} was cached before");
                return null;
            }

            Log.Add($"ReadAheadAsync page {pageNo}");
            var lines = await this.GetPageFromFileAsync(bulk.FileStartLine, bulk.LinesPerBulk).ConfigureAwait(false);
            this.Cache.Set(bulk.BulkId, lines);

            return lines;
        }

        private async Task<IList<string>> GetPageFromFileAsync(int start, int length) =>
            await Task.Run(() =>
                File.ReadLines(this.Settings.FileName).Skip(start).Take(length).ToList()
            ).ConfigureAwait(false);

        private async Task<string> GetTitleAsync()
        {
            if (!this.cachedTitle.IsNullOrEmpty())
                return this.cachedTitle;

            Log.Add("read title from file");

            var titleLine = await this.GetPageFromFileAsync(0, 1);
            return this.cachedTitle = titleLine.FirstOrDefault();
        }

        private void SetEstimatedFileLength()
        {
            var fileInfo = new FileInfo(this.Settings.FileName);
            var lines = fileInfo.Length / 250;
            this.pagination.SetPageRangeEstimated(lines, this.Settings.RecordsPerPage);
            Log.Add($"Initialized MaxPage to estimated {this.pagination.PageRange.Max}");
        }

        #endregion file


        #region cache

        public int GetCachedPages() =>
            this.Cache.Items
                .Select(x => x.Value.CachedItem)
                .Sum(y => y.Count / this.Settings.RecordsPerPage);


        private IList<string> GetPageFromCache(int pageNo)
        {
            var bulk = BulkInfo.Create(pageNo, this.Settings);

            var records = this.Cache.Get(bulk.BulkId)
                .Skip(bulk.OffsetStart)
                .Take(this.Settings.RecordsPerPage)
                .ToList();

            var title = this.GetTitleAsync().Result;
            records.Insert(0, title);

            return records;
        }

        #endregion cache
    }
}