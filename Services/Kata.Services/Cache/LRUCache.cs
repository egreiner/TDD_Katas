namespace Kata.Services.Cache
{
    /// <summary>
    /// Least recently cache (LRU)
    /// </summary>
    public class LRUCache<TKey, TValue>: Cache<TKey, TValue>
    {
        public LRUCache(int maxCacheLength)
        {
            this.MaxCacheLength = maxCacheLength;
        }

        public int MaxCacheLength { get; }


    }
}