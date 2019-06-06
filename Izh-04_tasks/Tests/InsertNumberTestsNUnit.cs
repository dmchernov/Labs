namespace Izh_04_tasks
{
    using NUnit.Framework;

    [TestFixture]
    public class InsertNumberTestsNUnit
    {
        [Test]
        [TestCase(15, 15, 0, 0, ExpectedResult = 15)]
        [TestCase(8, 15, 0, 0, ExpectedResult = 9)]
        [TestCase(8, 15, 3, 8, ExpectedResult = 120)]
        public int InsertNumberTest(int a, int b, int c, int d)
        {
            return InsertNumberClass.InsertNumber(a, b, c, d);
        }
    }
}
