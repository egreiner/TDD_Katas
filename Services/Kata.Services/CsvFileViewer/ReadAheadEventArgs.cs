namespace Kata.Services.CsvFileViewer
{
    using System;

    public class ReadAheadEventArgs: EventArgs
    {
        public ReadAheadEventArgs(int page, int priority)
        {
            this.Page = page;
            this.Priority = priority;
        }
        
        public int Page { get; }
        public int Priority { get; }
    }
}