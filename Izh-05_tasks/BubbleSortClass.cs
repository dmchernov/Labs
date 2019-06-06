using System;

namespace Izh_05_tasks
{
    /// <summary>
    /// Нахождение минимального или максимального элемента в одномерном массиве.
    /// </summary>
    public enum ExtremumSign
    {
        Min,
        Max,
    }

    /// <summary>
    /// Признак сортировки: по возрастанию или по убыванию.
    /// </summary>
    public enum SortDirection
    {
        Ascending, // по возрастанию
        Descending, // по убыванию
    }

    /// <summary>
    /// Тип сортировки матрицы.
    /// </summary>
    public enum SortType
    {
        SumOfElements, // по сумме элементов строк
        MaxElement, // по максимальным элементам строк
        MinElement, // по минимальным элементам строк
    }

    public static class BubbleSortClass
    {
        public delegate int SortFunc(int[] row);

        public static int[,] Sort(int[,] matrix, SortType sortType, SortDirection sortDirection)
        {
            if (matrix == null)
            {
                throw new ArgumentException();
            }

            SortFunc sortFunc;

            switch (sortType)
            {
                case SortType.SumOfElements:
                    sortFunc = RowSum;
                    break;
                case SortType.MaxElement:
                    sortFunc = RowMax;
                    break;
                case SortType.MinElement:
                    sortFunc = RowMin;
                    break;
                default:
                    sortFunc = RowSum;
                    break;
            }

            bool swapped = false;

            for (int j = 1; j < matrix.GetLength(0); j++)
            {
                for (int i = 0; i < matrix.GetLength(0) - j; i++)
                {
                    switch (sortDirection)
                    {
                        case SortDirection.Ascending:
                            if (sortFunc(GetRow(matrix, i)) > sortFunc(GetRow(matrix, i + 1)))
                            {
                                SwapRows(matrix, i, i + 1);
                                swapped = true;
                            }

                            break;
                        case SortDirection.Descending:
                            if (sortFunc(GetRow(matrix, i)) < sortFunc(GetRow(matrix, i + 1)))
                            {
                                SwapRows(matrix, i, i + 1);
                                swapped = true;
                            }

                            break;
                    }
                }

                if (swapped)
                {
                    swapped = false;
                }
                else
                {
                    return matrix;
                }
            }

            return matrix;
        }

        /// <summary>
        /// Сравнивает два двухмерных массива.
        /// </summary>
        /// <param name="a">Массив a.</param>
        /// <param name="b">Массив b.</param>
        /// <returns>true -- если равны, иначе -- false.</returns>
        public static bool Compare(int[,] a, int[,] b)
        {
            if (a == null || b == null || (a.GetLength(0) != b.GetLength(0)) || (a.GetLength(1) != b.GetLength(1)))
            {
                return false;
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Рассчитывает сумму элементов одномерного массива.
        /// </summary>
        /// <param name="arrayRow">Массив.</param>
        /// <returns>Сумма элементов массива.</returns>
        private static int RowSum(int[] arrayRow)
        {
            if (arrayRow == null || arrayRow.Length == 0)
            {
                throw new ArgumentException();
            }

            int sum = 0;
            for (int i = 0; i < arrayRow.Length; i++)
            {
                sum += arrayRow[i];
            }

            return sum;
        }

        /// <summary>
        /// Метод-обёртка для нахождения максимального элемента массива.
        /// </summary>
        /// <param name="arrayRow">Массив.</param>
        /// <returns>Максимальный элемент.</returns>
        private static int RowMax(int[] arrayRow)
        {
            return RowExtremum(arrayRow, ExtremumSign.Max);
        }

        /// <summary>
        /// Метод-обёртка для нахождения минимального элемента массива.
        /// </summary>
        /// <param name="arrayRow">Массив.</param>
        /// <returns>Минимальный элемент.</returns>
        private static int RowMin(int[] arrayRow)
        {
            return RowExtremum(arrayRow, ExtremumSign.Min);
        }

        /// <summary>
        /// Метод возвращает максимальный или минимальный элемент массива, в зависимости от extremumSign.
        /// </summary>
        /// <param name="arrayRow">Массив.</param>
        /// <param name="extremumSign">Флаг выбора нахождения максимального или минимального элемента enum.ExtremumSign.</param>
        /// <returns>Полученный результат.</returns>
        private static int RowExtremum(int[] arrayRow, ExtremumSign extremumSign)
        {
            if (arrayRow == null || arrayRow.Length == 0)
            {
                throw new ArgumentException();
            }

            int extremum = extremumSign == ExtremumSign.Max ? int.MinValue : int.MaxValue;

            for (int i = 0; i < arrayRow.Length; i++)
            {
                switch (extremumSign)
                {
                    case ExtremumSign.Min:
                        if (arrayRow[i] < extremum)
                        {
                            extremum = arrayRow[i];
                        }

                        break;
                    case ExtremumSign.Max:
                        if (arrayRow[i] > extremum)
                        {
                            extremum = arrayRow[i];
                        }

                        break;
                }
            }

            return extremum;
        }

        /// <summary>
        /// Меняет местами указанные строки матрица.
        /// </summary>
        /// <param name="matrix">Матрица.</param>
        /// <param name="row1">Индекс строки 1.</param>
        /// <param name="row2">Индекс строки 2.</param>
        /// <returns>Получившаяся матрица.</returns>
        private static int[,] SwapRows(int[,] matrix, int row1, int row2)
        {
            int[] tmpRow = GetRow(matrix, row1);

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[row1, i] = matrix[row2, i];
            }

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[row2, i] = tmpRow[i];
            }

            return matrix;
        }

        /// <summary>
        /// Метод возвращает указанную строку матрицы ввиде одномерного массива.
        /// </summary>
        /// <param name="matrix">Матрица.</param>
        /// <param name="row">Номер строки.</param>
        /// <returns>Строка матрицы ввиде одномерного массива.</returns>
        private static int[] GetRow(int[,] matrix, int row)
        {
            if (row < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            int[] tmpArray = new int[matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                tmpArray[i] = matrix[row, i];
            }

            return tmpArray;
        }
    }
}
