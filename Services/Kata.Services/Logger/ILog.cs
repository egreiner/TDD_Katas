namespace Kata.Services.Logger
{
    using System.Collections.Generic;

    public interface ILog
    {
        IList<LogInfo> OrderedLogInfos { get; }
        
        void Add(string text);
    }
}