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

        public void Add(T item)
        {
            var element = CreateElement(item);
            ////this.lastAddedElement?.Next = element;

            this.lastAddedElement = element;
            this.internalList.Add(element);
        }

        public void Clear() =>
            this.internalList.Clear();

        public bool Contains(T item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public T this[int index]
        {
            get => this.internalList[index].Item;
            set => this.internalList[index] = CreateElement(value);
        }



        private static Element<T> CreateElement(T item) =>
            new Element<T>(item);
    }
}