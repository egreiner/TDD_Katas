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

        public async Task<bool> ReadAheadFirstPagesAsync()
        {
            // add job to pool with priority 2
            Log.Add("ReadAheadFirstPagesAsync");
            var start = 2;
            var end = start + this.cacheSettings.ReadAheadNextPages - 1;
            return await this.ReadAheadNextPagesAsync(start, end);
        }

        public async Task<bool> ReadAheadLastPagesAsync()
        {
            // add job to pool with priority 2
            Log.Add("ReadAheadLastPagesAsync");
            var max = this.pageRange.Max;
            var min = max - this.cacheSettings.ReadAheadPrevPages + 1;
            return await this.ReadAheadPrevPagesAsync(max, min);
        }




        public async Task ReadAheadSurroundingPagesAsync(int pageNo)
        {
            var t1 = this.ReadAheadNextPagesAsync(pageNo);
            var t2 = this.ReadAheadPrevPagesAsync(pageNo);
            await t1.ConfigureAwait(false);
            await t2.ConfigureAwait(false);
        }



        private async Task<bool> ReadAheadNextPagesAsync(int pageNo)
        {
            var pageRange = this.pageRange;
            var min = pageNo + 1;
            var max = min + this.cacheSettings.ReadAheadNextPages;

            if (!min.IsBetween(pageRange) && !max.IsBetween(pageRange))
                return false;

            Log.Add("ReadAheadNextPagesAsync");
            return await this.ReadAheadNextPagesAsync(min, max);
        }

        private async Task<bool> ReadAheadNextPagesAsync(int min, int max)
        {
            Log.Add($"ReadAheadNextPagesAsync {min}-{max}");
            var priority = 2;
            for (var i = min; i <= max; i++)
            {
                this.RaiseEvent_ReadAheadDemanded(new ReadAheadEventArgs(i, priority++));
                ////this.AddPageToQueue(i, priority++);
            }

            await Task.Delay(0);
            return true;
        }





        private async Task<bool> ReadAheadPrevPagesAsync(int pageNo)
        {
            var pageRange = this.pageRange;
            var max = pageNo - 1;
            var min = max - this.cacheSettings.ReadAheadPrevPages;

            if (!min.IsBetween(pageRange) && !max.IsBetween(pageRange))
                return false;

            Log.Add("ReadAheadPrevPagesAsync");
            return await this.ReadAheadPrevPagesAsync(max, min);
        }

        private async Task<bool> ReadAheadPrevPagesAsync(int max, int min)
        {
            Log.Add($"ReadAheadPrevPagesAsync {max}-{min}");
            var priority = 2;
            for (var i = max; i >= min; i--)
            {
                this.RaiseEvent_ReadAheadDemanded(new ReadAheadEventArgs(i, priority++));
                ////this.AddPageToQueue(i, priority++);
            }

            await Task.Delay(0);
            return true;
        }

        private void RaiseEvent_ReadAheadDemanded(ReadAheadEventArgs e) =>
            this.ReadAheadDemanded?.Invoke(this, e);
    }
}