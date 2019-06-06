using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Izh_04_tasks
{
    [TestClass]
    public class InsertNumberTestsMSTest
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow(15, 15, 0, 0, 15)]
        [DataRow(8, 15, 0, 0, 9)]
        [DataRow(8, 15, 3, 8, 120)]
        public void InsertNumberTestMSTest(int a, int b, int c, int d, int result)
        {
            Assert.AreEqual(result, InsertNumberClass.InsertNumber(a, b, c, d));
        }
    }
}
