using System;
using NUnit.Framework;
using Izh_12_tasks.Task_7;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Izh_13_tasks.Tests
{
    [SetCulture("ru-ru")]
    [TestFixture]
    public class TestsResultsQueryTestsNUnit
    {
        TestResult[] dataArray;
        BinarySearchTree<TestResult> bst;
        IEnumerable<TestResult> query;

        [OneTimeSetUp]
        public void DataSetUp()
        {
            dataArray = Helpers.ReadDataFromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResult.dat"));
            bst = new BinarySearchTree<TestResult>(dataArray);
        }

        [SetUp]
        public void TestSetUp()
        {
            query = bst.Inorder(bst.rootNode);
        }

        [TestCase]
        public void Query_StudentName_Filter_Test()
        {
            foreach (var item in query.StudentNameContainsQuery("Петр"))
            {
                Assert.AreEqual("Петров Петр", item.StudentName);
            }
        }

        [TestCase]
        public void Query_TestName_Filter_Test()
        {
            foreach (var item in query.TestNameContainsQuery("Хими"))
            {
                Assert.AreEqual("Химия", item.TestName);
            }
        }

        [TestCase]
        public void Query_Assessment_Exact_Filter_Test()
        {
            foreach (var item in query.AssessmentQuery("3"))
            {
                Assert.AreEqual(3, item.Assessment);
            }
        }

        [TestCase]
        public void Query_Assessment_GreaterThan_Filter_Test()
        {
            foreach (var item in query.AssessmentQuery(">3"))
            {
                Assert.IsTrue(item.Assessment > 3);
            }
        }

        [TestCase]
        public void Query_Assessment_LowerThan_Filter_Test()
        {
            foreach (var item in query.AssessmentQuery("<4"))
            {
                Assert.IsTrue(item.Assessment < 4);
            }
        }

        [TestCase]
        public void Query_Assessment_Between_Filter_Test()
        {
            foreach (var item in query.AssessmentQuery(">2<5"))
            {
                Assert.IsTrue(2 < item.Assessment && item.Assessment < 5);
            }
        }

        [TestCase]
        public void Query_Date_Exact_Filter_Test()
        {
            foreach (var item in query.DateQuery("05.07.2019"))
            {
                Assert.AreEqual(DateTime.Parse("05.07.2019"), item.TestDate);
            }
        }

        [TestCase]
        public void Query_Date_GreaterThan_Filter_Test()
        {
            foreach (var item in query.DateQuery(">04.07.2019"))
            {
                Assert.IsTrue(item.TestDate > DateTime.Parse("04.07.2019"));
            }
        }

        [TestCase]
        public void Query_Date_LowerThan_Filter_Test()
        {
            foreach (var item in query.DateQuery("<05.07.2019"))
            {
                Assert.IsTrue(item.TestDate < DateTime.Parse("05.07.2019"));
            }
        }

        [TestCase]
        public void Query_Date_Between_Filter_Test()
        {
            Assert.IsTrue(query.DateQuery(">04.07.2019<05.07.2019").Count() == 0);
        }

        [TestCase]
        public void Query_Sort_Assessment_Ascending_Test()
        {
            query = query.SortQuery(SortField.Assessment, SortDirection.Ascending);

            Assert.AreEqual(2, query.First().Assessment);
            Assert.AreEqual(5, query.Last().Assessment);
        }

        [TestCase]
        public void Query_Sort_TestName_Descending_Test()
        {
            query = query.SortQuery(SortField.TestName, SortDirection.Descending);

            Assert.AreEqual("Химия", query.First().TestName);
            Assert.AreEqual("Математический анализ", query.Last().TestName);
        }

        [TestCase]
        public void Query_Sort_TestDate_Ascending_Test()
        {
            query = query.SortQuery(SortField.Date, SortDirection.Ascending);

            Assert.AreEqual(DateTime.Parse("04.07.2019"), query.First().TestDate);
            Assert.AreEqual(DateTime.Parse("05.07.2019"), query.Last().TestDate);
        }
    }
}
