using System;

namespace Izh_09_tasks.Task_5
{
    public class ReverseWordsClass
    {
        public static string ReverseWords(string input)
        {
            string[] wordArray = input.Trim().Split();
            Array.Reverse(wordArray);
            return string.Join(" ", wordArray);
        }
    }
}
