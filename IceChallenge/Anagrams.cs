using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceChallenge
{
    public class Anagrams
    {
        private Dictionary<string, List<string>> anagrams = new Dictionary<string, List<string>>();
        public void ListAllAnagrams(string sentence)
        {
            var words = sentence.Split(' ');

            for(int i=0; i< words.Count(); i++)
            {
                string word = words[i];
                char[] wchars = word.ToCharArray();
                Array.Sort(wchars);
                string sortedWord = new string(wchars);

                if(anagrams.ContainsKey(sortedWord))
                {
                    var list = anagrams[sortedWord];
                    list.Add(word);
                }
                else
                {
                    anagrams.Add(sortedWord, new List<string>() { word });
                }

            }
            WriteAllAnagrams();
        }

        public void WriteAllAnagrams()
        {
            foreach (var entry in anagrams)
            {
                Console.Write("[ {0}: ", entry.Key);
                foreach (string word in entry.Value)
                {
                    Console.Write(word + " ");
                }
                Console.Write("]");
                Console.WriteLine();
            }
        }
        
    }
}
