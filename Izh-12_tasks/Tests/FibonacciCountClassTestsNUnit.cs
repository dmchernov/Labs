using NUnit.Framework;
using Izh_12_tasks.Task_3;
using System;

namespace Izh_12_tasks.Tests
{
    [TestFixture]
    public class FibonacciCountClassTestsNUnit
    {
        [TestCase(new int[] { 0, 1, 1, 2, 3, 5, 8, 12 }, ExpectedResult = 8)]
        [TestCase(new int[] { 0, 1, 1, 2 }, ExpectedResult = 4)]
        public int FibonacciCount_Test(int[] sequence)
        {
            return FibonacciCountClass.FibonacciCount(sequence);
        }

        [TestCase(null)]
        public void FibonacciCount_NullReferenceExceptiopn_Test(int[] sequence)
        {
            Assert.That(() => FibonacciCountClass.FibonacciCount(sequence), Throws.TypeOf<NullReferenceException>());
        }
    }
}
