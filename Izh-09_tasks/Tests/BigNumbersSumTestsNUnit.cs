using NUnit.Framework;
using Izh_09_tasks.Task_6;
using System;

namespace Izh_09_tasks.Tests
{
    [TestFixture]
    public class BigNumbersSumTestsNUnit
    {
        [TestCase("123456789012345678901234567890", "98765432109876543210987654321",  ExpectedResult = "222222221122222222112222222211")]
        [TestCase("12345", "56789", ExpectedResult = "69134")]
        public string BigNumbersSum_Test(string num1, string num2)
        {
            return BigNumbersSumClass.BugNumbersSum(num1, num2);
        }

        [TestCase("--12345", "56789")]
        public void BigNumbersSum_Exception_Test(string num1, string num2)
        {
            Assert.That(() => BigNumbersSumClass.BugNumbersSum(num1, num2), Throws.TypeOf<ArithmeticException>());
        }
    }
}
