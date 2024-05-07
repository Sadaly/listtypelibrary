namespace listtypelibrary
{
    internal class List<T>: IList<T>
        where T : struct
    {
        private T[] _items;
        private int _size;
        private const int DefaultCapacity = 4;

        // Конструктор без параметров
        public List()
        {
            _items = new T[DefaultCapacity];
        }

        // Конструктор с указанием начальной ёмкости
        public List(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be negative.");

            _items = new T[capacity];
        }

        // Добавление элемента в конец списка
        public void Add(T item)
        {
            if (_size == _items.Length)
                ResizeArray();

            _items[_size++] = item;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            while (_size + collection.Count() > _items.Length)
                ResizeArray();

            foreach (T item in collection)
                _items[_size++] = item;
        }
        private int _BinarySearch(int start, int end, T item, IComparer<T>? comparer = null)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            while (start < end)
            {
                int mid = (start + end) >> 1;
                int comparison = comparer.Compare(_items[mid], item);

                if (comparison == 0)
                    return mid;
                else if (comparison == 1)
                    start = mid + 1;
                else
                    end = mid - 1;
            }
            return -1;
        }
        public int BinarySearch(int start, int end, T item, IComparer<T> comparer)
        {
            return BinarySearch(start, end, item, comparer);
        }

        public int BinarySearch(T item)
        {
            return _BinarySearch(0, _items.Length, item);
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return _BinarySearch(0, _items.Length, item, comparer);
        }

        public void Clear()
        {
            _items = new T[DefaultCapacity];
            _size = 0;
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array)
        {
            _items.CopyTo(array, 0);
        }

        public void CopyTo(T[] array, int start_index)
        {
            _items.CopyTo(array, start_index);
        }

        public bool Exists(Predicate<T> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate ");
        }

        public T Find(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public T FindAll(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public int FindAllIndex(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public int FindIndex(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public T FindLast(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public int FindLastIndex(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T item, int start_index, int end_index)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(int index, IEnumerable<T> range)
        {
            throw new NotImplementedException();
        }

        public int LastIndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public int LastIndexOf(T item, int start_index, int end_index)
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll(Predicate<T> predicate)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(int start, int end)
        {
            throw new NotImplementedException();
        }

        private void ResizeArray()
        {
            int newCapacity = _items.Length == 0 ? DefaultCapacity : _items.Length << 1;
            T[] newArray = new T[newCapacity];
            Array.Copy(_items, newArray, _size);
            _items = newArray;
        }

        public void Reverse()
        {
            throw new NotImplementedException();
        }

        public void Reverse(int start, int end)
        {
            throw new NotImplementedException();
        }

        public void Sort()
        {
            throw new NotImplementedException();
        }

        public void Sort(int start_index, int end_index)
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }
    }
}
