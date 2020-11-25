namespace LearningTests.LinkedList
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class MyLinkedList<T>: IList<T>
    {
        private readonly IList<Element<T>> internalList = new List<Element<T>>();

        private Element<T> lastAddedElement;


        public int Count => this.internalList.Count;

        public bool IsReadOnly => this.internalList.IsReadOnly;



        public IEnumerator<T> GetEnumerator()
        {
            // TODO ???
            return this.internalList.Select(e => e.Item).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();

        
        
        public void Clear() =>
            this.internalList.Clear();

        public bool Contains(T item) =>
            this.internalList.Contains(CreateElement(item));



        public void Add(T item)
        {
            var element = CreateElement(item);

            if (this.lastAddedElement != null)
                this.lastAddedElement.Next = element;

            this.lastAddedElement = element;
            this.internalList.Add(element);
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            // catch fails arrayIndex
            var length = this.internalList.Count;
            Element<T>[] x = new Element<T>[length];
            this.internalList.CopyTo(x, 0);

            for (int i = 0; i < length; i++)
            {
                array[i + arrayIndex] = x[i].Item;
            }
        }

        public int IndexOf(T item) =>
            this.internalList.IndexOf(CreateElement(item));

        // TODO check Element.Next
        public void Insert(int index, T item) =>
            this.internalList.Insert(index, CreateElement(item));


        // TODO check Element.Next
        public bool Remove(T item)
        {
            return this.internalList.Remove(CreateElement(item));
        }

        // TODO check Element.Next
        public void RemoveAt(int index)
        {
            this.internalList.RemoveAt(index);
        }


        public T this[int index]
        {
            get => this.internalList[index].Item;
            set => this.internalList[index] = CreateElement(value);
        }

        public Element<T> GetItem(int index) =>
            this.internalList[index];


        private static Element<T> CreateElement(T item) =>
            new Element<T>(item);
    }
}