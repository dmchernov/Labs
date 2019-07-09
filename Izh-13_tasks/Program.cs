using System;
using System.Collections.Generic;
using System.Linq;
using Izh_12_tasks.Task_7;

namespace Izh_13_tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestResult[] testResultArray = new TestResult[] {
            //    new TestResult("Петров Петр", "Физика", DateTime.Now.AddDays(-3), 3, 1),
            //    new TestResult("Яблоков Геннадий", "Физика", DateTime.Now.AddDays(-3), 4, 2),
            //    new TestResult("Пирожков Александр", "Физика", DateTime.Now.AddDays(-3), 5, 3),
            //    new TestResult("Петров Петр", "Физика", DateTime.Now.AddDays(-3), 4, 4),

            //    new TestResult("Яблоков Геннадий", "Химия", DateTime.Now.AddDays(-2), 3, 5),
            //    new TestResult("Петров Петр", "Химия", DateTime.Now.AddDays(-2), 5, 6),
            //    new TestResult("Пирожков Александр", "Химия", DateTime.Now.AddDays(-2), 5, 7),
            //    new TestResult("Петров Петр", "Химия", DateTime.Now.AddDays(-2), 2, 8),

            //    new TestResult("Яблоков Геннадий", "Математический анализ", DateTime.Now.AddDays(-2), 3, 5),
            //    new TestResult("Петров Петр", "Математический анализ", DateTime.Now.AddDays(-2), 3, 6),
            //    new TestResult("Пирожков Александр", "Математический анализ", DateTime.Now.AddDays(-2), 4, 7),
            //    new TestResult("Петров Петр", "Математический анализ", DateTime.Now.AddDays(-2), 3, 8)
            //};

            //Helpers.WriteDataInFile("TestResult.dat", testResultArray);

            TestResult[] testResultArray = Helpers.ReadDataFromFile("TestResult.dat");

            BinarySearchTree<TestResult> bst = new BinarySearchTree<TestResult>(testResultArray);

            do
            {
                var query = bst.Inorder(bst.rootNode);

                Console.WriteLine("Задайте критерии для фильтрации и сортировки выборки.");
                Console.WriteLine("Для того, чтобы не задавать критерий можно просто нажать Enter.");
                Console.WriteLine("В критериях фильтрации по дате и оценке можно указывать как конкретное значение, так и диапазон.");
                Console.WriteLine("Возможные форматы указания даты: \"ДД.ММ.ГГГГ\", \">ДД.ММ.ГГГГ\", \"<ДД.ММ.ГГГГ\", \"\"<ДД.ММ.ГГГГ>ДД.ММ.ГГГГ\"");
                Console.WriteLine("Возможные форматы указания оценки: \"3\", \">3\", \"<3\", \">3<5\"");

                query = GetStudentNameFilter(query);
                query = GetTestNameFilter(query);
                query = GetDateFilter(query);
                query = GetAssessmentFilter(query);
                query = GetSortCriteria(query);
                query = GetLimitRows(query);

                Console.WriteLine(string.Format("{0,-25}{1,-23}{2,-12}{3,7}", "Имя:", "Предмет:", "Дата:", "Оценка:"));

                foreach (var item in query)
                {
                    Console.WriteLine(string.Format("{0,-25}{1,-23}{2,-12}{3,7}", item.StudentName, item.TestName, item.TestDate.ToShortDateString(), item.Assessment.ToString()));
                }

                query = null;

                Console.WriteLine();
                Console.WriteLine("Для выхода нажмите Esc. Для продолжения нажмите любую клавишу.");
            }
            while (Console.ReadKey().KeyChar != 27);
        }

        private static IEnumerable<TestResult> GetLimitRows(IEnumerable<TestResult> query)
        {
            int count;
            string rowLimit;
            bool parseResult;

            Console.Write("Количество строк результата: ");

            do
            {
                rowLimit = Console.ReadLine();
                parseResult = true;

                if (rowLimit.Length > 0)
                {
                    if (!int.TryParse(rowLimit, out count))
                    {
                        parseResult = false;
                    }
                    else
                    {
                        query = query.Take(count);
                    }
                }
            }
            while (!parseResult);

            return query;
        }

        private static IEnumerable<TestResult> GetSortCriteria(IEnumerable<TestResult> query)
        {
            Console.WriteLine("По какому критерию сортировать?");
            Console.WriteLine("1 - имя студента, 2 - имя теста, 3 - дата, 4 - оценка. ");

            ConsoleKeyInfo sortField;
            do
            {
                sortField = Console.ReadKey();
            }
            while (sortField.KeyChar != 13 && !"1234".Contains(sortField.KeyChar));

            Console.WriteLine();

            Console.WriteLine("Выберите порядок сортировки.");
            Console.WriteLine("1 - по возрастанию, 2 - по убыванию. ");

            ConsoleKeyInfo sortOrder;
            do
            {
                sortOrder = Console.ReadKey();
            }
            while (!"12".Contains(sortOrder.KeyChar) && sortOrder.KeyChar != 13);

            Console.WriteLine();

            SortField field;
            SortDirection direction;

            if (sortField.KeyChar != 13)
            {
                if (!Enum.TryParse(sortField.KeyChar.ToString(), out field))
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                field = SortField.Default;
            }

            if (sortOrder.KeyChar != 13)
            {
                if (!Enum.TryParse(sortOrder.KeyChar.ToString(), out direction))
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                direction = SortDirection.Ascending;
            }

            query = query.SortQuery(field, direction);

            return query;
        }

        private static IEnumerable<TestResult> GetAssessmentFilter(IEnumerable<TestResult> query)
        {
            Console.Write("Оценка: ");
            string assessment = Console.ReadLine();

            if (assessment.Length > 0)
            {
                query = query.AssessmentQuery(assessment);
            }

            return query;
        }

        private static IEnumerable<TestResult> GetDateFilter(IEnumerable<TestResult> query)
        {
            string date;
            bool dateParseResult;

            do
            {
                dateParseResult = true;
                Console.Write("Дата (ДД.ММ.ГГГГ): ");
                date = Console.ReadLine();

                if (date.Length > 0)
                {
                    try
                    {
                        query = query.DateQuery(date);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Не удалось распознать введённую дату. Попробуйте ещё раз.");
                        dateParseResult = false;
                    }
                }
            }
            while (!dateParseResult);

            return query;
        }

        private static IEnumerable<TestResult> GetTestNameFilter(IEnumerable<TestResult> query)
        {
            Console.Write("Название предмета: ");
            string testName = Console.ReadLine();

            if (testName.Length > 0)
            {
                query = query.TestNameContainsQuery(testName);
            }

            return query;
        }

        private static IEnumerable<TestResult> GetStudentNameFilter(IEnumerable<TestResult> query)
        {
            Console.Write("Имя студента: ");
            string studentName = Console.ReadLine();

            if (studentName.Length > 0)
            {
                query = query.StudentNameContainsQuery(studentName);
            }

            return query;
        }
    }
}
