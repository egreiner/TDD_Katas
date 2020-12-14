namespace Kata.Services.Cache
{
    using System;

    public class CacheItem<TKey>
    {
        private readonly int hashCode;


        public CacheItem(TKey key)
        {
            this.Key      = key;
            this.hashCode = key.GetHashCode();
            this.Fetched  = DateTime.Now;
        }


        public TKey Key { get; }

        /// <summary>
        /// For LFU cache
        /// Least frequently used cache
        /// </summary>
        public int FetchCount { get; set; }

        /// <summary>
        /// For LRU cache
        /// Least recently cache
        /// </summary>
        public DateTime Fetched { get; set; }


        public override string ToString() =>
            this.Key.ToString();


        public override bool Equals(object obj) =>
            obj is CacheItem<TKey> other && other.Key.Equals(this.Key);

        public override int GetHashCode() => this.hashCode;
    }
}