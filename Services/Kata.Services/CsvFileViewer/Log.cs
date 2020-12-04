namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Concurrent;

    public class Log : ILog
    {
        public ConcurrentBag<LogInfo> LogInfos { get; } = new ConcurrentBag<LogInfo>();


        // async ???
        public void Add(string text) =>
            this.LogInfos.Add(new LogInfo(text));
    }
}