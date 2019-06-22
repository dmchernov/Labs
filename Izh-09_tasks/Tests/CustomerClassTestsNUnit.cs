using NUnit.Framework;
using Izh_09_tasks.Task_1;

namespace Izh_09_tasks.Tests
{
    [TestFixture]
    class CustomerClassTestsNUnit
    {
        [SetCulture("en-us")]
        [TestCase("Jeffrey Richter", "+1 (425) 555-0100", 1000000, ToStringFormat.Name, ExpectedResult = "Jeffrey Richter")]
        [TestCase("Jeffrey Richter", "+1 (425) 555-0100", 1000000, ToStringFormat.Name | ToStringFormat.Phone, ExpectedResult = "Jeffrey Richter, +1 (425) 555-0100")]
        [TestCase("Jeffrey Richter", "+1 (425) 555-0100", 1000000, ToStringFormat.Phone, ExpectedResult = "+1 (425) 555-0100")]
        [TestCase("Jeffrey Richter", "+1 (425) 555-0100", 1000000, ToStringFormat.Revenue | ToStringFormat.Phone, ExpectedResult = "+1 (425) 555-0100, 1,000,000.00")]
        [TestCase("Jeffrey Richter", "+1 (425) 555-0100", 1000000, ToStringFormat.All, ExpectedResult = "Jeffrey Richter, +1 (425) 555-0100, 1,000,000.00")]
        public string Customer_ToString_Test(string name, string phone, decimal revenue, ToStringFormat format)
        {
            Customer customer = new Customer(name, phone, revenue);
            return customer.ToString(format);
        }

        [SetCulture("en-us")]
        [TestCase("Jeffrey Richter", "+1 (425) 555-0100", 1000000, ExpectedResult = "jEFFREY rICHTER")]
        public string Customer_FormatProvider_Test(string name, string phone, decimal revenue)
        {
            WordFormatProvider wfp = new WordFormatProvider();
            Customer customer = new Customer(name, phone, revenue);
            return string.Format(wfp, "{0:W}", customer.Name);
        }
    }
}
