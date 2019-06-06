using System;
using System.Text.RegularExpressions;

namespace Izh_04_tasks
{
    public class Concatination
    {
        private const string NOTLATINCHARS = "Concatinated strings contains not latin characters";

        /// <summary>
        /// Склеивает две строки, удалив из второй все повторяющиеся символы.
        /// </summary>
        /// <param name="one">Первая строка.</param>
        /// <param name="two">Вторая строка.</param>
        /// <returns>Склееная строка.</returns>
        public static string Concat(string one, string two)
        {
            Regex reg = new Regex("^[A-Za-z]*$");

            if (reg.IsMatch(one) && reg.IsMatch(two))
            {
                return string.Concat(one, RemoveDuplicateChars(one, two));
            }
            else
            {
                Console.WriteLine(NOTLATINCHARS);
                return "-1";
            }
        }

        /// <summary>
        /// Удаляет из второй строки символы, которые встречаются в первой стоке.
        /// </summary>
        /// <param name="srcString">Первая строка.</param>
        /// <param name="victimString">Вторая строка.</param>
        /// <returns>Полученная строка.</returns>
        public static string RemoveDuplicateChars(string srcString, string victimString)
        {
            for (int i = 0; i < srcString.Length; i++)
            {
                victimString = victimString.Replace(srcString[i].ToString(), string.Empty);
            }

            return victimString;
        }
    }
}
