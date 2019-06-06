namespace Izh_04_tasks.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ConcatinationTestsNUnit
    {
        [TestCase("AsdfeAd", "Assqaasssqs", ExpectedResult = "AsdfeAdqaaq")]
        public string ConctinationTestNUnite(string firstString, string secondString)
        {
            return Concatination.Concat(firstString, secondString);
        }
    }
}
