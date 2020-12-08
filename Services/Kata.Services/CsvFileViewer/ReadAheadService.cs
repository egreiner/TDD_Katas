namespace Kata.Services.CsvFileViewer
{
    using System;
    using Logger;
    using PriorityQueue;

    public class ReadAheadService
    {
        private readonly PaginationService paginationService;
        private readonly PageCacheSettings cacheSettings;
        

        public ReadAheadService(PaginationService paginationService, PageCacheSettings cacheSettings)
        {
            this.paginationService = paginationService;
            this.cacheSettings     = cacheSettings;
        }


        public event EventHandler<EnqueueItemEventArgs<int>> EnqueuePage;

        private (int Min, int Max) PageRange => this.paginationService.PageRange;
        

        public void LastPages()
        {
            Log.Add("Read Ahead Last Pages");
            var max = this.PageRange.Max;
            var min = max - this.cacheSettings.ReadAheadPages + 1;
            this.EnqueuePrevPages(max, min, 2);
        }
        
        public void SurroundingPages(int pageNo)
        {
            Log.Add($"Read Ahead surrounding pages for page {pageNo}");
            var (min, max) = this.GetRangeForNextPages(pageNo);
            this.EnqueueNextPages(min, max, 2);

            (min, max) = this.GetRangeForPrevPages(pageNo);
            this.EnqueuePrevPages(max, min, 2);
        }


        private (int min, int max) GetRangeForNextPages(int pageNo)
        {
            var min = pageNo + 1;
            var max = min + this.cacheSettings.ReadAheadPages;
            return this.paginationService.GetLimitedPageRange(min, max);
        }

        private (int min, int max) GetRangeForPrevPages(int pageNo)
        {
            var max = pageNo - 1;
            var min = max - this.cacheSettings.ReadAheadPages;
            return this.paginationService.GetLimitedPageRange(min, max);
        }


        private void EnqueueNextPages(int min, int max, int priority)
        {
            Log.Add($"Read Ahead next pages from {min} to {max}");
            for (var i = min; i <= max; i++)
                this.Raise_EnqueuePage(new EnqueueItemEventArgs<int>(i, priority++));
        }


        private void EnqueuePrevPages(int max, int min, int priority)
        {
            Log.Add($"Read Ahead previous pages from {max} to {min}");
            for (var i = max; i >= min; i--) 
                this.Raise_EnqueuePage(new EnqueueItemEventArgs<int>(i, priority++));
        }


        private void Raise_EnqueuePage(EnqueueItemEventArgs<int> e) =>
            this.EnqueuePage?.Invoke(this, e);
    }
}