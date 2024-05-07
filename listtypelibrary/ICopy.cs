namespace listtypelibrary
{
    internal interface ICopy<T>
    {
        void CopyTo(T[] array);
        void CopyTo(T[] array, int start_index);
        T[] ToArray();
    }
}
