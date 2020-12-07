namespace Kata.Services.CsvFileViewer
{
    using System;
    using Extensions;
    using Logger;

    public class ReadAheadService
    {
        private readonly PageCacheSettings cacheSettings;
        

        public ReadAheadService(PageCacheSettings cacheSettings) =>
            this.cacheSettings = cacheSettings;


        public event EventHandler<EnqueuePageEventArgs> EnqueuePage;

        public (int Min, int Max) PageRange { get; set; } = (1, 1000);



        public void LastPages()
        {
            Log.Add("Read Ahead Last Pages");
            var max = this.PageRange.Max;
            var min = max - this.cacheSettings.ReadAheadPrevPages + 1;
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
            var min = pageNo + 1.LimitToMin(this.PageRange.Min);
            var max = min + this.cacheSettings.ReadAheadNextPages.LimitToMax(this.PageRange.Max);
            return (min, max);
        }

        private (int min, int max) GetRangeForPrevPages(int pageNo)
        {
            var max = (pageNo - 1).LimitToMax(this.PageRange.Max);
            var min = (max - this.cacheSettings.ReadAheadPrevPages).LimitToMin(this.PageRange.Min);
            return (min, max);
        }


        private void EnqueueNextPages(int min, int max, int priority)
        {
            Log.Add($"Read Ahead next pages from {min} to {max}");
            for (var i = min; i <= max; i++)
                this.Raise_EnqueuePage(new EnqueuePageEventArgs(i, priority++));
        }


        private void EnqueuePrevPages(int max, int min, int priority)
        {
            Log.Add($"Read Ahead previous pages from {max} to {min}");
            for (var i = max; i >= min; i--) 
                this.Raise_EnqueuePage(new EnqueuePageEventArgs(i, priority++));
        }


        private void Raise_EnqueuePage(EnqueuePageEventArgs e) =>
            this.EnqueuePage?.Invoke(this, e);
    }
}