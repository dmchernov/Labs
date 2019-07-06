using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Izh_12_tasks.Task_8
{
    public class Calculator
    {
        static Regex rgx = new Regex(@"[\/\*\+\-]{1}");
        static Stack<double> stack = new Stack<double>();

        /// <summary>
        /// Вычисляет математическое выражение в обратной польской нотации.  Все операнды и операторы во входной строке должны быть разделены пробелами.
        /// </summary>
        /// <param name="expression">Выражение.</param>
        /// <returns>Результат с округлением до 0,1. Если выражение является пустой стройкой, то возвращается 0,0.</returns>
        public static double Calculate(string expression)
        {
            double operand1;
            double operand2;
            double parseTmp;

            if (expression == null)
            {
                throw new NullReferenceException(nameof(expression));
            }

            if (expression.Length == 0)
            {
                return 0.0;
            }

            int exprSignsCount = expression.Where(c => c == '/' || c == '*' || c == '+' || c == '-').Count();
            string tmp = rgx.Replace(expression, string.Empty);
            int valuesCount = tmp.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;

            if (exprSignsCount != valuesCount - 1)
            {
                throw new ArgumentException();
            }

            foreach (var item in expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (IsOperator(item))
                {
                    operand2 = stack.Pop();
                    operand1 = stack.Pop();
                    stack.Push(Evaluate(operand1, operand2, item));
                }
                else
                {
                    if (!double.TryParse(item, out parseTmp))
                    {
                        throw new ArgumentException();
                    }

                    stack.Push(parseTmp);
                }
            }

            return stack.Pop();
        }

        /// <summary>
        /// Проверяет, является ли элемент строки выражения математическим оператором.
        /// </summary>
        /// <param name="value">Элемент.</param>
        /// <returns>True -- если, элемент является одним из математических операторов + - * /, иначе false.</returns>
        private static bool IsOperator(string value)
        {
            if (value.Length > 1)
            {
                return false;
            }

            char operato;

            if (!char.TryParse(value, out operato))
            {
                return false;
            }

            if (operato == '/' || operato == '*' || operato == '+' || operato == '-')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Осуществить вычисление двух операндов согласно оператору.
        /// </summary>
        /// <param name="operand1">Операнд 1.</param>
        /// <param name="operand2">Операнд 2.</param>
        /// <param name="operato">Оператор.</param>
        /// <returns>Результат с округлением до 0,1.</returns>
        private static double Evaluate(double operand1, double operand2, string operato)
        {
            char oprtor;
            double result;

            if (!char.TryParse(operato, out oprtor))
            {
                throw new ArgumentException();
            }

            switch (oprtor)
            {
                case '+':
                    result = operand1 + operand2;
                    break;
                case '-':
                    result = operand1 - operand2;
                    break;
                case '*':
                    result = operand1 * operand2;
                    break;
                case '/':
                    if (operand2 == 0.0)
                    {
                        throw new DivideByZeroException();
                    }

                    result = operand1 / operand2;
                    break;
                default:
                    throw new ArgumentException();
            }

            return Math.Round(result, 1);
        }
    }
}
