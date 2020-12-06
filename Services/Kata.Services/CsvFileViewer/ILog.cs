namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Generic;

    public interface ILog
    {
        IList<LogInfo> LogInfos { get; }
        void Add(string text);
    }
}