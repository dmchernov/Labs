using NUnit.Framework;
using System;

namespace Izh_05_tasks.Test
{
    [TestFixture]
    public class NthRootTestsNUnit
    {
        [TestCase(1, 5, 0.0001, ExpectedResult = 1)]
        [TestCase(8, 3, 0.0001, ExpectedResult = 2)]
        [TestCase(0.001, 3, 0.0001, ExpectedResult = 0.1)]
        [TestCase(0.04100625, 4, 0.0001, ExpectedResult = 0.45)]
        [TestCase(0.0279936, 7, 0.0001, ExpectedResult = 0.6)]
        [TestCase(0.0081, 4, 0.1, ExpectedResult = 0.3)]
        [TestCase(-0.008, 3, 0.1, ExpectedResult = -0.2)]
        [TestCase(0.004241979, 9, 0.00000001, ExpectedResult = 0.545)]
        public decimal FindNthRootTestMethod(double number, double power, double accuracy)
        {
            return NthRootClass.FindNthRoot(number, power, accuracy);
        }

        [TestCase(-0.01, 2, 0.0001)]
        [TestCase(0.01, 2, -1)]
        public void FindNthRoot_Number_Degree_Precision_ArgumentOutOfRangeException(double number, double power, double accuracy)
        {
            Assert.That(() => NthRootClass.FindNthRoot(number, power, accuracy), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
