using NUnit.Framework;
using Izh_12_tasks.Task_4;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Izh_12_tasks.Tests
{
    [TestFixture]
    public class QueueClassTestsNUnit
    {
        CustomQueue<string> queue;

        [SetUp]
        public void TestBefore()
        {
            queue = new CustomQueue<string>();
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("last");
        }

        [TearDown]
        public void TearDown()
        {
            queue = null;
        }

        [TestCase]
        public void Queue_Enqueue_Test()
        {
            queue.Enqueue("added");
            Assert.IsTrue(queue.Count == 4);
            Assert.IsTrue(queue[3] == "added");
        }

        [TestCase]
        public void Queue_Dequeue_Test()
        {
            Assert.AreEqual("one", queue.Dequeue());
            Assert.IsTrue(queue.Count == 2);
        }

        [TestCase]
        public void Queue_Dequeue_Exception_Test()
        {
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            Assert.That(() => queue.Dequeue(), Throws.TypeOf<InvalidOperationException>());
        }

        [TestCase]
        public void Queue_Peek_Exception_Test()
        {
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            Assert.That(() => queue.Peek(), Throws.TypeOf<InvalidOperationException>());
        }

        [TestCase]
        public void Queue_Peek_Test()
        {
            Assert.AreEqual("one", queue.Peek());
            Assert.IsTrue(queue.Count == 3);
        }

        [TestCase]
        public void Queue_Clear_Test()
        {
            queue.Clear();
            Assert.IsTrue(queue.Count == 0);
        }

        [TestCase]
        public void Queue_Foreach_Test()
        {
            var iterator = queue.GetEnumerator();
            iterator.MoveNext();
            Assert.AreEqual("one", iterator.Current);
            iterator.MoveNext();
            Assert.AreEqual("two", iterator.Current);
            iterator.MoveNext();
            Assert.AreEqual("last", iterator.Current);
            Assert.IsTrue(queue.Count == 3);
        }

        [TestCase]
        public void Queue_Contains_Test()
        {
            Assert.IsTrue(queue.Contains("two") == true);
        }

        [TestCase]
        public void Queue_CopyTo_ArgumentException_Test()
        {
            string[] strArray = new string[2];
            Assert.That(() => queue.CopyTo(strArray, 1), Throws.TypeOf<ArgumentException>());
        }

        [TestCase]
        public void Queue_CopyTo_ArgumentNullException_Test()
        {
            string[] strArray = null;
            Assert.That(() => queue.CopyTo(strArray, 1), Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase]
        public void Queue_CopyTo_IndexOutOfRangeException_Test()
        {
            string[] strArray = new string[7];
            Assert.That(() => queue.CopyTo(strArray, -1), Throws.TypeOf<IndexOutOfRangeException>());
        }

        static object[] stringData = new object[] {
            new string[] { "initial 1", "initial 2", "one", "two", "last" }
        };

        [TestCaseSource("stringData")]
        public void Queue_CopyTo_Test(string[] expectedResult)
        {
            string[] strArray = new string[5];
            strArray[0] = "initial 1";
            strArray[1] = "initial 2";
            queue.CopyTo(strArray, 2);
            IStructuralEquatable comparer = strArray;
            Assert.IsTrue(comparer.Equals(expectedResult, EqualityComparer<string>.Default));
        }

        [TestCase(ExpectedResult = "two")]
        public string Queue_GetIndexer_Test()
        {
            return queue[1];
        }

        [TestCase]
        public void Queue_SetIndexer_Test()
        {
            queue[1] = "settled";
            Assert.AreEqual("settled", queue[1]);
        }
    }
}
