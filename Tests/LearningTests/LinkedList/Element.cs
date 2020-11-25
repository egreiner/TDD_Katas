namespace LearningTests.LinkedList
{
    public class Element<T>
    {
        public Element(T item)
        {
            this.Item = item;
        }

        public T Item { get;  }

        public Element<T> Next { get; set; }
    }
}