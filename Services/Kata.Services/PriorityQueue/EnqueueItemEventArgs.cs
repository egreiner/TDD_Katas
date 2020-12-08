namespace Kata.Services.PriorityQueue
{
    public class EnqueueItemEventArgs<T>: System.EventArgs
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