namespace Kata.Services.Cache
{
    public class CacheItem<T>
    {
        public CacheItem(T cachedItem)
        {
            this.CachedItem = cachedItem;
            this.Fetched = System.DateTime.Now;
        }


        public T CachedItem { get; set; }

        /// <summary>
        /// For LFU cache
        /// Least frequently used cache
        /// </summary>
        public int FetchCount { get; set; }

        /// <summary>
        /// For LRU cache
        /// Least recently used cache
        /// </summary>
        public System.DateTime Fetched { get; set; }
    }
}