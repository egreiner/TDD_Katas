namespace Kata.Services.Cache
{
    using System;

    public class CacheItem<TKey>
    {
        private readonly int hashCode;


        public CacheItem(TKey key)
        {
            this.Key = key;
            this.hashCode = key.GetHashCode();
        }


        public TKey Key { get; }

        // TODO these are properties for the CacheGarbageCollector
        // The oldest items should be removed over time...
        public int FetchCount { get; set; }
        public DateTime Fetched { get; set; }

        public override string ToString() =>
            this.Key.ToString();


        public override bool Equals(object obj) =>
            obj is CacheItem<TKey> other && other.Key.Equals(this.Key);

        public override int GetHashCode() => this.hashCode;
    }
}