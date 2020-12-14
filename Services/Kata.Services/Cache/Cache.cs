namespace Kata.Services.Cache
{
    using System.Collections.Concurrent;
    using System.Collections.Immutable;

    public class Cache<TKey, TValue>
    {
        private readonly ConcurrentDictionary<CacheItem<TKey>, TValue> cache =
            new ConcurrentDictionary<CacheItem<TKey>, TValue>();


        public ImmutableDictionary<CacheItem<TKey>, TValue> Items =>
            this.cache.ToImmutableDictionary();

        public bool Contains(TKey key) =>
            this.cache.ContainsKey(CreateCacheItem(key));

        public TValue Get(TKey key)
        {
            _ = this.cache.TryGetValue(CreateCacheItem(key), out TValue result);
            return result;
        }

        public bool Set(TKey key, TValue value) =>
            this.cache.TryAdd(CreateCacheItem(key), value);


        private static CacheItem<TKey> CreateCacheItem(TKey key) =>
            new CacheItem<TKey>(key);
    }
}