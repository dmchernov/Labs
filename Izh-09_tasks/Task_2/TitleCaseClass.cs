using System.Text;

namespace Izh_09_tasks.Task_2
{
    public class TitleCaseClass
    {
        public static string TitleCase(string convert, string minor)
        {
            if (!string.IsNullOrEmpty(minor))
            {
                minor = minor.ToUpperInvariant();
            }

            StringBuilder sb = new StringBuilder();

            foreach (string word in convert.Split())
            {
                if (!string.IsNullOrEmpty(minor) && minor.Contains(word.ToUpperInvariant()))
                {
                    sb.Append(word.ToLowerInvariant());
                }
                else
                {
                    sb.Append(word[0].ToString().ToUpperInvariant());
                    if (word.Length > 1)
                    {
                        sb.Append(word.Substring(1).ToLowerInvariant());
                    }
                }

                sb.Append(" ");
            }

            sb[0] = sb[0].ToString().ToUpperInvariant().ToCharArray()[0];

            return sb.ToString().TrimEnd();
        }

        public static string TitleCase(string convert)
        {
            return TitleCase(convert, string.Empty);
        }
    }
}
