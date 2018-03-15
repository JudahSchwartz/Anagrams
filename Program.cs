using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anagram_Finder
{
    class Program
    {
        static readonly HashSet<AnagrammizedWord> Words = new HashSet<AnagrammizedWord>();

        static void Main(string[] args)
        {
            ReadFromFile();
            var orderedByAnagram = Words.OrderBy(aw => aw.Anagram).ToArray();
            string currentAnagram = String.Empty;
            foreach (var word in orderedByAnagram)
            {
                if (word.Anagram != currentAnagram)
                {
                    currentAnagram = word.Anagram;
                    Console.WriteLine("__________________________________");
                }

                Console.WriteLine(word.Word);
            }

            Console.ReadLine();

        }

        private static void ReadFromFile()
        {
            var reader =
                new StreamReader(
                    @"C:\Users\SchwartzPC\source\repos\Anagram Finder\Anagram Finder\Resources\wordsFile.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Words.Add(new AnagrammizedWord(line.ToLower()));
            }
        }
    }

    public class AnagrammizedWord
    {
        public string Word { get; set; }
        public string Anagram { get; set; }

        public AnagrammizedWord(string word)
        {
            this.Word = word;
            Anagram = String.Concat(word.OrderBy(c => c));
        }
    }
}
