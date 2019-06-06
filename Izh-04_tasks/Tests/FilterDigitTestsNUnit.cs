namespace Izh_04_tasks
{
    using NUnit.Framework;

    [TestFixture]
    public class FilterDigitTestsNUnit
    {
        private static object[] testData =
        {
            new object[] { new int[] { 13, 3, 300, 0, 45, 32 }, 3, new int[] { 13, 3, 300, 32 } }
        };

        private TestContext testContext;

        public TestContext TestContext
        {
            get { return this.testContext; }
            set { this.testContext = value; }
        }

        [TestCaseSource("testData")]
        public void FilterDigitTestNUnit(int[] inputArray, int digit, int[] expectedArray)
        {
            int[] resultArray = FilterDigitClass.FilterDigit(inputArray, digit);
            Assert.IsTrue(FilterDigitClass.Compare(resultArray, expectedArray));
        }

        [TestCase(new int[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 }, 7, ExpectedResult = new int[] { 7, 7, 70, 17 })]
        [TestCase(new int[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, -69, 70, 15, 17 }, 6, ExpectedResult = new int[] { 6, 68, -69 })]
        [TestCase(new int[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, -69, 70, 15, 17 }, 9, ExpectedResult = new int[] { -69 })]
        public int[] FilterDigitTestNUnit1(int[] inputArray, int digit)
        {
            return FilterDigitClass.FilterDigit(inputArray, digit);
        }
    }
}
