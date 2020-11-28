namespace LearningTests.LinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class MyLinkedList<T>: IList<T>
    {
        private readonly Enumerator enumerator;

        private Element<T> rootElement;
        private Element<T> lastElement;
        

        public MyLinkedList() =>
            this.enumerator = new Enumerator(this);


        public int Count { get; private set; }

        public bool IsReadOnly => false;


        public IEnumerator<T> GetEnumerator() => 
            this.enumerator;

        IEnumerator IEnumerable.GetEnumerator() =>
            this.enumerator;

        
        public void Clear()
        {
            this.rootElement = null;
            this.lastElement = null;
            this.Count = 0;
        }

        public bool Contains(T item) =>
            this.EnumerateAllItems().Any(x => x.Item.Equals(item));

        public int IndexOf(T item)
        {
            var element = this.EnumerateAllItems().FirstOrDefault(x => x.Item.Equals(item));
            if (element == null) return -1;

            return this.Count - 1 - element.Index;
        }


        public void Add(T item)
        {
            var element = CreateElement(item);

            if (this.Count == 0)
                this.rootElement = element;

            if (this.lastElement != null)
                this.lastElement.Next = element;

            this.lastElement = element;

            this.Count++;
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
            ////// catch fails arrayIndex
            ////var length = this.internalList.Count;
            ////Element<T>[] x = new Element<T>[length];
            ////this.internalList.CopyTo(x, 0);

            ////for (int i = 0; i < length; i++)
            ////{
            ////    array[i + arrayIndex] = x[i].Item;
            ////}
        }

        /// <summary>
        /// (0)->(1)->(insert here)->(2)->(3)->
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);

            if (this.Count == 0)
            {
                this.Add(item);
                return;
            }

            var newElement = CreateElement(item);
            this.Count++;

            if (index == 0)
            {
                newElement.Next  = this.rootElement;
                this.rootElement = newElement;
            }
            else
            {
                var elements = this.EnumerateAllItems()
                                .Where(x => x.Index >= index-1)
                                .Take(2).ToList();

                elements[0].Next = newElement;
                
                if (elements.Count == 2)
                    newElement.Next = elements[1];
            }
        }

        private void ValidateIndex(int index)
        {
            if (this.Count < index || index < 0)
                throw new IndexOutOfRangeException();
        }


        // TODO check Element.Next
        public bool Remove(T item)
        {
            return false;
        }

        // TODO check Element.Next
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get
            {
                var element = this.GetElementAt(index);
                return element != null ? element.Item : default;
            }
            set
            {
                var element = this.GetElementAt(index);
                if (element == null) throw new IndexOutOfRangeException();

                element.Item = value;
            }
        }


        public IEnumerable<Element<T>> EnumerateAllItems()
        {
            var element = this.rootElement;
            yield return element;

            while (element.Next != null)
            {
                element = element.Next;
                yield return element;
            }
        }

        private Element<T> GetElementAt(int index) =>
            this.EnumerateAllItems()
                .SingleOrDefault(x => index == this.Count - 1 - x.Index);

        private static Element<T> CreateElement(T item) =>
            new Element<T>(item);


        private class Enumerator : IEnumerator<T>
        {
            private readonly MyLinkedList<T> root;

            private Element<T> current;

            public Enumerator(MyLinkedList<T> root)
            {
                this.root = root;
                this.Reset();
            }

            public bool MoveNext()
            {
                // initialize first access
                if (this.current == null)
                {
                    this.Reset();
                    return this.current != null;
                }

                var hasNext = this.current?.Next != null;

                if (hasNext) this.current = this.current.Next;

                return hasNext;
            }

            public void Reset() =>
                this.current = this.root.rootElement;


            public T Current => this.current.Item;

            object IEnumerator.Current => this.Current;

            public void Dispose() => this.Reset();
        }
    }
}