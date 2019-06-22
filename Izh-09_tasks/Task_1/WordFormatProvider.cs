using System;
using System.Text;

namespace Izh_09_tasks.Task_1
{
    public class WordFormatProvider : IFormatProvider, ICustomFormatter
    {
        //преобразует слова в строке к виду "в сТРОКЕ тАКОЙ рЕГИСТР"
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null || string.IsNullOrEmpty(format) || format.Substring(0, 1) != "W")
            {
                return string.Empty;
            }

            string[] wordArray = arg.ToString().ToUpperInvariant().Split(' ');
            StringBuilder sb = new StringBuilder();
            foreach (var str in wordArray)
            {
                sb.Append(str[0].ToString().ToLowerInvariant());
                if (str.Length > 1)
                {
                    sb.Append(str.Substring(1));
                }

                sb.Append(" ");
            }

            return sb.ToString().TrimEnd();
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }
    }
}
