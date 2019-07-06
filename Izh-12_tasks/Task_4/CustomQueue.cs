using System;
using System.Collections;
using System.Collections.Generic;

namespace Izh_12_tasks.Task_4
{
    /// <summary>
    /// Класс-дженерик типа стэк FIFO.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomQueue<T> : IEnumerable<T>
    {
        private T[] container;

        public CustomQueue(T[] array)
        {
            this.container = new T[array.Length];
            for (int i = 0; i < this.container.Length; i++)
            {
                this.container[i] = array[i];
            }
        }

        public CustomQueue()
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
        /// Количество элементов в очереди.
        /// </summary>
        public int Count
        {
            get
            {
                return container.Length;
            }
        }

        /// <summary>
        /// Добавить элемент в очередь.
        /// </summary>
        /// <param name="item">Элемент для добавления.</param>
        public void Enqueue(T item)
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
        /// Очистить очередь.
        /// </summary>
        public void Clear()
        {
            container = new T[0];
        }

        /// <summary>
        /// Проверяет, содежит ли очередь искомый элемент.
        /// </summary>
        /// <param name="item">Искомый элемент.</param>
        /// <returns>true -- содержит, иначе false.</returns>
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
        /// Копировать очередь в указанный массив начиная с указанного индекса.
        /// </summary>
        /// <param name="array">Массив.</param>
        /// <param name="arrayIndex">Номер индекса массива.</param>
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
        /// Извлечь следующий элемент из очереди.
        /// </summary>
        /// <returns>Извлечённый элемент.</returns>
        public T Dequeue()
        {
            if (container.Length == 0)
            {
                throw new InvalidOperationException();
            }

            T[] tmp = new T[container.Length - 1];
            T resultValue = container[0];

            for (int i = 1; i < container.Length; i++)
            {
                tmp[i-1] = container[i];
            }

            container = tmp;

            return resultValue;
        }

        /// <summary>
        /// Получить следующий элемент не удаляя его из очереди.
        /// </summary>
        /// <returns>Следующий элемент очереди.</returns>
        public T Peek()
        {
            if (container.Length == 0)
            {
                throw new InvalidOperationException();
            }

            return container[0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        struct Iterator : IEnumerator<T>
        {
            private readonly CustomQueue<T> collection;
            private int current;

            public Iterator(CustomQueue<T> collection)
            {
                this.collection = collection;
                current = -1;
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
                return ++current < collection.Count;
            }

            public void Reset()
            {
                current = -1;
            }
        }
    }
}
