namespace Kata.Services.LinkedList
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


        public IEnumerable<T> Items() =>
            this.GetElements().Select(x => x.Item);

        public void Clear()
        {
            this.rootElement = null;
            this.lastElement = null;
            this.Count = 0;
        }

        public bool Contains(T item) =>
            this.GetElements().Any(x => x.Item.Equals(item));

        public int IndexOf(T item)
        {
            var element = this.GetElements().FirstOrDefault(x => x.Item.Equals(item));
            return this.GetElementIndex(element);
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
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "The arrayIndex must be >= 0");

            if (this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(array), "This LinkedList doesn't fir into the given array...");
            
            var list = this.GetElements().ToList();
            for (int i = 0; i < this.Count; i++) 
                array[i + arrayIndex] = list[i].Item;
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
                var elements = this.GetElementsAt(index, 2);

                elements[0].Next = newElement;
                
                if (elements.Count == 2)
                    newElement.Next = elements[1];
            }
        }

        public bool Remove(T item)
        {
            var index = this.IndexOf(item);
            var found = index != -1;
            
            if (found) this.RemoveAt(index);
            return found;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            if (index == 0)
            {
                this.rootElement = this.rootElement.Next;
            }
            else
            {
                var elements = this.GetElementsAt(index - 1, 3);

                if (elements.Count == 3)
                {
                    elements[0].Next = elements[2];
                }
                else
                {
                    elements[0].Next = null;
                    this.lastElement = elements[0];
                }
            }

            this.Count--;
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
                if (element == null) 
                    throw new ArgumentOutOfRangeException(nameof(index), $"Index '{index}' could not be found.");

                element.Item = value;
            }
        }
        
        private void ValidateIndex(int index)
        {
            if (this.Count < index || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index '{index}' could not be found.");
        }

        private int GetElementIndex(Element<T> element) =>
            element == null 
                ? -1 
                : this.Count - element.InvertedIndex;


        private Element<T> GetElementAt(int index) =>
            this.GetElements()
                .SingleOrDefault(x => this.GetElementIndex(x) == index);

        private List<Element<T>> GetElementsAt(int index, int numberOfElements) =>
            this.GetElements()
                .Where(x => this.GetElementIndex(x) >= index)
                .Take(numberOfElements).ToList();

        private IEnumerable<Element<T>> GetElements()
        {
            var element = this.rootElement;
            yield return element;

            while (element.Next != null)
            {
                element = element.Next;
                yield return element;
            }
        }

        private static Element<T> CreateElement(T item) =>
            new(item);


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