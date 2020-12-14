namespace Kata.Services.Cache
{
    using System.Linq;

    /// <summary>
    /// Least Frequently Used cache (LFU)
    /// </summary>
    public class LFUCache<TKey, TValue>: Cache<TKey, TValue>
    {
        public LFUCache(int maxCacheLength)
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
                var x = this.CachedItems.OrderBy(x => x.Value.FetchCount)
                    .FirstOrDefault();
                this.CachedItems.TryRemove(x);
            }
        }
    }
}