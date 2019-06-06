namespace Izh_04_tasks
{
    public class RecurtionSearch
    {
        /// <summary>
        /// Метод рекурсивно находит максимальное значение в неотсортированном целочисленном массиве.
        /// </summary>
        /// <param name="array">Неотсортированный целочисленный массив</param>
        /// <param name="maxIndx">Текущий индекс с максимальным значением</param>
        /// <param name="indx">Индекс массива текущей интерации</param>
        /// <returns>Максимальное значение в массиве</returns>
        public static int Max(int[] array, int maxIndx = 0, int indx = 0)
        {
            if (array[indx] > array[maxIndx])
            {
                maxIndx = indx;
            }

            if (indx < array.Length - 1)
            {
                return Max(array, maxIndx, indx + 1);
            }
            else
            {
                return array[maxIndx];
            }
        }
    }
}
