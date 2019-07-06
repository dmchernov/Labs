using System;
using System.Collections.Generic;

namespace Izh_12_tasks.Task_7
{
    public class CustomInvertComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            var comparer = Comparer<T>.Default;

            if (comparer.Compare(x, y) > 0)
            {
                return -1;
            }

            if (comparer.Compare(x, y) < 0)
            {
                return 1;
            }

            return 0;
        }
    }

    public class NullPointVectorComparer<T> : IComparer<T>
    {
        private double VectorLength(Point a)
        {
            return Math.Round(Math.Sqrt((a.x * a.x) + (a.y * a.y)), 1);
        }

        public int Compare(T x, T y)
        {
            var a = (Point)(object)x;
            var b = (Point)(object)y;

            if (VectorLength(a) > VectorLength(b))
            {
                return 1;
            }

            if (VectorLength(a) < VectorLength(b))
            {
                return -1;
            }

            return 0;
        }
    }
}
