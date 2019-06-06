namespace Izh_04_tasks
{
    public class TwoSideSum
    {
        /// <summary>
        /// Метод возвращает индекс элемента массива, сумма элементов до которого и сумма элементов после которого равны.
        /// </summary>
        /// <param name="array">Массив вещественных чисел</param>
        /// <returns>Индекс элемента массива</returns>
        public static int FindBisectIndex(double[] array)
        {
            for (int i = 1; i < array.Length - 2; i++)
            {
                if (SliceSum(array, 0, i - 1) == SliceSum(array, i + 1, array.Length - 1))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Метод возвращает сумму элементов массива в пределах указанного диапазона индексов.
        /// </summary>
        /// <param name="array">Массив вещественных чисел.</param>
        /// <param name="from">Начальный индекс.</param>
        /// <param name="to">Конечный индекс.</param>
        /// <returns>Сумма элементов.</returns>
        private static double SliceSum(double[] array, int from, int to)
        {
            decimal sum = 0m;
            for (int i = from; i <= to; i++)
            {
                sum += (decimal)array[i];
            }

            return (double)sum;
        }
    }
}
