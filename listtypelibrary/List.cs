using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace listtypelibrary
{
    internal class List<T> : IList<T>
        where T : IComparable<T>
    {
        private T[] _items;
        private int _size; // количество элементов списка
        private const int DefaultCapacity = 4;

        // Осуществление доступа к элементам по индексу
        public T this[int index]
        { 
            get
            {
                if (index < 0 || index >= _size)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _size)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                _items[index] = value;
            }
        }

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

        // Конструктор с изначально заданными элементами в виде массива
        public List(T[] items)
        {
            int capacity = DefaultCapacity;
            while (capacity < items.Length)
            {
                capacity = capacity << 1;
            }

            _items = new T[capacity];
            items.CopyTo(_items, 0);
        }

        // Добавление элемента в конец списка
        public void Add(T item)
        {
            if (_size == _items.Length)
                ResizeArray();

            _items[_size++] = item;
        }

        // Добавление коллекции элементов в конец списка
        public void AddRange(IEnumerable<T> collection)
        {
            while (_size + collection.Count() > _items.Length)
                ResizeArray();

            foreach (T item in collection)
                _items[_size++] = item;
        }

        // Бинарный поиск элемента на интервале по условию
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

        // Бинарный поиск элемента на интервале по условию
        public int BinarySearch(int start, int end, T item, IComparer<T> comparer)
        {
            return _BinarySearch(start, end, item, comparer);
        }

        // Бинарный поиск элемента 
        public int BinarySearch(T item)
        {
            return _BinarySearch(0, _items.Length, item);
        }

        // Бинарный поиск элемента по условию
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return _BinarySearch(0, _items.Length, item, comparer);
        }

        // Сброс списка к стандартным значениям
        public void Clear()
        {
            _items = new T[DefaultCapacity];
            _size = 0;
        }

        // Проверка на наличие элемента
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        // Копирование в массив
        public void CopyTo(T[] array)
        {
            _items.CopyTo(array, 0);
        }

        // Копирование в массив с индекса
        public void CopyTo(T[] array, int start_index)
        {
            _items.CopyTo(array, start_index);
        }

        // Получает количество элементов
        public int Count
        {
            get { return _size; }
        }
        
        // Сравнивает списки
        public bool Equals(IList<T> list)
        {
            List<T> temp_list = (List<T>)list;
            if (this.Count == temp_list.Count && this.GetType() == temp_list.GetType())
            {
                for (int i = 0; i < temp_list.Count; i++)
                {
                    if (temp_list[i].CompareTo(this[i]) != 0)
                        return false;
                }
            }
            return true;
        }

        // Проверка существования элемента, удовлетворяющего условию
        public bool Exists(Predicate<T> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate can't be null.");

            for (int i = 0; i < this._size; i++)
            {
                if (predicate(_items[i]))
                {
                    return true;
                }
            }
            return false;
        }

        // Находит первый элемент, удовлетворяющий предикату
        public T Find(Predicate<T> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate can't be null.");

            for (int i = 0; i < this._size; i++)
            {
                if (predicate(this[i]))
                {
                    return this._items[i];
                }
            }

            return default(T);
        }

        // Находит все элементы, удовлетворяющие предикату
        public T[] FindAll(Predicate<T> predicate)
        {
            List<T> result = new List<T>();

            if (predicate == null)
                throw new ArgumentNullException("Predicate can't be null.");

            for (int i = 0; i < this._size; i++)
            {
                if (predicate(this[i]))
                {
                    result.Add(this._items[i]);
                }
            }

            return result.ToArray();
        }

        // Находит индекс всех элементов, удовлетворяющих предикату
        public int[] FindAllIndex(Predicate<T> predicate)
        {
            List<int> result = new List<int>();

            if (predicate == null)
                throw new ArgumentNullException("Predicate can't be null.");

            for (int i = 0; i < this._size; i++)
            {
                if (predicate(this[i]))
                {
                    result.Add(i);
                }
            }

            return result.ToArray();
        }

        // Находит индекс первого элемента, удовлетворяющего предикату
        public int FindIndex(Predicate<T> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("Predicate can't be null.");

            for (int i = 0; i < this._size; i++)
            {
                if (predicate(this[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        // Находит последний элемент, удовлетворяющий предикату
        public T FindLast(Predicate<T> predicate)
        {
            T result;
            if (predicate == null)
                throw new ArgumentNullException("Predicate can't be null.");

            for (int i = 0; i < this._size; i++)
            {
                if (predicate(this[i]))
                {
                    result = this[i];
                }
            }

            return default(T);
        }

        // Находит индекс последнего элемента, удовлетворяющего предикату
        public int FindLastIndex(Predicate<T> predicate)
        {
            int result = -1;
            if (predicate == null)
                throw new ArgumentNullException("Predicate can't be null.");

            for (int i = 0; i < this._size; i++)
            {
                if (predicate(this[i]))
                {
                    result = i;
                }
            }

            return result;
        }

        // Возвращает индекс элемента, если он есть
        public int IndexOf(T item)
        {
            return _IndexOf(item, 0, _size-1);
        }

        // Возвращает индекс элемента, если он есть в указанном диапазоне
        public int IndexOf(T item, int start_index, int end_index)
        {
            return _IndexOf(item, start_index, end_index);
        }

        // Вспомогательный метод поиска индекса элемента
        private int _IndexOf(T item, int start_index, int end_index)
        {
            if (start_index < 0)
                throw new ArgumentOutOfRangeException("Start index is out of range.");
            if (end_index >= _size)
                throw new ArgumentOutOfRangeException("End index is out of range.");
            if (end_index < start_index)
                throw new ArgumentOutOfRangeException("End index goes before start index.");

            for (int i = start_index; i <= end_index; i++)
            {
                if (this[i].CompareTo(item) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        // Вставляет значение в список
        public void Insert(int index, T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var first_part = Helper<T>.SubArray(_items, 0, index + 1);
            first_part[index] = item;
            var second_part = Helper<T>.SubArray(_items, index, _size - index + 1);
            
            _items = first_part.Concat(second_part).ToArray();
        }

        // Вставляет коллекцию в список
        public void InsertRange(int index, IEnumerable<T> range)
        {
            if (range == null)
            {
                throw new ArgumentNullException("range");
            }

            var first_part = Helper<T>.SubArray(_items, 0, index);
            var second_part = Helper<T>.SubArray(_items, index, _size - index + 1);

            while (_size + range.Count() > first_part.Length)
                ResizeArray();

            foreach (T item in range)
                first_part[_size++] = item;

            _items = first_part.Concat(second_part).ToArray();
        }

        // Возвращает последний индекс элемента, равного данному
        public int LastIndexOf(T item)
        {
            return _LastIndexOf(item, 0, _size);
        }

        // Возвращает последний индекс элемента, равного данному, в указанном диапазоне
        public int LastIndexOf(T item, int start_index, int end_index)
        {
            return _LastIndexOf(item, start_index, end_index);
        }

        // Вспомогательный метод для возврата последнего индекса элемента, равного данному
        private int _LastIndexOf(T item, int start_index, int end_index)
        {
            if (start_index < 0)
                throw new ArgumentOutOfRangeException("Start index is out of range.");
            if (end_index >= _size)
                throw new ArgumentOutOfRangeException("End index is out of range.");
            if (end_index < start_index)
                throw new ArgumentOutOfRangeException("End index goes before start index.");

            int result = -1;

            for (int i = start_index; i <= end_index; i++)
            {
                if (this[i].CompareTo(item) == 0)
                {
                    result = i;
                }
            }

            return result;
        }

        // Удаляет первое вхождение значения
        public void Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            int remove_index = -1;

            for (int i = 0; i < _size; i++)
            {
                if (this[i].CompareTo(item) == 0)
                {
                    remove_index = i;
                    break;
                }
            }

            if (remove_index < 0)
            {
                return;
            }
            else
            {
                var first_part = Helper<T>.SubArray(_items, 0, remove_index);
                var second_part = Helper<T>.SubArray(_items, remove_index + 1, _size - remove_index - 1);
                _items = first_part.Concat(second_part).ToArray();
            }
        }

        // Удаляет все вхождения значения
        public void RemoveAll(Predicate<T> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            List<int> indexes = new List<int>();

            for (int i = 0; i < _size; i++)
            {
                if (predicate(this[i]))
                {
                    indexes.Add(i);
                }
            }

            int start_index = 0;
            T[] result = new T[_size - indexes.Count];
            foreach (int i in indexes)
            {
                for (int j = start_index; j < i; j++)
                {
                    result[j] = this[i];
                }
                start_index = start_index + i + 1;
            }

            _items = result;
        }

        // Удаляет значение под индексом
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException("Index is out of range.");

            var first_part = Helper<T>.SubArray(_items, 0, index);
            var second_part = Helper<T>.SubArray(_items, index + 1, _size - index - 1);
            _items = first_part.Concat(second_part).ToArray();
       
        }

        // Удаляет значения на отрезке
        public void RemoveRange(int start_index, int end_index)
        {
            if (start_index < 0)
                throw new ArgumentOutOfRangeException("Start index is out of range.");
            if (end_index >= _size)
                throw new ArgumentOutOfRangeException("End index is out of range.");
            if (end_index < start_index)
                throw new ArgumentOutOfRangeException("End index goes before start index.");

            var first_part = Helper<T>.SubArray(_items, 0, start_index);
            var second_part = Helper<T>.SubArray(_items, end_index + 1, _size - end_index - 1);
            _items = first_part.Concat(second_part).ToArray();
        }

        // Расширяет размер массива
        private void ResizeArray()
        {
            int newCapacity = _items.Length == 0 ? DefaultCapacity : _items.Length << 1;
            T[] newArray = new T[newCapacity];
            Array.Copy(_items, newArray, _size);
            _items = newArray;
        }

        public void Reverse()
        {
            _Reverse(0, _size - 1);
        }

        public void Reverse(int start_index, int end_index)
        {
            _Reverse(start_index, end_index);
        }

        private void _Reverse(int start_index, int end_index)
        {
            Array.Reverse(_items, start_index, end_index - start_index + 1);
        }

        public void Sort()
        {
            _Sort(0, _size - 1);
        }

        public void Sort(int start_index, int end_index)
        {
            _Sort(start_index, end_index);
        }

        public void _Sort(int start_index, int end_index)
        {
            Array.Sort(_items, start_index, end_index - start_index + 1);
        }

        // Конвертируем список в массив
        public T[] ToArray()
        {
            T[] result = new T[_size];
            for (int i = 0; i < _size; i++)
            {
                result[i] = _items[i];
            }
            return result;
        }

        #region Реализация IEnumerable<T> 
        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ListEnumerator : IEnumerator<T>
        {
            private readonly List<T> _list;
            private int _index;
            private T? _current;
            
            public ListEnumerator(List<T> list)
            {
                _list = list;
                _index = -1;
                _current = default(T);
            }

            public T Current => _current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                // Не требует реализации
            }

            public bool MoveNext()
            {
                if (++_index >= _list.Count)
                {
                    return false;
                }

                _current = _list[_index];
                return true;
            }

            public void Reset()
            {
                _index = -1;
                _current = default(T);
            }
        }
        #endregion
    }
}