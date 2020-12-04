namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Concurrent;

    public interface ILog
    {
        ConcurrentBag<LogInfo> LogInfos { get; }
        void Add(string text);
    }
}