using NUnit.Framework;
using System.Collections;
using Izh_09_tasks.Task_4;
using System.Collections.Generic;

namespace Izh_09_tasks.Tests
{
    [TestFixture]
    public class UniqueInOrderTestNUnit
    {
        static object[] UniqueSeqTestData = new object[]
        {
            new object[] { "AAAABBBCCDAABBB" , new List<object>() { 'A', 'B', 'C', 'D', 'A', 'B' } },
            new object[] { "ABBCcAD", new List<object>() { 'A', 'B', 'C', 'c', 'A', 'D' } },
            new object[] { "12233", new List<object>() { '1', '2', '3' } },
            new object[] { new List<double> { 1.1, 2.2, 2.2, 3.3 }, new List<double> {1.1, 2.2, 3.3} },
        };

        [TestCaseSource("UniqueSeqTestData")]
        public void UniqueInOrder_StringSequence_Test(IEnumerable sequence, IEnumerable expectedresult)
        {
            List<object> result = UniqueInOrderClass.UniqueInOrder(sequence);
            Assert.AreEqual(result, expectedresult);
        }
    }
}
