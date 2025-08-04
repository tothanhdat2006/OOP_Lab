using System;

namespace Calendar
{
    public class DoublyLinkedList<T>
    {
        public Node<T>? Head = null;
        public Node<T>? Tail = null;
        public int Count = 0;

        public void AddLast(T data)
        {
            Node<T> newNode = new Node<T>(data);
            if (Head == null)
            {
                Head = Tail = newNode;
            }
            else
            {
                Tail!.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
            Count++;
        }

        public void Remove(Node<T>? node)
        {
            if (node == null) return;

            if (node == Head) Head = Head!.Next;
            if (node == Tail) Tail = Tail!.Prev;

            if (node.Prev != null) node.Prev.Next = node.Next;
            if (node.Next != null) node.Next.Prev = node.Prev;

            Count--;
        }

        public Node<T>? GetAt(int index)
        {
            if (index < 0 || index >= Count) return null;
            Node<T>? current = Head;
            for (int i = 0; i < index; i++)
                current = current!.Next;
            return current;
        }

        public void EditAt(int index, T newData)
        {
            Node<T>? node = GetAt(index);
            if (node != null)
                node.Data = newData;
        }

        public void PrintAll()
        {
            Node<T>? current = Head;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }
    }
}
