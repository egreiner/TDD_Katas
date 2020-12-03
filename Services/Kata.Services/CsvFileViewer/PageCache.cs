namespace Kata.Services.CsvFileViewer
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PageCache
    {
        private readonly ConcurrentDictionary<PageInfo, IList<string>> cache =
            new ConcurrentDictionary<PageInfo, IList<string>>();


        public async Task<bool> IsCached(int pageNo) =>
            await Task.Run(() =>
            {
                var page = GetNewPageInfo(pageNo);
                return this.cache.ContainsKey(page);
            }).ConfigureAwait(false);

        public async Task<IList<string>> GetPageCacheAsync(int pageNo) =>
            await Task.Run(() =>
            {
                var page = GetNewPageInfo(pageNo);
                ////var fetched = this.cache.TryGetValue(page, out var result);
                _ = this.cache.TryGetValue(page, out var result);
                return result;
            }).ConfigureAwait(false);

        public async Task SetPageCacheAsync(int pageNo, IList<string> lines) =>
            await Task.Run(() =>
            {
                var page = GetNewPageInfo(pageNo);
                this.cache.TryAdd(page, lines);
            }).ConfigureAwait(false);


        private static PageInfo GetNewPageInfo(int pageNo) =>
            new PageInfo(pageNo);
    }
}