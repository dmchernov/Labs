namespace Izh_04_tasks
{
    public class FilterDigitClass
    {
        /// <summary>
        /// Фильтрует входной массив целых чисел и возвращает массив с элементами, которые содержат указанную цифру.
        /// </summary>
        /// <param name="input">Входной массив целых чисел</param>
        /// <param name="digit">Цифры, по которой фильтруются элементы входного массива</param>
        /// <returns>Отфильтрованный массив</returns>
        public static int[] FilterDigit(int[] input, int digit)
        {
            int[] resultArray = new int[0];

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].ToString().Contains(digit.ToString()))
                {
                    resultArray = resultArray.Add(input[i]);
                }
            }

            return resultArray;
        }

        public static bool Compare(int[] a, int[] b)
        {
            if (a == null || b == null || (a.Length != b.Length))
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
