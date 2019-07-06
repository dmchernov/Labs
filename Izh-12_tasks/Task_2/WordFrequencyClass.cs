using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Izh_12_tasks.Task_2
{
    class WordFrequencyClass
    {
        public static Dictionary<string, int> WordFrequencyCalculation(string text)
        {
            if (text == null)
            {
                throw new NullReferenceException(nameof(text));
            }

            text = text.Trim();

            if (text.Length == 0)
            {
                return null;
            }

            Regex regEx = new Regex(@"[^\w\s]*");
            text = regEx.Replace(text, string.Empty);
            string[] wordArray = text.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> wordDict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (var word in wordArray)
            {
                if (wordDict.ContainsKey(word))
                {
                    wordDict[word]++;
                }
                else
                {
                    wordDict.Add(word, 1);
                }
            }

            return wordDict;
        }
    }
}
