using System.Diagnostics;

namespace Izh_04_tasks
{
    public class BiggestNumber
    {
        /// <summary>
        /// Возвращает ближайшее к указанному максимальное целое число. Если такового нет, то -1.
        /// </summary>
        /// <param name="number">Целое положительное число</param>
        /// <returns>Ближайшее полодительное целое число. -1 если такового нет или указано отрицательное число.</returns>
        public static int FindNextBiggerNumber(int number)
        {
            if (number < 0)
            {
                return -1;
            }

            string str = number.ToString();

            for (int i = str.Length - 1; i > 0; i--)
            {
                if (int.Parse(str[i].ToString()) > int.Parse(str[i - 1].ToString()))
                {
                    str = Swap(str, i, i - 1);
                    str = Sort(str, i);
                }

                if (int.Parse(str) > number)
                {
                    return int.Parse(str);
                }
            }

            return -1;
        }

        /// <summary>
        /// Находит ближайшее максимальное число от указанного. Расчитывается время, затраченное на поиск.
        /// </summary>
        /// <param name="number">Указанное число.</param>
        /// <param name="ms">Переменная для сохранения времени поиска.</param>
        /// <returns>Ближайшее максимальнео число.</returns>
        public static int FindNextBiggerNumber(int number, out long ms)
        {
            Stopwatch time = new Stopwatch();
            time.Start();

            int result = FindNextBiggerNumber(number);
            time.Stop();
            ms = time.ElapsedMilliseconds;
            return result;
        }

        /// <summary>
        /// Меняет местами указанные символы в строке.
        /// </summary>
        /// <param name="str">Строка</param>
        /// <param name="indx1">Индекс первого символа</param>
        /// <param name="indx2">Индекс второго символа</param>
        /// <returns>Полученная строка.</returns>
        public static string Swap(string str, int indx1, int indx2)
        {
            char[] tmpCharArr = str.ToCharArray();
            char tmpPos = tmpCharArr[indx1];
            tmpCharArr[indx1] = tmpCharArr[indx2];
            tmpCharArr[indx2] = tmpPos;
            return new string(tmpCharArr);
        }

        /// <summary>
        /// Сортирует строку, содержащую цифры, начиная с указанного индекса и до конца.
        /// </summary>
        /// <param name="str">Строка, содержащая цифры.</param>
        /// <param name="startIndx">Начальный индекс.</param>
        /// <returns>Отсортированная строка.</returns>
        public static string Sort(string str, int startIndx)
        {
            char[] sortTail = str.ToCharArray(startIndx, str.Length - startIndx);
            for (int i = 0; i < sortTail.Length - 1; i++)
            {
                if (int.Parse(sortTail[i].ToString()) > int.Parse(sortTail[i + 1].ToString()))
                {
                    char tmp = sortTail[i];
                    sortTail[i] = sortTail[i + 1];
                    sortTail[i + 1] = tmp;
                    i = -1;
                }
            }

            return string.Concat(str.Substring(0, startIndx), new string(sortTail));
        }
    }
}
