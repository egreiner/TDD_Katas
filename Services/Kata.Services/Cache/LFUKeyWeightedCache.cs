namespace Kata.Services.Cache
{
    using System.Linq;
    using Logger;

    /// <summary>
    /// Least Frequently Used cache (LFU)
    /// </summary>
    public class LFUKeyWeightedCache<TKey, TValue>: Cache<TKey, TValue>
    {
        public LFUKeyWeightedCache(int maxCacheLength)
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
                var item = this.CachedItems
                    .OrderBy(x => x.Value.FetchCount)
                    .ThenBy(x => x.Key)
                    .FirstOrDefault();
                Log.Add($"Removing key {item.Key} from LFU-Key-weighted-cache");
                this.CachedItems.TryRemove(item);
            }
        }
    }
}