using System;
using System.Diagnostics;

namespace Izh_06_tasks
{
    public enum FindNODType
    {
        Euclid,
        Stein,
    }

    public class NODCalculates
    {
        /// <summary>
        /// Метод-обёртка для вычисления НОД чисел a и b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int CalculateNOD(FindNODType findType, int a, int b)
        {
            if (findType == FindNODType.Euclid)
            {
                return a > b ? FindNOD(a, b) : FindNOD(b, a);
            }
            else
            {
                return a > b ? FindNODByGCD(a, b) : FindNODByGCD(b, a);
            }
        }

        /// <summary>
        /// Вычисление НОД у произвольного числа чисел.
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static int CalculateNOD(FindNODType findType, params int[] digits)
        {
            if (digits.Length < 2)
            {
                throw new ArgumentException();
            }

            int iter = CalculateNOD(findType, digits[0], digits[1]);

            for (int i = 2; i < digits.Length; i++)
            {
                iter = CalculateNOD(findType,iter, digits[i]);
            }

            return iter;
        }

        /// <summary>
        /// Рассчитывает время выполнения переданного метода в миллисекундах при помощи StopWatch.
        /// </summary>
        /// <param name="method">Метод, длительность работы которого подсчитывается.</param>
        /// <param name="time">Время в миллисекундах.</param>
        /// <returns>Результат работы метода.</returns>
        public static int FuncBenchmarkMilliSeconds(Func<int> method, out long time)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            int result = method();
            stopWatch.Stop();
            time = stopWatch.ElapsedMilliseconds;
            return result;
        }

        /// <summary>
        /// Рассчитывает время выполнения переданного метода в тиках процессора.
        /// </summary>
        /// <param name="method">Метод, длительность работы которого подсчитывается.</param>
        /// <param name="time">Количество тиков процессора.</param>
        /// <returns>Результат работы метода.</returns>
        public static int FuncBenchmarkUTCTicks(Func<int> method, out long time)
        {
            long start = DateTime.UtcNow.Ticks;
            int result = method();
            time = DateTime.UtcNow.Ticks - start;
            return result;
        }

        /// <summary>
        /// Находит НОД чисел u и v по бинарному алгоритму Штейна.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns>НОД чисел.</returns>
        public static int FindNODByGCD(int u, int v)
        {
            if (u < 0 || v < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (u == 0)
            {
                return v;
            }

            if (v == 0)
            {
                return u;
            }

            if (u == v)
            {
                return v;
            }

            if (Convert.ToBoolean(~u & 1))
            {
                if (Convert.ToBoolean(v & 1))
                {
                    return FindNODByGCD(u >> 1, v);
                }
                else
                {
                    return FindNODByGCD(u >> 1, v >> 1) << 1;
                }
            }

            if (Convert.ToBoolean(~v & 1))
            {
                return FindNODByGCD(u, v >> 1);
            }

            if (u > v)
            {
                return FindNODByGCD((u - v) >> 1, v);
            }

            return FindNODByGCD((v - u) >> 1, u);
        }

        /// <summary>
        /// Вычисление НОД двух чисел a и b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int FindNOD(int a, int b)
        {
            if (b == 0)
            {
                return Math.Abs(a);
            }

            return FindNOD(b, a % b);
        }
    }
}
