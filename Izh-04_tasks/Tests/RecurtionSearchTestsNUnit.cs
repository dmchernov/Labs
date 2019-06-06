namespace Izh_04_tasks
{
    using NUnit.Framework;

    [TestFixture]
    public class RecurtionSearchTestsNUnit
    {
        [Test]
        [TestCase(new int[] { 15, 17, 0, 3, 255 }, ExpectedResult = 255)]
        [TestCase(new int[] { 15, -3457, 0, 31678, 255, 5, -367 }, ExpectedResult = 31678)]
        [TestCase(new int[] { 1, 1, 1, 1, 1, 1, 1 }, ExpectedResult = 1)]
        [TestCase(new int[] { 0, -1, 11, 21, 1, 2, 8, 65, 34, 21, 765, -12, 566, 7878, -199, 0, 34, 65, 87, 12, 34 }, ExpectedResult = 7878)]
        public int RecurtionSearchTest(int[] arr)
        {
            return RecurtionSearch.Max(arr);
        }
    }
}
