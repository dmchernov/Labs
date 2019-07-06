using System;
using NUnit.Framework;
using Izh_12_tasks.Task_5;
using System.Collections;
using System.Collections.Generic;

namespace Izh_12_tasks.Tests
{
    [TestFixture]
    public class StackClassTestsNUnit
    {
        CustomStack<int> stack;

        [SetUp]
        public void TestBefore()
        {
            stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
        }

        [TearDown]
        public void TearDown()
        {
            stack = null;
        }

        [TestCase]
        public void Stack_Push_Test()
        {
            stack.Push(15);
            Assert.IsTrue(stack.Count == 4);
            Assert.IsTrue(stack[3] == 15);
        }

        [TestCase]
        public void Stack_Pop_Test()
        {
            Assert.AreEqual(3, stack.Pop());
            Assert.IsTrue(stack.Count == 2);
        }

        [TestCase]
        public void Stack_Pop_Exception_Test()
        {
            stack.Pop ();
            stack.Pop();
            stack.Pop();
            Assert.That(() => stack.Pop(), Throws.TypeOf<InvalidOperationException>());
        }

        [TestCase]
        public void Stack_Peek_Exception_Test()
        {
            stack.Pop();
            stack.Pop();
            stack.Pop();
            Assert.That(() => stack.Peek(), Throws.TypeOf<InvalidOperationException>());
        }

        [TestCase]
        public void Stack_Peek_Test()
        {
            Assert.AreEqual(3, stack.Peek());
            Assert.IsTrue(stack.Count == 3);
        }

        [TestCase]
        public void Stack_Clear_Test()
        {
            stack.Clear();
            Assert.IsTrue(stack.Count == 0);
        }

        [TestCase]
        public void Stack_Foreach_Test()
        {
            var iterator = stack.GetEnumerator();
            iterator.MoveNext();
            Assert.AreEqual(3, iterator.Current);
            iterator.MoveNext();
            Assert.AreEqual(2, iterator.Current);
            iterator.MoveNext();
            Assert.AreEqual(1, iterator.Current);
            Assert.IsTrue(stack.Count == 3);
        }

        [TestCase]
        public void Stack_Contains_Test()
        {
            Assert.IsTrue(stack.Contains(2) == true);
        }

        [TestCase]
        public void Stack_CopyTo_ArgumentException_Test()
        {
            int[] intArray = new int[2];
            Assert.That(() => stack.CopyTo(intArray, 1), Throws.TypeOf<ArgumentException>());
        }

        [TestCase]
        public void Stack_CopyTo_ArgumentNullException_Test()
        {
            int[] intArray = null;
            Assert.That(() => stack.CopyTo(intArray, 1), Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase]
        public void Stack_CopyTo_IndexOutOfRangeException_Test()
        {
            int[] intArray = new int[7];
            Assert.That(() => stack.CopyTo(intArray, -1), Throws.TypeOf<IndexOutOfRangeException>());
        }

        [TestCase(new int[] { 11, 12, 1, 2, 3 })]
        public void Stack_CopyTo_Test(int[] expectedResult)
        {
            int[] intArray = new int[5];
            intArray[0] = 11;
            intArray[1] = 12;
            stack.CopyTo(intArray, 2);
            IStructuralEquatable comparer = intArray;
            Assert.IsTrue(comparer.Equals(expectedResult, EqualityComparer<int>.Default));
        }

        [TestCase(ExpectedResult = 2)]
        public int Stack_GetIndexer_Test()
        {
            return stack[1];
        }

        [TestCase]
        public void Stack_SetIndexer_Test()
        {
            stack[1] = 300;
            Assert.AreEqual(300, stack[1]);
        }
    }
}
