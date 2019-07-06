using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izh_12_tasks.Task_1
{
    public class BinarySearchClass
    {
        /// <summary>
        /// Осуществляет бинарный поиск по одномерному отсортированному массиву.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">Одномерный отсортированный массив.</param>
        /// <param name="value">Искомое число.</param>
        /// <returns>Индекс искомого массива.
        /// Если в массиве несколько одинаковых числе, то возвращается индекс самого первого числа.
        /// Если искомое число меньше самого минимального числа массива, то возвращается отрицательное число, которое является результатом побитового отрицания индекса первого элемента массива.
        /// Если искомое число больше самого максимального числа массива, то возвращается отрицательное число, которое является результатом побитового отрицания самого старшего индекса массива + 1.
        /// </returns>
        public static int BinarySearch<T>(T[] array, T value)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length == 0)
            {
                throw new ArgumentException(nameof(array));
            }

            var objectComparer = Comparer<T>.Default;
            int leftBound = 0;
            int rightBound = array.Length - 1;

            while (leftBound < rightBound)
            {
                int middle = (leftBound + rightBound) / 2;

                if (objectComparer.Compare(array[middle], value) < 0)
                {
                    leftBound = middle + 1;
                }
                else
                {
                    rightBound = middle;
                }
            }

            if ((leftBound < array.Length) && (objectComparer.Compare(array[leftBound], value) == 0))
            {
                return leftBound;
            }
            else if (leftBound == 0)
            {
                return ~leftBound;
            }
            else
            {
                return ~(rightBound + 1);
            }
        }
    }
}
