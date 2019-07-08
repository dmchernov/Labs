using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izh_13_tasks
{
    /// <summary>
    /// Поле, по которому сотрировать.
    /// </summary>
    public enum SortField
    {
        StudentName = 1,
        TestName,
        Date,
        Assessment,
        Default
    }

    /// <summary>
    /// Порядок сортировки.
    /// </summary>
    public enum SortDirection
    {
        Ascending = 1,
        Descending
    }
}
