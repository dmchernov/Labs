using System;
using Izh_12_tasks.Task_7;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Izh_12_tasks.Tests
{
    [TestFixture]
    public class BinarySearchTreeTestsNUnit
    {
        BinarySearchTree<string> traverseBst;
        BinarySearchTree<string> pluginComparatorBst;
        BinarySearchTree<Book> bookBst;
        BinarySearchTree<Book> bookPluginBst;
        BinarySearchTree<Point> pointPluginBst;

        IComparer<string> stringComparator;
        IComparer<Book> bookComparator;
        IComparer<Point> pointComparator;

        static object[] preorderTraversStringResult = new object[]
            {
                new string[] {"abcd", "abc", "ab", "abcc", "dcef", "dce", "dc", "dcfg" }
            };

        static object[] inorderTraversStringResult = new object[]
            {
                new string[] {"ab", "abc", "abcc", "abcd", "dc", "dce", "dcef", "dcfg" }
            };

        static object[] postorderTraversStringResult = new object[]
            {
                new string[] {"ab", "abcc", "abc", "dc", "dce", "dcfg", "dcef", "abcd" }
            };

        static object[] pluginComparatorStringResult = new object[]
            {
                new string[] {"dcfg", "dcef", "dce", "dc", "abcd", "abcc", "abc", "ab" }
            };

        static object[] comparatorBookResult = new object[]
            {
                new string[] {"Book 5", "Book 7", "Book 3", "Book 8", "Book 1", "Book 6", "Book 2", "Book 4" }
            };

        static object[] pluginComparatorBookResult = new object[]
            {
                new string[] { "Book 4", "Book 2", "Book 6", "Book 1", "Book 8", "Book 3", "Book 7", "Book 5" }
            };

        static object[] pluginComparatorPointResult = new object[]
            {
                new double[] { 1.0, 2.2, 2.9, 3.2, 3.6, 5, 5.8 }
            };

        Book[] bookArray = new Book[] { new Book("Book 1", 300), new Book("Book 2", 400), new Book("Book 3", 200), new Book("Book 4", 450), new Book("Book 5", 150), new Book("Book 6", 350), new Book("Book 7", 165), new Book("Book 8", 250) };
        Point[] pointArray = new Point[] { new Point(4.0, 3.0), new Point(1.0, 0.0), new Point(1.0, 2.0), new Point(-1.0, 3.0), new Point(2.5, 1.5), new Point(5.0, -3.0), new Point(0.5, 3.2), new Point(2.0, 3.0) };

        [SetUp]
        public void SetUp()
        {
            traverseBst = new BinarySearchTree<string>("abcd", "abc", "ab", "dcef", "dce", "dc", "dcfg", "abcc");

            stringComparator = new CustomInvertComparer<string>();
            bookComparator = new CustomInvertComparer<Book>();
            pointComparator = new NullPointVectorComparer<Point>();

            pluginComparatorBst = new BinarySearchTree<string>(stringComparator, "abcd", "abc", "ab", "dcef", "dce", "dc", "dcfg", "abcc");

            bookBst = new BinarySearchTree<Book>(bookArray);
            bookPluginBst = new BinarySearchTree<Book>(bookComparator, bookArray);

            pointPluginBst = new BinarySearchTree<Point>(pointComparator, pointArray);
        }

        [TestCaseSource("preorderTraversStringResult")]
        public void BinarySearchTree_String_Preorder_Traverse_Test(string[] expectedResult)
        {
            string[] result = new string[expectedResult.Length];
            int i = 0;
            foreach (var item in traverseBst.Preorder(traverseBst.rootNode))
            {
                result[i++] = item;
            }

            IStructuralEquatable eq = result;
            Assert.IsTrue(eq.Equals(expectedResult, EqualityComparer<string>.Default));
        }

        [TestCaseSource("inorderTraversStringResult")]
        public void BinarySearchTree_String_Inorder_Traverse_Test(string[] expectedResult)
        {
            string[] result = new string[expectedResult.Length];
            int i = 0;
            foreach (var item in traverseBst.Inorder(traverseBst.rootNode))
            {
                result[i++] = item;
            }

            IStructuralEquatable eq = result;
            Assert.IsTrue(eq.Equals(expectedResult, EqualityComparer<string>.Default));
        }

        [TestCaseSource("postorderTraversStringResult")]
        public void BinarySearchTree_String_Postorder_Traverse_Test(string[] expectedResult)
        {
            string[] result = new string[expectedResult.Length];
            int i = 0;
            foreach (var item in traverseBst.Postorder(traverseBst.rootNode))
            {
                result[i++] = item;
            }

            IStructuralEquatable eq = result;
            Assert.IsTrue(eq.Equals(expectedResult, EqualityComparer<string>.Default));
        }

        [TestCaseSource("pluginComparatorStringResult")]
        public void BinarySearchTree_String_Plugin_Comparator_Inorder_Traverse_Test(string[] expectedResult)
        {
            string[] result = new string[expectedResult.Length];
            int i = 0;
            foreach (var item in pluginComparatorBst.Inorder(pluginComparatorBst.rootNode))
            {
                result[i++] = item;
            }

            IStructuralEquatable eq = result;
            Assert.IsTrue(eq.Equals(expectedResult, EqualityComparer<string>.Default));
        }

        [TestCaseSource("comparatorBookResult")]
        public void BinarySearchTree_Book_Default_Comparator_Inorder_Traverse_Test(string[] expectedResult)
        {
            string[] result = new string[expectedResult.Length];
            int i = 0;
            foreach (var item in bookBst.Inorder(bookBst.rootNode))
            {
                result[i++] = item.title;
            }

            IStructuralEquatable eq = result;
            Assert.IsTrue(eq.Equals(expectedResult, EqualityComparer<string>.Default));
        }

        [TestCaseSource("pluginComparatorBookResult")]
        public void BinarySearchTree_Book_Plugin_Comparator_Inorder_Traverse_Test(string[] expectedResult)
        {
            string[] result = new string[expectedResult.Length];
            int i = 0;
            foreach (var item in bookPluginBst.Inorder(bookPluginBst.rootNode))
            {
                result[i++] = item.title;
            }

            IStructuralEquatable eq = result;
            Assert.IsTrue(eq.Equals(expectedResult, EqualityComparer<string>.Default));
        }

        [TestCaseSource("pluginComparatorPointResult")]
        public void BinarySearchTree_Point_Plugin_Comparator_Inorder_Traverse_Test(double[] expectedResult)
        {
            double[] result = new double[expectedResult.Length];
            int i = 0;
            foreach (var item in pointPluginBst.Inorder(pointPluginBst.rootNode))
            {
                result[i++] = Math.Round(Math.Sqrt((item.x * item.x) + (item.y * item.y)), 1);
            }

            IStructuralEquatable eq = result;
            Assert.IsTrue(eq.Equals(expectedResult, EqualityComparer<double>.Default));
        }
    }
}
