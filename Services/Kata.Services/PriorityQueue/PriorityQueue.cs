namespace Kata.Services.PriorityQueue
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    public class PriorityQueue<T>
    {
        private readonly ConcurrentBag<PriorityItem<T>> queue = 
            new ConcurrentBag<PriorityItem<T>>();


        public int Count => this.queue.Count;

        public bool IsEmpty => this.queue.IsEmpty;
        
        
        public void Enqueue(T item, int priority) =>
            this.queue.Add(new PriorityItem<T>(item, priority));

        public bool TryDequeue(out T element)
        {
            element = default;
            var first = this.queue.Where(x => x != null)
                                 .OrderBy(x => x.Priority)
                                 .ThenBy(x => x.Created).FirstOrDefault();

            if (first == null) return false;

            element = first.Item;
            this.queue.TryTake(out first);

            return true;
        }


        private class PriorityItem<TItem>
        {
            public PriorityItem(TItem item, int priority)
            {
                this.Created  = DateTime.Now;
                this.Item     = item;
                this.Priority = priority;
            }

            public TItem Item { get; }
            
            public int Priority { get; }

            public DateTime Created { get; }
        }
    }
}