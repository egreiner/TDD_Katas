namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class Log : ILog
    {
        private readonly ConcurrentBag<LogInfo> logMessages = 
            new ConcurrentBag<LogInfo>();

        // async ???
        public void Add(string text) =>
            this.logMessages.Add(new LogInfo(text));


        public IList<LogInfo> LogInfos =>
            this.logMessages.OrderBy(x => x.Inserted).ToList();
    }
}