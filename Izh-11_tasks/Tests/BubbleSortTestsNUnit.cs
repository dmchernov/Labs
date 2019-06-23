using NUnit.Framework;
using System;
using Izh_11_tasks.Task_2;

namespace Izh_11_tasks
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
                SortDirection.Descending,
            },
            new object[] {
                new int[3, 3] { { 1, 3, 5 }, { 2, 3, -1}, { 0, 7, 1} },
                new int[3, 3] { { 2, 3, -1}, { 0, 7, 1 }, { 1, 3, 5} },
                SortType.MinElement,
                SortDirection.Ascending,
            },
            new object[] {
                new int[3, 3] { { 1, 3, 5 }, { 2, 3, -1}, { 0, 7, 1 } },
                new int[3, 3] { { 2, 3, -1}, { 1, 3, 5 }, { 0, 7, 1 } },
                SortType.MaxElement,
                SortDirection.Ascending,
            },
        };

        private static object[] testDataException =
        {
            new object[] {
                null,
                new int[2, 2] { { 2, 3 }, { 1, 3 } },
                SortType.SumOfElements,
                SortDirection.Descending,
            },
        };

        [TestCaseSource("testData")]
        public void BubbleSortTest(int[,] matrix, int[,] expectedResult, SortType sortType, SortDirection sortDirection)
        {
            int[,] result = BubbleSortClass.Sort(matrix, sortType, sortDirection);
            Assert.IsTrue(BubbleSortClass.Compare(result, expectedResult));
        }

        [TestCaseSource("testDataException")]
        public void BubbleSortTest_ArgumentExeption(int[,] matrix, int[,] expectedResult, SortType sortType, SortDirection sortDirection)
        {
            Assert.That(() => BubbleSortClass.Sort(matrix, sortType, sortDirection), Throws.TypeOf<ArgumentException>());
        }


        private static object[] delegateAscTestData =
        {
            new object[] {
                new int[3, 3] { { 1, 3, 5 }, { 2, 3, -1}, { 0, 7, 1} },
                new int[3, 3] { { 2, 3, -1}, { 0, 7, 1 }, { 1, 3, 5} },
                SortType.MinElement,
            },
            new object[] {
                new int[3, 3] { { 1, 3, 5 }, { 2, 3, -1}, { 0, 7, 1 } },
                new int[3, 3] { { 2, 3, -1}, { 1, 3, 5 }, { 0, 7, 1 } },
                SortType.MaxElement,
            },
        };

        private static object[] delegateDescTestData =
        {
            new object[] {
                new int[2, 2] { { 1, 3 }, { 2, 3 } },
                new int[2, 2] { { 2, 3 }, { 1, 3 } },
                SortType.SumOfElements,
            },
        };

        [TestCaseSource("delegateAscTestData")]
        public void BubbleSort_Delegate_Ascending_Test(int[,] matrix, int[,] expectedResult, SortType sortType)
        {
            int[,] result = BubbleSortClass.Sort(matrix, sortType, (x, y) => x > y);
            Assert.IsTrue(BubbleSortClass.Compare(result, expectedResult));
        }

        [TestCaseSource("delegateDescTestData")]
        public void BubbleSort_Delegate_Descending_Test(int[,] matrix, int[,] expectedResult, SortType sortType)
        {
            int[,] result = BubbleSortClass.Sort(matrix, sortType, (x, y) => x < y);
            Assert.IsTrue(BubbleSortClass.Compare(result, expectedResult));
        }
    }
}
