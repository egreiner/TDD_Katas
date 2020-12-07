namespace Kata.Services.PriorityQueue
{
    using System;

    public class EnqueueItemEventArgs<T>: EventArgs
    {
        public EnqueueItemEventArgs(T item, int priority)
        {
            this.Item     = item;
            this.Priority = priority;
        }
        
        public T Item { get; }
        public int Priority { get; }
    }
}