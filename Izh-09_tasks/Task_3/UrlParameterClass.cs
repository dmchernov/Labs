using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izh_09_tasks.Task_3
{
    public class UrlParameterClass
    {
        /// <summary>
        /// Метод добавляет или изменяет параметры в строке URL.
        /// </summary>
        /// <param name="url">Исходная строка URL.</param>
        /// <param name="keyValue">Параметры ключ=значение, которые нужно добавить или изменить. </param>
        /// <returns>Строка URL с добавленными или скорректированными параметрами.</returns>
        public static string AddOrChangeUrlParameter(string url, string keyValue)
        {
            string[] urlParts = url.Split('?');

            if (urlParts.Length == 1 || string.IsNullOrEmpty(urlParts[1]))
            {
                return string.Format("{0}?{1}", urlParts[0], keyValue);
            }

            Dictionary<string, string> urlKeyValues = urlParts[1].Split('&').ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);

            string[] inputKeyValue = keyValue.Split('=');

            if (urlKeyValues.ContainsKey(inputKeyValue[0]))
            {
                urlKeyValues[inputKeyValue[0]] = inputKeyValue[1];
            }
            else
            {
                urlKeyValues.Add(inputKeyValue[0], inputKeyValue[1]);
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(urlParts[0]).Append("?");

            foreach (var item in urlKeyValues)
            {
                sb.Append(item.Key).Append("=").Append(item.Value).Append("&");
            }

            return sb.ToString().TrimEnd('&');
        }
    }
}
