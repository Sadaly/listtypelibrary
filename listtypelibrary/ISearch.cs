namespace listtypelibrary
{
    internal interface ISearch<T>
    {
        int BinarySearch(int start, int end, T item, IComparer<T> comparer);
        int BinarySearch(T item);
        int BinarySearch(T item, IComparer<T> comparer);
        bool Contains(T item);
        bool Exists(Predicate<T> predicate);
        T Find(Predicate<T> predicate);
        T FindAll(Predicate<T> predicate);
        T FindLast(Predicate<T> predicate);
        int FindAllIndex(Predicate<T> predicate);
        int FindIndex(Predicate<T> predicate);
        int FindLastIndex(Predicate<T> predicate);
        int IndexOf(T item);
        int IndexOf(T item, int start_index, int end_index);
        int LastIndexOf(T item);
        int LastIndexOf(T item, int start_index, int end_index);
    }
}
