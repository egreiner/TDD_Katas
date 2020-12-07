namespace Kata.Services.Logger
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Log
    {
        private static readonly object lockObject = new object();

        private static readonly List<LogInfo> logMessages = 
            new List<LogInfo>();

        // async ???
        public static void Add(string text)
        {
            lock (lockObject) 
                logMessages.Add(new LogInfo(text));
        }


        public static IList<LogInfo> OrderedLogInfos
        {
            get
            {
                lock (lockObject)
                    return logMessages.OrderBy(x => x.Created).ToList();
            }
        }
    }
}