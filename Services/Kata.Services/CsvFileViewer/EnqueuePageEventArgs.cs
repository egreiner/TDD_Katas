namespace Kata.Services.CsvFileViewer
{
    using System;

    public class EnqueuePageEventArgs: EventArgs
    {
        public EnqueuePageEventArgs(int page, int priority)
        {
            this.Page = page;
            this.Priority = priority;
        }
        
        public int Page { get; }
        public int Priority { get; }
    }
}