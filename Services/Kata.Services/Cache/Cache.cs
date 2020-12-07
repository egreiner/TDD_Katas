namespace Kata.Services.Cache
{
    using System.Collections.Concurrent;
    using System.Collections.Immutable;
    using System.Threading.Tasks;

    public class Cache<TKey, TValue>
    {
        private readonly ConcurrentDictionary<CacheItem<TKey>, TValue> cache =
            new ConcurrentDictionary<CacheItem<TKey>, TValue>();


        public ImmutableDictionary<CacheItem<TKey>, TValue> Items =>
            this.cache.ToImmutableDictionary();


        public async Task<bool> ContainsAsync(TKey key) =>
            await Task.Run(() => this.cache.ContainsKey(CreateCacheItem(key))).ConfigureAwait(false);

        public async Task<TValue> GetAsync(TKey key) =>
            await Task.Run(() =>
            {
                _ = this.cache.TryGetValue(CreateCacheItem(key), out TValue result);
                return result;
            }).ConfigureAwait(false);

        public async Task<bool> SetAsync(TKey key, TValue value) =>
            await Task.Run(() => this.cache.TryAdd(CreateCacheItem(key), value))
                .ConfigureAwait(false);


        private static CacheItem<TKey> CreateCacheItem(TKey key) =>
            new CacheItem<TKey>(key);
    }
}