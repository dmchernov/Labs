using System;

namespace Izh_05_tasks
{
    public class NthRootClass
    {
        /// <summary>
        /// Нахождение корня числа number степени power с точностю accuracy.
        /// </summary>
        /// <param name="number">Число.</param>
        /// <param name="power">Степень корня.</param>
        /// <param name="accuracy">Точность.</param>
        /// <returns>Корень числа с заданной точностью.</returns>
        public static decimal FindNthRoot(double number, int power, double accuracy)
        {
            if (accuracy < 0 || (number < 0 && (power % 2) == 0) || power < 0)
            {
               throw new ArgumentOutOfRangeException();
            }

            if (number == 1)
            {
                return 1;
            }

            double x0 = number > 1 ? number / 2 : number / 0.5;
            double x1 = x0 - (Fx(x0, power, number) / DFx(x0, power));

            while (System.Math.Abs(x1 - x0) > accuracy)
            {
                x0 = x1;
                x1 = x1 - (Fx(x1, power, number) / DFx(x1, power));
            }

            byte roundMark = 0;
            double iterAccuracy = accuracy;
            while (iterAccuracy < 1)
            {
                iterAccuracy *= 10;
                roundMark++;
            }

            return (decimal)System.Math.Round(x1, roundMark);
        }

        /// <summary>
        /// Функция X^y - C.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private static double Fx(double x, int y, double number)
        {
            return System.Math.Pow(x, y) - number;
        }

        /// <summary>
        /// Производная функции Fx.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static double DFx(double x, int y)
        {
            return y * System.Math.Pow(x, y - 1);
        }
    }
}
