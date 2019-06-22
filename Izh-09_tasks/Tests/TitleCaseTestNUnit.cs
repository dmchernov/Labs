using NUnit.Framework;
using Izh_09_tasks.Task_2;

namespace Izh_09_tasks.Tests
{
    [TestFixture]
    public class TitleCaseTestNUnit
    {
        [TestCase("a clash of KINGS", "a an the of", ExpectedResult = "A Clash of Kings")]
        [TestCase("THE WIND IN THE WILLOWS", "The In", ExpectedResult = "The Wind in the Willows")]
        public string TitleCase_Two_Params_Test(string convert, string minor)
        {
            return TitleCaseClass.TitleCase(convert, minor);
        }

        [TestCase("the quick brown fox", ExpectedResult = "The Quick Brown Fox")]
        public string TitleCase_One_Param_Test(string convert)
        {
            return TitleCaseClass.TitleCase(convert);
        }
    }
}
