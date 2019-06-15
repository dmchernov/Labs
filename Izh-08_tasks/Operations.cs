using System.Reflection;

namespace Izh_08_tasks
{
    /// <summary>
    /// Класс, представляющий методы сравнения.
    /// </summary>
    public class Operations
    {
        /// <summary>
        /// Метод, выполняющий операцию сравнения.
        /// </summary>
        /// <param name="criterionName">имя критерия сравнения.</param>
        /// <param name="values">Массив сравниваемых объектов.</param>
        /// <returns>Результат выполнения операции сравнения.</returns>
        public bool CompareOperation(string criterionName, object[] values)
        {
            MethodInfo method = this.GetType().GetMethod(criterionName);
            return (bool)method.Invoke(this, values);
        }

        public bool GreaterThan(int a, int b)
        {
            return a > b;
        }

        public bool LowerThan(int a, int b)
        {
            return a < b;
        }
    }
}
