namespace listtypelibrary
{
    internal class Helper<T>
    {
        public static T[] SubArray(T[] array, int start_index, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, start_index, result, 0, length);
            return result;
        }
    }
}
