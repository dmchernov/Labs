using System;
using System.Numerics;

namespace Izh_09_tasks.Task_6
{
    public class BigNumbersSumClass
    {
        public static string BugNumbersSum(string num1, string num2)
        {
            BigInteger bigNum1;
            BigInteger bigNum2;

            if (!BigInteger.TryParse(num1, out bigNum1))
            {
                throw new ArithmeticException($"Ошибка при конвертации строки {num1}");
            }

            if (!BigInteger.TryParse(num2, out bigNum2))
            {
                throw new ArithmeticException($"Ошибка при конвертации строки {num2}");
            }

            return (bigNum1 + bigNum2).ToString();
        }
    }
}
