namespace Izh_04_tasks
{
    using NUnit.Framework;

    [TestFixture]
    public class BiggestNumberTestsNUnit
    {
        [Test]
        [TestCase(12, ExpectedResult = 21)]
        [TestCase(513, ExpectedResult = 531)]
        [TestCase(2017, ExpectedResult = 2071)]
        [TestCase(414, ExpectedResult = 441)]
        [TestCase(144, ExpectedResult = 414)]
        [TestCase(1234321, ExpectedResult = 1241233)]
        [TestCase(1234126, ExpectedResult = 1234162)]
        [TestCase(3456432, ExpectedResult = 3462345)]
        [TestCase(10, ExpectedResult = -1)]
        [TestCase(20, ExpectedResult = -1)]
        [TestCase(-123, ExpectedResult = -1)]
        public int FindNextBiggerNumberTest(int a)
        {
            return BiggestNumber.FindNextBiggerNumber(a);
        }


        [Test]
        [TestCase(12, ExpectedResult = 21)]
        [TestCase(513, ExpectedResult = 531)]
        [TestCase(2017, ExpectedResult = 2071)]
        [TestCase(414, ExpectedResult = 441)]
        [TestCase(144, ExpectedResult = 414)]
        [TestCase(1234321, ExpectedResult = 1241233)]
        [TestCase(1234126, ExpectedResult = 1234162)]
        [TestCase(3456432, ExpectedResult = 3462345)]
        [TestCase(10, ExpectedResult = -1)]
        [TestCase(20, ExpectedResult = -1)]
        [TestCase(-123, ExpectedResult = -1)]
        public int FindNextBiggerNumberDurationTest(int a)
        {
            long ms = -1;
            int result = BiggestNumber.FindNextBiggerNumber(a, out ms);
            Assert.That(ms >= 0);
            return result;
        }
    }
}