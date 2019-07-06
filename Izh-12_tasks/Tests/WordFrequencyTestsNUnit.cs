using NUnit.Framework;
using Izh_12_tasks.Task_2;
using System.IO;
using System.Collections.Generic;

namespace Izh_12_tasks.Tests
{
    [TestFixture]
    public class WordFrequencyTestsNUnit
    {
        [TestCase("WordFrequency_TestText.txt", "value", ExpectedResult = 9)]
        [TestCase("WordFrequency_TestText.txt", "returned", ExpectedResult = 3)]
        [TestCase("WordFrequency_TestText.txt", "return", ExpectedResult = 1)]
        [TestCase("WordFrequency_TestText_RuEn.txt", "если", ExpectedResult = 5)]
        [TestCase("WordFrequency_TestText_RuEn.txt", "value", ExpectedResult = 8)]
        [TestCase("WordFrequency_TestText_RuEn.txt", "индекса", ExpectedResult = 2)]
        public int WordFrequency_Test(string filename, string word)
        {
            string text = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory,"Tests", filename));
            Dictionary<string, int> resultDict = WordFrequencyClass.WordFrequencyCalculation(text);
            return resultDict[word];
        }

        [TestCase("WordFrequency_TestText_BlankStringsOnly.txt")]
        [TestCase("WordFrequency_TestText_Empty.txt")]
        public void WordFrequency_Null_Test(string filename)
        {
            string text = File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, "Tests", filename));
            Dictionary<string, int> resultDict = WordFrequencyClass.WordFrequencyCalculation(text);
            Assert.IsNull(resultDict);
        }
    }
}
