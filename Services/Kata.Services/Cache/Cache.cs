namespace Kata.Services.Cache
{
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    public class Cache<TKey, TValue>
    {
        public ConcurrentDictionary<TKey, TValue> Items { get; } =
            new ConcurrentDictionary<TKey, TValue>();
        

        public async Task<bool> IsCached(TKey key) =>
            await Task.Run(() => this.Items.ContainsKey(key)).ConfigureAwait(false);

        public async Task<TValue> GetPageCacheAsync(TKey key) =>
            await Task.Run(() =>
            {
                _ = this.Items.TryGetValue(key, out TValue result);
                return result;
            }).ConfigureAwait(false);

        public async Task<bool> SetPageCacheAsync(TKey key, TValue value) =>
            await Task.Run(() => this.Items.TryAdd(key, value))
                .ConfigureAwait(false);
    }
}