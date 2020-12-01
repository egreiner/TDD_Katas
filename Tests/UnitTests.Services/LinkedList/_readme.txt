-> https://ccd-school.de/en/coding-dojo/classes-katas/linked-list/

Class Kata „Linked List“

Implement the abstract data type list as a linked list. 
The class LinkedList<T> has to implement the interface IList<T>.

A linked list consists of elements that have a value (called Item) and 
a reference to the next element in the list (called Next):

class Element
{
    public Element(T item) {
        Item = item;
    }
 
    public T Item { get; set; }
 
    public Element Next { get; set; }
}

The list is internally built from these elements. 
But the public API does not show anything of this implementation details. 
The LinkedList<T> behaves as other classes the implement the interface IList<T>:
	
class LinkedList : IList {
 ...
}

Variation

The list may be double linked. In addition to the Next property each element has a Prev property that references the previous element in the list. This fastens the reverse traversal of the list from the last element backwards tot he first.

class Element
{
    public Element(T item) {
        Item = item;
    }
 
    public T Item { get; set; }
 
    public Element Next { get; set; }
 
    public Element Prev { get; set; }
}



