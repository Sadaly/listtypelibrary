namespace listtypelibrary
{
    internal class Node<T>
    where T : struct
    {
        public Node() { }
        public Node(T value, Node<T>? next, Node<T>? prev) {
            this.Value = value; this.Prev = prev; this.Next = next;
        }
        T Value { get; set; }
        Node<T>? Prev { get; set; }
        Node<T>? Next { get; set; }
    }
}
