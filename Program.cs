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
        private static readonly AnnagramBucketSet AnnagramBucketSet = new AnnagramBucketSet();
        
        static void Main(string[] args)
        {
            ReadFromFile();
            var topAnnagramBuckets = AnnagramBucketSet.GetTopAnnagramBuckets(10);
            int count = 0;
            foreach (var bucket in topAnnagramBuckets)
            {
                Console.WriteLine($"Number {++count}: Annagram Group:{bucket.Annagram} Number of Annagrams: {bucket.AnnagramCount}");
                foreach (var word in bucket.WordSet)
                {
                    Console.WriteLine(word);
                }
               
                
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
                AnnagramBucketSet.Add(line.ToLower());
            }
        }
    }

    class AnnagramBucketSet 
    {
        private Dictionary<string, AnnagramBucket> BackingDictionary = new Dictionary<string, AnnagramBucket>();

        public List<AnnagramBucket> GetTopAnnagramBuckets(int num)
        {
            return BackingDictionary.Values.OrderByDescending(b => b.AnnagramCount).Take(num).ToList();
            
        }
        public bool Add(string word)
        {
            var annagram = String.Concat(word.OrderBy(c => c));
            if (! BackingDictionary.ContainsKey(annagram))
            {
                BackingDictionary[annagram] = new AnnagramBucket(annagram);
            }

            return BackingDictionary[annagram].Add(word);
        }
    }

    public class AnnagramBucket 
    {
        public readonly string Annagram;
        public readonly ISet<string> WordSet = new HashSet<string>();

        public override int GetHashCode()
        {
            return Annagram.GetHashCode();
        }

        public int AnnagramCount => WordSet.Count;

        public AnnagramBucket(string annagram)
        {
            Annagram = annagram;
        }

        public bool Add(string word)
        {
            return WordSet.Add(word);
        }
    }
}
