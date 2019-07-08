using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Izh_13_tasks
{
    public class Helpers
    {
        /// <summary>
        /// Читает данные из бинарного файла в массив.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <returns>Массив, прочитанных из файла данных.</returns>
        public static TestResult[] ReadDataFromFile(string filePath)
        {
            FileStream fstream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryFormatter formatter = new BinaryFormatter();

            TestResult[] resultArray = (TestResult[])formatter.Deserialize(fstream);
            fstream.Close();
            return resultArray;
        }

        /// <summary>
        /// Записывает данные из массива в бинарный файл.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="data">Массив данных.</param>
        public static void WriteDataInFile(string filePath, TestResult[] data)
        {
            FileStream fstream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fstream, data);
            fstream.Close();
        }
    }
}
