namespace LearningTests.LinkedList
{
    public class Element<T>
    {
        public Element(T item)
        {
            this.Item = item;
        }

        public int Index => this.Next?.Index + 1 ?? 0;

        public T Item { get; set; }

        public Element<T> Next { get; set; }


        public override string ToString() =>
            $"{this.Item?.ToString()} ({this.Index})";

        public override bool Equals(object? obj) =>
            obj is Element<T> x && (x.Item?.Equals(this.Item) ?? false);

        public override int GetHashCode() =>
            this.Item?.GetHashCode() ?? -1;
    }
}