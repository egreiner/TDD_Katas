namespace LearningTests.LinkedList
{
    public class Element<T>
    {
        public Element(T item)
        {
            this.Item = item;
        }

        public int InvertedIndex => this.Next?.InvertedIndex + 1 ?? 1;

        public T Item { get; set; }

        public Element<T> Next { get; set; }


        public override string ToString()
        {
            var item = this.Item?.ToString();
            var nextItem = this.Next?.Item?.ToString() ?? "end";
            return $"[{this.InvertedIndex}] {item} -> {nextItem}";
        }

        public override bool Equals(object? obj) =>
            obj is Element<T> x && (x.Item?.Equals(this.Item) ?? false);

        public override int GetHashCode() =>
            this.Item?.GetHashCode() ?? -1;
    }
}