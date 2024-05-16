namespace listtypelibrary
{
    internal interface IList<T>: ISearch<T>, ICopy<T>, IEnumerable<T>
    {
        void Add(T item);
        void AddRange(IEnumerable<T> collection);
        void Clear();
        bool Equals(IList<T> list);
        void Insert(int index, T item);
        void InsertRange(int index, IEnumerable<T> range);
        void Remove(T item);
        void RemoveAll(Predicate<T> predicate);
        void RemoveAt(int index);
        void RemoveRange(int start, int end);
        void Reverse();
        void Reverse(int start, int end);
        void Sort();
        void Sort(int start_index, int end_index);

    }
}
