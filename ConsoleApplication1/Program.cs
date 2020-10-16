using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConsoleApplication1
{
    public class Counter
    {
        public const int typeCount = 4;
        public Dictionary<string, int> count = new Dictionary<string, int>();
        public HashSet<string> allowedWords = new HashSet<string>();
        public List<string> wrongWords = new List<string>();

        public Counter()
        {
            allowedWords.Add(DataType.None.ToString());
            allowedWords.Add(DataType.First.ToString());
            allowedWords.Add(DataType.Second.ToString());
            allowedWords.Add(DataType.Third.ToString());
            allowedWords.Add(DataType.Fourth.ToString());
            foreach (var word in allowedWords)
            {
                count.Add(word, 0);
            }
        }

        public enum DataType
        {
            None = 0,
            First = 1,
            Second = 2,
            Third = 3,
            Fourth = 4
        }


        public void AddWord(string word)
        {
            string keyWord = word;
            if (word.Length == 1 && '0' <= word[0] && word[0] <= '4')
            {
                keyWord = ((DataType) (word[0]) - '0').ToString();
            }

            if (allowedWords.Contains(keyWord))
            {
                count[keyWord]++;
            }
            else
            {
                wrongWords.Add(keyWord);
            }
        }

        public int getCount(DataType type)
        {
            return count[type.ToString()];
        }

        public List<string> getErrors()
        {
            return wrongWords;
        }
    }

    internal class Program
    {
        public static void Print(Counter cnt)
        {
            Console.WriteLine("Input data types:");
            for (int i = 0; i <= Counter.typeCount; i++)
            {
                Counter.DataType type = (Counter.DataType) i;
                Console.WriteLine("{0}({1})-{2}", type.ToString(), i, cnt.getCount(type));
            }

            if (cnt.getErrors().Count == 0)
            {
                return;
            }

            Console.WriteLine("Errors:");
            Console.Write("Not valid input strings: ");
            for (int i = 0; i < cnt.getErrors().Count; i++)
            {
                Console.Write(cnt.getErrors()[i]);
                if (i < cnt.getErrors().Count - 1)
                {
                    Console.Write(",");
                }
            }
        }

        public static void Main()
        {
            Counter cnt = new Counter();
            String Line = Console.ReadLine();
            String[] words = Line.Split(',');
            if (Line.Length == 0)
            {
                Console.WriteLine("No data");
                return;
            }

            foreach (var word in words)
            {
                cnt.AddWord(word);
            }

            Print(cnt);
        }
    }
}