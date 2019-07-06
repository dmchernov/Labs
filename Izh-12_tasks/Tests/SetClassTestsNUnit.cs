using System;
using NUnit.Framework;
using Izh_12_tasks.Task_6;
using System.Collections;
using System.Collections.Generic;

namespace Izh_12_tasks.Tests
{
        [TestFixture]
    public class SetClassTestsNUnit
    {
        Set<string> set;

        [SetUp]
        public void TestBefore()
        {
            set = new Set<string>();
            set.Add("one");
            set.Add("two");
            set.Add("three");
        }

        [TearDown]
        public void TearDown()
        {
            set = null;
        }

        [TestCase]
        public void Set_AddExistingItem_Test()
        {
            Assert.IsFalse(set.Add("two"));
            Assert.IsTrue(set.Count == 3);
        }

        [TestCase]
        public void Set_Add_Test()
        {
            Assert.IsTrue(set.Add("four"));
            Assert.IsTrue(set.Count == 4);
        }

        [TestCase]
        public void Set_Remove_Test()
        {
            Assert.IsTrue(set.Remove("two"));
            Assert.IsTrue(set.Count == 2);
            Assert.IsFalse(set.Contains("two"));
        }

        [TestCase]
        public void Set_RemoveNotExistedItem_Test()
        {
            Assert.IsFalse(set.Remove("four"));
            Assert.IsTrue(set.Count == 3);
        }

        [TestCase]
        public void Set_Clear_Test()
        {
            set.Clear();
            Assert.IsTrue(set.Count == 0);
        }

        [TestCase]
        public void Set_Yield_Test()
        {
            string[] resultArray = new string[3];
            var iterator = set.Iterator();
            int i = 0;

            foreach (var item in iterator)
            {
                resultArray[i++] = item;
            }

            Assert.AreEqual("one", resultArray[0]);
            Assert.AreEqual("two", resultArray[1]);
            Assert.AreEqual("three", resultArray[2]);
            Assert.IsTrue(set.Count == 3);
        }

        [TestCase]
        public void Set_Contains_Test()
        {
            Assert.IsTrue(set.Contains("three"));
        }

        [TestCase]
        public void Set_CopyTo_ArgumentException_Test()
        {
            string[] strArray = new string[2];
            Assert.That(() => set.CopyTo(strArray, 1), Throws.TypeOf<ArgumentException>());
        }

        [TestCase]
        public void Set_CopyTo_ArgumentNullException_Test()
        {
            string[] strArray = null;
            Assert.That(() => set.CopyTo(strArray, 1), Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase]
        public void Set_CopyTo_IndexOutOfRangeException_Test()
        {
            string[] strArray = new string[7];
            Assert.That(() => set.CopyTo(strArray, -1), Throws.TypeOf<IndexOutOfRangeException>());
        }

        static object[] stringData = new object[] {
            new string[] { "initial 1", "initial 2", "one", "two", "three" }
        };

        [TestCaseSource("stringData")]
        public void Set_CopyTo_Test(string[] expectedResult)
        {
            string[] strArray = new string[5];
            strArray[0] = "initial 1";
            strArray[1] = "initial 2";
            set.CopyTo(strArray, 2);
            IStructuralEquatable comparer = strArray;
            Assert.IsTrue(comparer.Equals(expectedResult, EqualityComparer<string>.Default));
        }

        [TestCase(ExpectedResult = "two")]
        public string Set_GetIndexer_Test()
        {
            return set[1];
        }

        [TestCase]
        public void Set_SetIndexer_Test()
        {
            set[1] = "ten";
            Assert.AreEqual("ten", set[1]);
        }
    }
}
