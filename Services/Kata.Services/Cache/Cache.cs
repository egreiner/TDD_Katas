namespace Kata.Services.Cache
{
    using System.Collections.Concurrent;
    using System.Collections.Immutable;

    public class Cache<TKey, TValue>
    {
        public ImmutableDictionary<TKey, CacheItem<TValue>> Items =>
            this.CachedItems.ToImmutableDictionary();

        protected ConcurrentDictionary<TKey, CacheItem<TValue>> CachedItems { get; set; } =
            new ConcurrentDictionary<TKey, CacheItem<TValue>>();


        public virtual bool Contains(TKey key) =>
            this.CachedItems.ContainsKey(key);

        public virtual TValue Get(TKey key)
        {
            var cached = this.GetCacheItem(key);
            return cached.CachedItem;
        }

        public virtual void Set(TKey key, TValue value)
        {
            if (this.Contains(key))
            {
                var cached            = this.GetCacheItem(key);
                cached.CachedItem     = value;
                this.CachedItems[key] = cached;
            }
            else
            {
                this.CachedItems.TryAdd(key, CreateCacheItem(value));
            }
        }


        private CacheItem<TValue> GetCacheItem(TKey key)
        {
            _ = this.CachedItems.TryGetValue(key, out var cached);
            cached.FetchCount++;
            cached.Fetched = System.DateTime.Now;
            return cached;
        }
        
        private static CacheItem<TValue> CreateCacheItem(TValue value) =>
            new CacheItem<TValue>(value);
    }
}