namespace Kata.Services.Cache
{
    using System.Linq;
    using Logger;

    /// <summary>
    /// Least Recently Used cache (LRU)
    /// </summary>
    public class LRUCache<TKey, TValue>: Cache<TKey, TValue>
    {
        public LRUCache(int maxCacheLength)
        {
            this.MaxCacheLength = maxCacheLength;
        }

        public int MaxCacheLength { get; }


        public override void Set(TKey key, TValue value)
        {
            this.RemoveToLength(this.MaxCacheLength - 1);
            base.Set(key, value);
        }


        private void RemoveToLength(int maxLength)
        {
            while (this.CachedItems.Count > maxLength)
            {
                var x = this.CachedItems.OrderBy(x => x.Value.Fetched)
                    .FirstOrDefault();
                Log.Add($"Removing key {x.Key} from LFU-cache");
                this.CachedItems.TryRemove(x);
            }
        }
    }
}