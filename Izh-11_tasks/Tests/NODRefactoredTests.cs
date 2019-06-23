using NUnit.Framework;
using System;
using Izh_11_tasks.Task_1;

namespace Izh_11_tasks.Tests
{
    [TestFixture]
    public class NODRefactoredTests
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
        public int FindNODRefactoredTest(FindNODType findType, int a, int b)
        {
            return NODCalculatesRefactored.CalculateNOD(findType, a, b);
        }

        [TestCase(FindNODType.Stein, -12, 1)]
        [TestCase(FindNODType.Stein, 6, -3)]
        public void FindNODRefactoredTest_ArgumentOutOfRangeException(FindNODType findType, int a, int b)
        {
            Assert.That(() => NODCalculatesRefactored.CalculateNOD(findType, a, b), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [TestCase(FindNODType.Stein, 12, 8, 52, 16, ExpectedResult = 4)]
        [TestCase(FindNODType.Stein, 78, 294, 570, 36, ExpectedResult = 6)]
        [TestCase(FindNODType.Stein, 78, 294, 570, 36, 4, ExpectedResult = 2)]
        [TestCase(FindNODType.Stein, 78, 294, 570, 36, 11, ExpectedResult = 1)]
        public int FindNODRefactored_Params_Test(FindNODType findType, params int[] digits)
        {
            return NODCalculatesRefactored.CalculateNOD(findType, digits);
        }

        [TestCase(78, 294, 570, 36, 11)]
        public void FindNODRefactoredBenchmarkTest(params int[] digits)
        {
            long timing = -1;
            NODCalculatesRefactored.FuncBenchmarkMilliSeconds(() => { System.Threading.Thread.Sleep(1000); return NODCalculatesRefactored.CalculateNOD(FindNODType.Euclid, digits); }, out timing);
            Assert.That(timing >= 1000);
        }

        [TestCase(78, 294, 570, 36, 11)]
        public void FindNODRefactoredBenchmarkUTCTest(params int[] digits)
        {
            long timing = -1;
            NODCalculatesRefactored.FuncBenchmarkUTCTicks(() => { System.Threading.Thread.Sleep(10); return NODCalculatesRefactored.CalculateNOD(FindNODType.Stein, digits); }, out timing);
            Assert.That(timing >= 0);
        }
    }
}
