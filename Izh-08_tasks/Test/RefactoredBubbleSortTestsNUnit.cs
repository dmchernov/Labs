using NUnit.Framework;
using System;

namespace Izh_08_tasks
{
    [TestFixture]
    public class BubbleSortTestsNUnit
    {

        private static object[] testData =
        {
            new object[] {
                new int[2, 2] { { 1, 3 }, { 2, 3 } },
                new int[2, 2] { { 2, 3 }, { 1, 3 } },
                SortType.SumOfElements,
                "LowerThan",
            },
            new object[] {
                new int[3, 3] { { 1, 3, 5 }, { 2, 3, -1}, { 0, 7, 1} },
                new int[3, 3] { { 2, 3, -1}, { 0, 7, 1 }, { 1, 3, 5} },
                SortType.MinElement,
                "GreaterThan",
            },
            new object[] {
                new int[3, 3] { { 1, 3, 5 }, { 2, 3, -1}, { 0, 7, 1 } },
                new int[3, 3] { { 2, 3, -1}, { 1, 3, 5 }, { 0, 7, 1 } },
                SortType.MaxElement,
                "GreaterThan",
            },
        };

        private static object[] testDataException =
        {
            new object[] {
                null,
                new int[2, 2] { { 2, 3 }, { 1, 3 } },
                SortType.SumOfElements,
                "GreaterThan",
            },
        };

        [TestCaseSource("testData")]
        public void RefactoredBubbleSortTest(int[,] matrix, int[,] expectedResult, SortType sortType, string compareCriterionName)
        {
            int[,] result = RefactoredBubbleSortClass.Sort(matrix, sortType, compareCriterionName);
            Assert.IsTrue(RefactoredBubbleSortClass.Compare(result, expectedResult));
        }

        [TestCaseSource("testDataException")]
        public void RefactoredBubbleSortTest_ArgumentExeption(int[,] matrix, int[,] expectedResult, SortType sortType, string compareCriterionName)
        {
            Assert.That(() => RefactoredBubbleSortClass.Sort(matrix, sortType, compareCriterionName), Throws.TypeOf<ArgumentException>());
        }
    }
}
