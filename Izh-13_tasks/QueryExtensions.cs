using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izh_13_tasks
{
    public static class QueryExtensions
    {
        /// <summary>
        /// Добавляет в LINQ-запрос фильтрацию по имени студента, указанному пользователем.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="text">Имя или часть имени студента.</param>
        /// <returns></returns>
        public static IEnumerable<T> StudentNameContainsQuery<T>(this IEnumerable<T> query, string text) where T : TestResult
        {
            return query.Where(n => n.StudentName.Contains(text));
        }

        /// <summary>
        /// Добавляет в LINQ-запрос фильтрацию по названию предмета, указанному пользователем.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="text">Название или часть названия предмета.</param>
        /// <returns></returns>
        public static IEnumerable<T> TestNameContainsQuery<T>(this IEnumerable<T> query, string text) where T : TestResult
        {
            return query.Where(n => n.TestName.Contains(text));
        }

        /// <summary>
        /// Добавляет в LINQ-запрос фильтрацию по дате или диапазону дат, указанным пользователем.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="text">Формат указания фильтра дат: точная дата "ДД.ММ.ГГГГ", больше указанной даты "&gt;ДД.ММ.ГГГГ", меньше указанной "&lt;ДД.ММ.ГГГГ" или оба сразу.</param>
        /// <returns></returns>
        public static IEnumerable<T> DateQuery<T>(this IEnumerable<T> query, string text) where T : TestResult
        {
            DateTime start;
            DateTime end;

            text = text.Trim();

            if (text.Length == 0)
            {
                return query;
            }

            if (text.IndexOf('>') != -1)
            {
                if (!DateTime.TryParse(text.Substring(text.IndexOf('>') + 1, 10), out start))
                {
                    throw new ArgumentException("Ошибка при преобразовании в формат даты значения, для фильтрации по критерию 'больше которой'.");
                }

                query = query.Where(n => n.TestDate > start);
            }
            
            if (text.IndexOf('<') != -1)
            {
                if (!DateTime.TryParse(text.Substring(text.IndexOf('<') + 1, 10), out end))
                {
                    throw new ArgumentException("Ошибка при преобразовании в формат даты значения, для фильтрации по критерию 'меньше которой'.");
                }

                query = query.Where(n => n.TestDate < end);
            }
            
            if (!text.Contains('<') && !text.Contains('>'))
            {
                if (!DateTime.TryParse(text, out start))
                {
                    throw new ArgumentException("Ошибка при преобразовании в формат даты значения, для фильтрации по критерию 'точная дата'.");
                }

                query = query.Where(n => n.TestDate == start);
            }

            return query;
        }

        /// <summary>
        /// Добавляет в LINQ-запрос фильтрацию по оценке или диапазону оценок, указанным пользователем.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="text">Формат указания фильтра оценок: точная оценка "3", больше чем "&gt;1", меньше чем "&lt;4" или оба сразу.</param>
        /// <returns></returns>
        public static IEnumerable<T> AssessmentQuery<T>(this IEnumerable<T> query, string text) where T: TestResult
        {
            int start;
            int end;

            text = text.Trim();

            if (text.Length == 0)
            {
                return query;
            }

            if (text.IndexOf('>') != -1)
            {
                if (!int.TryParse(text.Substring(text.IndexOf('>') + 1, 1), out start))
                {
                    throw new ArgumentException("Ошибка при преобразовании в число значения оценки, для фильтрации по критерию 'больше которой'.");
                }

                query = query.Where(n => n.Assessment > start);
            }

            if (text.IndexOf('<') != -1)
            {
                if (!int.TryParse(text.Substring(text.IndexOf('<') + 1, 1), out end))
                {
                    throw new ArgumentException("Ошибка при преобразовании в число значения оценки, для фильтрации по критерию 'меньше которой'.");
                }

                query = query.Where(n => n.Assessment < end);
            }

            if (!text.Contains('<') && !text.Contains('>'))
            {
                if (!int.TryParse(text, out start))
                {
                    throw new ArgumentException("Ошибка при преобразовании в число значения оценки, для фильтрации по критерию 'равно'.");
                }

                query = query.Where(n => n.Assessment == start);
            }

            return query;
        }

        /// <summary>
        /// Добавляет в LINQ-запрос направление и поле сортировки.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="sortField"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static IEnumerable<T> SortQuery<T>(this IEnumerable<T> query, SortField sortField, SortDirection sortDirection) where T: TestResult
        {
            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    switch (sortField)
                    {
                        case SortField.StudentName:
                            query = query.OrderBy(n => n.StudentName);
                            break;
                        case SortField.TestName:
                            query = query.OrderBy(n => n.TestName);
                            break;
                        case SortField.Date:
                            query = query.OrderBy(n => n.TestDate);
                            break;
                        case SortField.Assessment:
                            query = query.OrderBy(n => n.Assessment);
                            break;
                        case SortField.Default:
                            query = query.OrderBy(n => n);
                            break;
                    }
                    break;
                case SortDirection.Descending:
                    switch (sortField)
                    {
                        case SortField.StudentName:
                            query = query.OrderByDescending(n => n.StudentName);
                            break;
                        case SortField.TestName:
                            query = query.OrderByDescending(n => n.TestName);
                            break;
                        case SortField.Date:
                            query = query.OrderByDescending(n => n.TestDate);
                            break;
                        case SortField.Assessment:
                            query = query.OrderByDescending(n => n.Assessment);
                            break;
                        case SortField.Default:
                            query = query.OrderByDescending(n => n);
                            break;
                    }
                    break;
            }

            return query;
        }
    }
}
