namespace Kata.Services.PriorityQueue
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PriorityQueue<T>
    {
        private readonly object lockObject = new object();
        private readonly List<PriorityItem<T>> queue =
            new List<PriorityItem<T>>();

        public int Count => this.queue.Count;

        public bool HasItems => this.queue.Count > 0;
        
        
        public void Enqueue(T item, int priority)
        {
            lock (this.lockObject)
            {
                this.queue.Add(new PriorityItem<T>(item, priority));
            }
        }
        
        public bool TryDequeue(out T element)
        {
            lock (this.lockObject)
            {
                element = default;
                var first = this.queue.Where(x => x != null)
                    .OrderBy(x => x.Priority)
                    .ThenBy(x => x.Created).FirstOrDefault();

                if (first == null) return false;

                element = first.Item;

                this.queue.Remove(first);
                return true;
            }
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

            public override string ToString() =>
                $"{this.Item} (Priority:{this.Priority})";
        }
    }
}