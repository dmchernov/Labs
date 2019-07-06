using System;
using System.Collections.Generic;

namespace Izh_12_tasks.Task_6
{
    public class Set<T> where T : class
    {
        private T[] container;

        public Set(T[] array)
        {
            this.container = new T[array.Length];
            for (int i = 0; i < this.container.Length; i++)
            {
                this.container[i] = array[i];
            }
        }

        public Set()
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
        /// Количество элементов в наборе.
        /// </summary>
        public int Count
        {
            get
            {
                return container.Length;
            }
        }

        /// <summary>
        /// Добавить элемент в набор.
        /// </summary>
        /// <param name="item">Элемент для добавления.</param>
        /// <returns>True -- элемент успешно добавлен, false -- если такой элемент уже присутствует в наборе.</returns>
        public bool Add(T item)
        {
            if (Contains(item))
            {
                return false;
            }

            T[] tmp = new T[container.Length + 1];
            for (int i = 0; i < container.Length; i++)
            {
                tmp[i] = container[i];
            }

            tmp[tmp.Length - 1] = item;
            container = tmp;

            return true;
        }

        /// <summary>
        /// Очистить набор.
        /// </summary>
        public void Clear()
        {
            container = new T[0];
        }

        /// <summary>
        /// Проверяет, содержит ли набор искомый элемент.
        /// </summary>
        /// <param name="item">Искомый элемент.</param>
        /// <returns>True -- содержит, иначе false.</returns>
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
        /// Копирует содежимое набора в указанный массив с указанного индекса.
        /// </summary>
        /// <param name="array">Массив.</param>
        /// <param name="arrayIndex">Номер индекса массива, с которого начинать заносить элементы набора.</param>
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

        /// <summary>
        /// Удалить элемент из набора.
        /// </summary>
        /// <param name="item">Удаляемый элемент.</param>
        /// <returns>true -- элемент успешно удалён, false -- элемент отсутсвует в наборе.</returns>
        public bool Remove(T item)
        {
            if (container.Length == 0)
            {
                throw new InvalidOperationException();
            }

            if (!Contains(item))
            {
                return false;
            }

            T[] tmp = new T[container.Length - 1];
            int j = 0;
            for (int i = 0; i < container.Length; i++)
            {
                if (!container[i].Equals(item))
                {
                    tmp[j++] = container[i];
                }
            }

            container = tmp;

            return true;
        }

        public IEnumerable<T> Iterator()
        {
            for (int i = 0; i < container.Length; i++)
            {
                yield return container[i];
            }
        }
    }
}
