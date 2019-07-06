using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izh_12_tasks.Task_3
{
    class FibonacciCountClass
    {
        /// <summary>
        /// Возращает количество чисел в переданной последовательности Фибоначчи.
        /// </summary>
        /// <param name="sequence">Массив, содержащий последовательность Фибоначчи.</param>
        /// <returns>Количество чисел в последовательности.</returns>
        public static int FibonacciCount(int[] sequence)
        {
            if (sequence == null)
            {
                throw new NullReferenceException(nameof(sequence));
            }

            int count = 0;
            foreach (var item in Iter(sequence))
            {
                count++;
            }

            return count;
        }

        private static IEnumerable<int> Iter(int[] sequence)
        {
            var iterator = sequence.GetEnumerator();

            while (iterator.MoveNext())
            {
                yield return (int)iterator.Current;
            }
        }

    }
}
