using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izh_13_tasks
{
    [Serializable]
    public class TestResult : IComparable
    {
        private int Id { get; set; }
        public string StudentName { get; private set; }
        public string TestName { get; private set; }
        public DateTime TestDate { get; private set; }
        public int Assessment { get; private set; }

        public TestResult(string studentName, string testName, DateTime testDate, int assessment, int id)
        {
            StudentName = studentName;
            TestName = testName;
            TestDate = testDate.Date;
            Assessment = assessment;
            this.Id = id;
        }

        public int CompareTo(object obj)
        {
            TestResult tr = (TestResult)obj;
            var comparer = Comparer<string>.Default;

            return
                Assessment > tr.Assessment ? 1 : Assessment < tr.Assessment ? -1 :
                TestDate > tr.TestDate ? 1 : TestDate < tr.TestDate ? -1 :
                comparer.Compare(TestName, tr.TestName) > 0 ? 1 : comparer.Compare(TestName, tr.TestName) < 0 ? -1 :
                comparer.Compare(StudentName, tr.StudentName) > 0 ? 1 : comparer.Compare(StudentName, tr.StudentName) < 0 ? -1 :
                Id > tr.Id ? 1 : Id < tr.Id ? -1 : 0;
        }
    }
}
