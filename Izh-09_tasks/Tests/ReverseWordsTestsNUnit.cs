using NUnit.Framework;
using Izh_09_tasks.Task_5;

namespace Izh_09_tasks.Tests
{
    [TestFixture]
    public class ReverseWordsTestsNUnit
    {
        [TestCase("The greatest victory is that which requires no battle", ExpectedResult = "battle no requires which that is victory greatest The")]
        public string ReverseWords_Test(string input)
        {
            return ReverseWordsClass.ReverseWords(input);
        }
    }
}
