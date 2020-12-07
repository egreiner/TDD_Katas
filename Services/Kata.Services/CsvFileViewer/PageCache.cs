namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PageCache
    {
        


        public ConcurrentDictionary<PageInfo, IList<string>> Cache { get; } =
            new ConcurrentDictionary<PageInfo, IList<string>>();
        

        public async Task<bool> IsCached(int pageNo) =>
            await Task.Run(() =>
            {
                var page = GetNewPageInfo(pageNo);
                return this.Cache.ContainsKey(page);
            }).ConfigureAwait(false);

        public async Task<IList<string>> GetPageCacheAsync(int pageNo) =>
            await Task.Run(() =>
            {
                var page = GetNewPageInfo(pageNo);
                ////var fetched = this.cache.TryGetValue(page, out var result);
                _ = this.Cache.TryGetValue(page, out var result);
                return result;
            }).ConfigureAwait(false);

        public async Task<bool> SetPageCacheAsync(int pageNo, IList<string> lines) =>
            await Task.Run(() =>
            {
                var page = GetNewPageInfo(pageNo);
                return this.Cache.TryAdd(page, lines);
            }).ConfigureAwait(false);


        private static PageInfo GetNewPageInfo(int pageNo) =>
            new PageInfo(pageNo);
    }
}