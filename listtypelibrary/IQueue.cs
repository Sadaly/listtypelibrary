namespace listtypelibrary
{
    internal interface IQueue<T>: ICopy<T>
    {
        void Clear();
        bool Contains();
        int Count();
        void Dequeue();
        void Enqueue(T item);
        T Pick();
    }
}
