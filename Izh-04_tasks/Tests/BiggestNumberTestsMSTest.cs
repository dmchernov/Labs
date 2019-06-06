namespace Izh_04_tasks
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BiggestNumberTestsMSTest
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow(12, 21)]
        [DataRow(513, 531)]
        [DataRow(2017, 2071)]
        [DataRow(414, 441)]
        [DataRow(144, 414)]
        [DataRow(1234321, 1241233)]
        [DataRow(1234126, 1234162)]
        [DataRow(3456432, 3462345)]
        [DataRow(10, -1)]
        [DataRow(20, -1)]
        [DataRow(-123, -1)]
        public void FindNextBiggerNumberTestMSTest(int a, int result)
        {
            Assert.AreEqual(result, BiggestNumber.FindNextBiggerNumber(a));
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(12, 21)]
        [DataRow(513, 531)]
        [DataRow(2017, 2071)]
        [DataRow(414, 441)]
        [DataRow(144, 414)]
        [DataRow(1234321, 1241233)]
        [DataRow(1234126, 1234162)]
        [DataRow(3456432, 3462345)]
        [DataRow(10, -1)]
        [DataRow(20, -1)]
        [DataRow(-123, -1)]
        public void FindNextBiggerNumberDurationTestMSTest(int a, int expectedResult)
        {
            long ms = -1;
            int result = BiggestNumber.FindNextBiggerNumber(a, out ms);
            Assert.IsTrue(ms >= 0);
            Assert.AreEqual(result, expectedResult);
        }
    }
}
