using NUnit.Framework;
using System;

namespace Izh_06_tasks.Tests
{
    [TestFixture]
    public class NODTests
    {
        [TestCase(FindNODType.Euclid, 12, 6, ExpectedResult = 6)]
        [TestCase(FindNODType.Euclid, 9, 12, ExpectedResult = 3)]
        [TestCase(FindNODType.Euclid, 12, 12, ExpectedResult = 12)]
        [TestCase(FindNODType.Euclid, 12, 1, ExpectedResult = 1)]
        [TestCase(FindNODType.Euclid, 12, 1, ExpectedResult = 1)]
        [TestCase(FindNODType.Euclid, 0, 0, ExpectedResult = 0)]
        [TestCase(FindNODType.Euclid, 12, 0, ExpectedResult = 12)]
        [TestCase(FindNODType.Euclid, -12, 1, ExpectedResult = 1)]
        [TestCase(FindNODType.Euclid, 6, -3, ExpectedResult = 3)]
        [TestCase(FindNODType.Stein, 12, 6, ExpectedResult = 6)]
        [TestCase(FindNODType.Stein, 9, 12, ExpectedResult = 3)]
        [TestCase(FindNODType.Stein, 12, 12, ExpectedResult = 12)]
        [TestCase(FindNODType.Stein, 12, 1, ExpectedResult = 1)]
        [TestCase(FindNODType.Stein, 12, 1, ExpectedResult = 1)]
        [TestCase(FindNODType.Stein, 0, 0, ExpectedResult = 0)]
        [TestCase(FindNODType.Stein, 12, 0, ExpectedResult = 12)]
        public int FindNODTest(FindNODType findType, int a, int b)
        {
            return NODCalculates.CalculateNOD(findType, a, b);
        }

        [TestCase(FindNODType.Stein, -12, 1)]
        [TestCase(FindNODType.Stein, 6, -3)]
        public void FinfNODTest_ArgumentOutOfRangeException(FindNODType findType, int a, int b)
        {
            Assert.That(() => NODCalculates.CalculateNOD(findType, a, b), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [TestCase(FindNODType.Stein, 12, 8, 52, 16, ExpectedResult = 4)]
        [TestCase(FindNODType.Stein, 78, 294, 570, 36, ExpectedResult = 6)]
        [TestCase(FindNODType.Stein, 78, 294, 570, 36, 4, ExpectedResult = 2)]
        [TestCase(FindNODType.Stein, 78, 294, 570, 36, 11, ExpectedResult = 1)]
        public int FindNODTest(FindNODType findType, params int[] digits)
        {
            return NODCalculates.CalculateNOD(findType, digits);
        }

        [TestCase(78, 294, 570, 36, 11)]
        public void FindNODBenchmarkTest(params int[] digits)
        {
            long timing = -1;
            NODCalculates.FuncBenchmarkMilliSeconds(() => { System.Threading.Thread.Sleep(1000); return NODCalculates.CalculateNOD(FindNODType.Euclid, digits); }, out timing);
            Assert.That(timing >= 1000);
        }

        [TestCase(78, 294, 570, 36, 11)]
        public void FindNODBenchmarkUTCTest(params int[] digits)
        {
            long timing = -1;
            NODCalculates.FuncBenchmarkUTCTicks(() => { System.Threading.Thread.Sleep(10); return NODCalculates.CalculateNOD(FindNODType.Stein, digits); }, out timing);
            Assert.That(timing >= 0);
        }
    }
}
