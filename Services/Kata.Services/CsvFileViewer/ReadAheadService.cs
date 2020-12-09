namespace Kata.Services.CsvFileViewer
{
    using System;
    using Logger;
    using PriorityQueue;

    public class ReadAheadService
    {
        private readonly PaginationService paginationService;
        private readonly int readAheadPages;
        

        public ReadAheadService(PaginationService paginationService, int readAheadPages)
        {
            this.paginationService = paginationService;
            this.readAheadPages    = readAheadPages;
        }


        public event EventHandler<EnqueueItemEventArgs<int>> EnqueuePage;

        private (int Min, int Max) PageRange => this.paginationService.PageRange;


        public void LastPages()
        {
            Log.Add("Read Ahead Last Pages");
            var max = this.PageRange.Max;
            var min = max - this.readAheadPages + 1;
            this.EnqueuePrevPages(max, min, 2);
        }

        public void AllPages()
        {
            Log.Add("Read Ahead All Pages");
            var min = this.PageRange.Min;
            var max = this.PageRange.Max;
            this.EnqueueNextPages(min, max, 100);
        }

        public void SurroundingPages(int pageNo, bool preferNextPages)
        {
            Log.Add($"Read Ahead surrounding pages for page {pageNo}");
            var (min, max) = this.GetRangeForNextPages(pageNo);
            this.EnqueueNextPages(min, max, preferNextPages ? 2: 4);

            (min, max) = this.GetRangeForPrevPages(pageNo);
            this.EnqueuePrevPages(max, min, preferNextPages ? 4: 2);
        }


        private (int min, int max) GetRangeForNextPages(int pageNo)
        {
            var min = pageNo + 1;
            var max = min + this.readAheadPages;
            return this.paginationService.GetLimitedPageRange(min, max);
        }

        private (int min, int max) GetRangeForPrevPages(int pageNo)
        {
            var max = pageNo - 1;
            var min = max - this.readAheadPages;
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