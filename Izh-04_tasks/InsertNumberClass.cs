namespace Izh_04_tasks
{
    using System;

    /// <summary>
    /// Класс тестового задания.
    /// </summary>
    public class InsertNumberClass
    {
        private const string CONDITIONERR = "i variable greater than j";

        /// <summary>
        /// Копирует биты с 0-го по (j-i) из первого числа во второе с i-го по j-й разряды.
        /// </summary>
        /// <param name="numberSource">Первое число.</param>
        /// <param name="numberIn">Второе число.</param>
        /// <param name="i">i-й разряд.</param>
        /// <param name="j">j-й разряд.</param>
        /// <returns>Полученное число.</returns>
        public static int InsertNumber(int numberSource, int numberIn, int i, int j)
        {
            if (i > j)
            {
                throw new Exception(CONDITIONERR);
            }

            int copyBitsLength = j - i;

            int bitMask = 1;

            for (int k = 0; k < copyBitsLength; k++)
            {
                bitMask = (bitMask << 1) + 1;
            }

            int copyBits = numberIn & bitMask;

            int shiftedNumberIn = copyBits << i;

            return numberSource | shiftedNumberIn;
        }
    }
}
