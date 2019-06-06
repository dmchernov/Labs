namespace Izh_04_tasks
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FilterDigitTestsMSTest
    {
        private TestContext testContext;

        public TestContext TestContext
        {
            get { return this.testContext; }
            set { this.testContext = value; }
        }

        public static int[] LineToArray(string line)
        {
            List<int> list = new List<int>();
            foreach (var item in line.Split(' '))
            {
                list.Add(int.Parse(item));
            }

            return list.ToArray();
        }

        [TestMethod]
        [DeploymentItem("Tests\\FilterDataDDT.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\FilterDataDDT.csv", "FilterDataDDT#csv", DataAccessMethod.Sequential)]
        public void FilterDigitTestMSTest()
        {
            int[] inputArray = LineToArray(this.TestContext.DataRow["array"].ToString());
            int[] expectedArray = LineToArray(this.TestContext.DataRow["result"].ToString());

            int[] resultArray = FilterDigitClass.FilterDigit(inputArray, int.Parse(this.TestContext.DataRow["digit"].ToString()));

            Assert.IsTrue(FilterDigitClass.Compare(resultArray, expectedArray));
        }
    }
}
