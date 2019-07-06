using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izh_12_tasks.Task_5
{
    /// <summary>
    /// Класс-дженерик типа стэк LIFO.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class CustomStack<T> : IEnumerable<T>
    {
        private T[] container;

        public CustomStack(T[] array)
        {
            this.container = new T[array.Length];
            for (int i = 0; i < this.container.Length; i++)
            {
                this.container[i] = array[i];
            }
        }

        public CustomStack()
        {
            this.container = new T[0];
        }

        public T this[int index]
        {
            get
            {
                return container[index];
            }

            set
            {
                this.container[index] = value;
            }
        }

        /// <summary>
        /// Количество элементов в стеке.
        /// </summary>
        public int Count
        {
            get
            {
                return container.Length;
            }
        }

        /// <summary>
        /// Поместить элемент в стек.
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            T[] tmp = new T[container.Length + 1];
            for (int i = 0; i < container.Length; i++)
            {
                tmp[i] = container[i];
            }

            tmp[tmp.Length - 1] = item;
            container = tmp;
        }

        /// <summary>
        /// Очистить стек.
        /// </summary>
        public void Clear()
        {
            container = new T[0];
        }

        /// <summary>
        /// Проверяет, находится ли элемент в стеке.
        /// </summary>
        /// <param name="item">Искомый элемент.</param>
        /// <returns>True -- элемент присутсвуте в стеке, иначе false.</returns>
        public bool Contains(T item)
        {
            for (int i = 0; i < container.Length; i++)
            {
                if (container[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Копирует содержимое стека в указанный массив с указанного индекса.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {

            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (array.Length - arrayIndex < container.Length)
            {
                throw new ArgumentException();
            }

            if (arrayIndex < 0)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = 0; i < container.Length; i++)
            {
                array[arrayIndex + i] = container[i];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Iterator(this);
        }

        /// <summary>
        /// Извлечь элемент из стека.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (container.Length == 0)
            {
                throw new InvalidOperationException();
            }

            T[] tmp = new T[container.Length - 1];
            T resultValue = container[container.Length - 1];

            for (int i = 0; i < container.Length - 1; i++)
            {
                tmp[i] = container[i];
            }

            container = tmp;

            return resultValue;
        }

        /// <summary>
        /// Поулить следующего элемента из стека не удаляя его.
        /// </summary>
        /// <returns>Следеющий элемент стека.</returns>
        public T Peek()
        {
            if (container.Length == 0)
            {
                throw new InvalidOperationException();
            }

            return container[container.Length - 1];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        struct Iterator : IEnumerator<T>
        {
            private readonly CustomStack<T> collection;
            private int current;

            public Iterator(CustomStack<T> collection)
            {
                this.collection = collection;
                current = collection.Count;
            }

            public T Current
            {
                get
                {
                    if (current == -1 || current == collection.Count)
                    {
                        throw new InvalidOperationException();
                    }

                    return collection[current];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            void IDisposable.Dispose()
            {
            }

            public bool MoveNext()
            {
                return --current > -1;
            }

            public void Reset()
            {
                current = collection.Count;
            }
        }
    }
}
