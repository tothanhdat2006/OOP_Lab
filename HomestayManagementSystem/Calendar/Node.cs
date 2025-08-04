namespace Calendar
{
    public class Node<T>
    {
        public T Data;
        public Node<T>? Next;
        public Node<T>? Prev;
        public Node(T data)
        {
            this.Data = data;
            this.Next = null;
            this.Prev = null;
        }
    }
}