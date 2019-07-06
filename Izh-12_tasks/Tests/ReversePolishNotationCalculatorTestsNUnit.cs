using System;
using Izh_12_tasks.Task_8;
using NUnit.Framework;

namespace Izh_12_tasks.Tests
{
    [SetCulture("en-us")]
    [TestFixture]
    public class ReversePolishNotationCalculatorTestsNUnit
    {
        [TestCase("5 1 2 + 4 * + 3 -", ExpectedResult = 14.0)] // 5 + ((1 + 2) * 4) - 3
        [TestCase("56 17.3 1 33 + * 3 / -", ExpectedResult = -140.1)] // 56 - 17,3 * (1 + 33) / 3
        [TestCase("56 1 33 + 17.3 * 3 / -", ExpectedResult = -140.1)] // 56 - 17,3 * (1 + 33) / 3
        [TestCase("56 1 33 + 17.3 *  3 / -", ExpectedResult = -140.1)] // 56 - 17,3 * (1 + 33) / 3
        [TestCase("", ExpectedResult = 0.0)]
        public double ReversePolishNotationCalculator_Test(string expression)
        {
            return Calculator.Calculate(expression);
        }

        [TestCase("56 102 + 36 * 5 2.5 2 * - /")] // (56 + 102) * 36 / (5 - 2.5 * 2)
        public void ReversePolishNotationCalculator_DivideByZeroException_Test(string expession)
        {
            Assert.That(() => Calculator.Calculate(expession), Throws.TypeOf<DivideByZeroException>());
        }

        [TestCase("56 102 + 36* 5 2.5 2 * - /")]
        [TestCase("56 102 + 36 * 5 2.5 2 * - / +")]
        [TestCase("56 102 + 36 * 5 2.5 2 * - //")]
        public void ReversePolishNotationCalculator_Exception_Test(string expession)
        {
            Assert.That(() => Calculator.Calculate(expession), Throws.TypeOf<ArgumentException>());
        }
    }
}
