using NUnit.Framework;

namespace Izh_06_tasks.Tests
{
    [TestFixture]
    public class NullableTests
    {
        [Test]
        public void NullableExtension_NullableInt_Test()
        {
            int? i = null;
            Assert.IsTrue(i.IsNull<int?>() == true);
        }

        [Test]
        public void NullableExtension_FilledString_Test()
        {
            string name = "Kathy";
            Assert.IsTrue(name.IsNull<string>() == false);
        }

        [Test]
        public void NullableExtension_NullString_Test()
        {
            string name = null;
            Assert.IsTrue(name.IsNull<string>() == true);
        }
    }
}
