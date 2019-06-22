using NUnit.Framework;
using Izh_09_tasks.Task_3;

namespace Izh_09_tasks.Tests
{
    [TestFixture]
    public class UrlParameterClassTestsNUnit
    {
        [TestCase("www.example.com", "key=value", ExpectedResult = "www.example.com?key=value")]
        [TestCase("www.example.com?", "key=value", ExpectedResult = "www.example.com?key=value")]
        [TestCase("www.example.com?key=value", "key2=value2", ExpectedResult = "www.example.com?key=value&key2=value2")]
        [TestCase("www.example.com?key=oldValue", "key=newValue", ExpectedResult = "www.example.com?key=newValue")]
        public string AddOrChangeUrlParameter_Test(string url, string keyValue)
        {
            return UrlParameterClass.AddOrChangeUrlParameter(url, keyValue);
        }
    }
}
