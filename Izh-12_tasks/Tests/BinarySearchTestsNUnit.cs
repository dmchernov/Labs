using NUnit.Framework;
using System;
using Izh_12_tasks.Task_1;

namespace Izh_12_tasks.Tests
{
    [TestFixture]
    public class BinarySearchTestsNUnit
    {
        [TestCase(new int[] { 5, 3, 7, 2, 10, 6 }, 3, ExpectedResult = 1)]
        [TestCase(new int[] { 5, 3, 7, 2, 10, 6 }, 10, ExpectedResult = 5)]
        [TestCase(new int[] { 5, 3, 7, 2, 10, 6 }, 15, ExpectedResult = -7)]
        [TestCase(new int[] { 5, 3, 7, 2, 10, 6 }, 1, ExpectedResult = -1)]
        [TestCase(new int[] { -5, 3, -7, 2, 10, 6 }, -7, ExpectedResult = 0)]
        [TestCase(new int[] { -5, 3, 3, 2, 10, 6 }, 3, ExpectedResult = 2)]
        public int BinarySearch_Int_Test(int[] arr, int value)
        {
            Array.Sort(arr);
            return BinarySearchClass.BinarySearch<int>(arr, value);
        }

        [TestCase(new char[] { 'e', 'c', 'b', 'R', 'r', 'z', 'q' }, 'r', ExpectedResult = 5)]
        public int BinarySearch_Char_Test(char[] arr, char value)
        {
            Array.Sort(arr);
            return BinarySearchClass.BinarySearch<char>(arr, value);
        }

        [TestCase(new double[] { 13.2, 3.7, 21.75, 2.0, -4.5, 6.3 }, 3.7, ExpectedResult = 2)]
        [TestCase(new double[] { 13.2, 3.7, 21.75, 2.0, -4.5, 6.3 }, -6.0, ExpectedResult = -1)]
        [TestCase(new double[] { 13.2, 3.7, 21.75, 2.0, -4.5, 6.3 }, 22.0, ExpectedResult = -7)]
        public int BinarySearch_Double_Test(double[] arr, double value)
        {
            Array.Sort(arr);
            return BinarySearchClass.BinarySearch<double>(arr, value);
        }

        [TestCase(new int[] { }, -7)]
        public void BinarySearch_ArgumentException_Test(int[] arr, int value)
        {
            Array.Sort(arr);
            Assert.That(() => BinarySearchClass.BinarySearch<int>(arr, value), Throws.TypeOf<ArgumentException>());
        }

        [TestCase(null, -7)]
        public void BinarySearch_ArgumentNullException_Test(int[] arr, int value)
        {
            Assert.That(() => BinarySearchClass.BinarySearch<int>(arr, value), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
