namespace Kata.Services.CsvFileViewer
{
    using System;
    using System.Threading.Tasks;
    using Extensions;
    using Logger;
    using PriorityQueue;


    public class ReadAhead
    {
        private readonly PriorityQueue<int> queue;
        private readonly PageCacheSettings cacheSettings;

        // TODO
        private readonly (int Min, int Max) pageRange = (1, 666_667);

        public event EventHandler<ReadAheadEventArgs> ReadAheadDemanded;


        public ReadAhead(PageCacheSettings cacheSettings, PriorityQueue<int> queue)
        {
            this.cacheSettings = cacheSettings;
            this.queue = queue;
        }


        public async Task ReadAheadFirstPagesAsync()
        {
            Log.Add("Read Ahead First Pages");
            var min = 2;
            var max = min + this.cacheSettings.ReadAheadNextPages - 1;
            this.ReadAheadNextPages(min, max, 2);
            await Task.Delay(0).ConfigureAwait(false);
        }

        public async Task ReadAheadLastPagesAsync()
        {
            Log.Add("Read Ahead Last Pages");
            var max = this.pageRange.Max;
            var min = max - this.cacheSettings.ReadAheadPrevPages + 1;
            this.ReadAheadPrevPages(max, min, 2);
            await Task.Delay(0).ConfigureAwait(false);
        }



        public async Task ReadAheadSurroundingPagesAsync(int pageNo)
        {
            Log.Add($"Read Ahead surrounding pages for page {pageNo}");
            var (min, max) = this.GetRangeForNextPages(pageNo);
            this.ReadAheadNextPages(min, max, 2);

            (min, max) = this.GetRangeForPrevPages(pageNo);
            this.ReadAheadPrevPages(max, min, 2);
            await Task.Delay(0).ConfigureAwait(false);
        }


        private (int min, int max) GetRangeForNextPages(int pageNo)
        {
            var min = pageNo + 1.LimitToMin(this.pageRange.Min);
            var max = min + this.cacheSettings.ReadAheadNextPages.LimitToMax(this.pageRange.Max);
            return (min, max);
        }

        private (int min, int max) GetRangeForPrevPages(int pageNo)
        {
            var max = (pageNo - 1).LimitToMax(this.pageRange.Max);
            var min = (max - this.cacheSettings.ReadAheadPrevPages).LimitToMin(this.pageRange.Min);
            return (min, max);
        }


        private void ReadAheadNextPages(int min, int max, int priority)
        {
            Log.Add($"Read Ahead next pages from {min} to {max}");
            for (var i = min; i <= max; i++)
                this.RaiseEvent_ReadAheadDemanded(new ReadAheadEventArgs(i, priority++));
        }


        private void ReadAheadPrevPages(int max, int min, int priority)
        {
            Log.Add($"Read Ahead previous pages from {max} to {min}");
            for (var i = max; i >= min; i--) 
                this.RaiseEvent_ReadAheadDemanded(new ReadAheadEventArgs(i, priority++));
        }


        private void RaiseEvent_ReadAheadDemanded(ReadAheadEventArgs e) =>
            this.ReadAheadDemanded?.Invoke(this, e);
    }
}