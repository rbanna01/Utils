using System;
//Doubly-linked list
//Generic.
//Just for testing.
//to do: add iterator interface, maybe PeekFront and PeekBack methods.
//size method?
namespace Deque
{
	public class DoublyLinkedList<T>
	{
		private ListNode<T> head;
		private ListNode<T> tail;
		public DoublyLinkedList ()
		{
			head = null;
			tail = null;
		}

		public void AddFront(T toAdd) {
			if (toAdd == null)
				throw new InvalidOperationException ("Cannot add null value!");
			ListNode<T> newFront = new ListNode<T>(toAdd);
			if (head == null) {
				head = newFront;
				tail = newFront;
			} else {
				newFront.SetNext (head);
				newFront.SetPrev (tail);
				head.SetPrev (newFront);
				tail.SetNext (newFront);
				head = newFront;
			}
		}

		public bool HasNext() {
			return head == null;
		}

		public void AddBack(T toAdd) {
			if (toAdd == null)
				throw new InvalidOperationException ("Cannot add null value!");
			ListNode<T> newBack = new ListNode<T>(toAdd);
			if (tail == null) {
				head = newBack;
				tail = newBack;
			} else {
				newBack.SetNext (head);
				newBack.SetPrev (tail);
				tail.SetNext (newBack);
				tail = newBack;
			}
        }

		public T GetBack() {
			if (tail == null)
				throw new InvalidOperationException
				   ("Doubly-linked list is empty!");
			T output = tail.GetVal();
			ListNode<T> newTail = tail.GetPrev ();
			if (newTail == null) {
				tail = null;
				head = null;
				return output;
			}
			if (newTail == head) { //problem when one null
				newTail.SetNext (null);
				newTail.SetPrev (null);
			} else if (newTail != null) {
				newTail.SetNext (head);
				head.SetPrev (newTail);
				}
			tail = newTail;
			return output;
		}

		public T GetFront() {
			if (head == null)
				throw new InvalidOperationException
				   ("Doubly-linked list is empty!");
			T output = head.GetVal(); //when only one left?
			ListNode<T> newHead = head.GetNext ();
			if (newHead == null) {
				head = null;
				tail = null;
				return output;
			}
			if (tail == newHead) {
				newHead.SetPrev (null);
				newHead.SetNext (null);
			} else if(newHead != null) {
				newHead.SetPrev (tail);
				tail.SetNext (newHead);
				}
			head = newHead;
			return output;
		}




	}

	public class ListNode<T> {
		private ListNode<T> next;
		private ListNode<T> prev;
		private T val;

		public ListNode(T initVal) {
			this.val = initVal;
			next = null;
			prev = null;
		}

		public void SetNext(ListNode<T> newNext) {
			this.next = newNext;
		}
		public void SetPrev(ListNode<T> newPrev) {
			this.prev = newPrev;
		}
		public ListNode<T> GetNext() {
			return this.next;
		}
		public ListNode<T> GetPrev() {
			return this.prev;
		}
		public T GetVal () {
			return this.val;
		}
		public void SetVal(T newVal) {
			if (newVal == null)
				throw new NullReferenceException ("Cannot add null value!");
			this.val = newVal;
		}

	}



}

