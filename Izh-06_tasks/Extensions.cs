using System;

namespace Izh_06_tasks
{
    public static class Extensions
    {
        /// <summary>
        /// Преобразует число в строку, отражающую его бинарное представление.
        /// </summary>
        /// <param name="dbl">Число типа double, которое необходимо преобразовать.</param>
        /// <returns>Строка бинарного представления.</returns>
        public static string ToBinaryString(this double dbl)
        {
            string result = string.Empty;

            byte[] array = BitConverter.GetBytes(dbl);
            for (int i = array.GetLength(0) - 1; i >= 0; i--)
            {
                string tmp = Convert.ToString(array[i], 2);
                if (tmp.Length < 8)
                {
                    tmp = new string('0', 8 - tmp.Length) + tmp;
                }

                result = result + tmp;
            }

            return result;
        }

        /// <summary>
        /// Проверяет, является ли значение null или нет.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T t)
        {
            var type = typeof(T);
            if (type.IsValueType)
            {
                if (!ReferenceEquals(Nullable.GetUnderlyingType(type), null)
                    && t.GetHashCode() == 0)
                {
                    return true;
                }
            }
            else
            {
                if (ReferenceEquals(t, null))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
