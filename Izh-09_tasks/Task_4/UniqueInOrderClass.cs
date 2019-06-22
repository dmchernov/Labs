using System.Collections.Generic;
using System.Collections;

namespace Izh_09_tasks.Task_4
{
    public class UniqueInOrderClass
    {
        public static List<object> UniqueInOrder(IEnumerable sequence)
        {
            object last = null;
            List<object> data = new List<object>();

            foreach (var item in sequence)
            {
                if (last == null || !item.Equals(last))
                {
                    last = item;
                    data.Add(item);
                }
            }

            return data;
        }
    }
}
