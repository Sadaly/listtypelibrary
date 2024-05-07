namespace listtypelibrary
{
    internal class Node<T>
    where T : struct
    {
        T Value { get; set; }
        Node<T> Previous { get; set; }
        Node<T> Next { get; set; }
    }
}
