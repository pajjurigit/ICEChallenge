using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace IceChallenge
{
    public class CusipPrices
    {
        private Dictionary<string, string> dict = new Dictionary<string, string>();

        public bool ProcessCusipFiles(string folderPath)
        {
            //folderPath=@"Data\Names.txt";

            if (string.IsNullOrEmpty(folderPath)) return false;

            string lastCusip = "";
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.txt"))
            {
                lastCusip = ProcessCusipPriceFile( file, lastCusip);
            }

            WriteResultsToATextFile();

            return true;
        }

        public bool WriteResultsToATextFile()
        {
            using (StreamWriter file = new StreamWriter("LatestCusipPrices.txt"))
            {
                foreach (var entry in dict)
                    file.WriteLine("[{0} {1}]", entry.Key, entry.Value);
            }

            return true;

        }

        public string ProcessCusipPriceFile(string filePath, string lastFileCusip="")
        {

            if (string.IsNullOrEmpty(filePath)) return "";
            string currCusip = lastFileCusip;

            using (StreamReader sr = File.OpenText(filePath))
            {
                //String row = "", current = "", previous = "", price = "";
                string row = "";
                Regex r = new Regex("^[a-zA-Z0-9]*$");

                 //to keep track of cusips, between the files...
                //bool skipInvalid = false;

                while((row=sr.ReadLine()) != null)
                {
                    if(r.IsMatch(row) && row.Length==8)
                    {
                        currCusip = row;
                        //add it to Hashtable
                        if(!dict.ContainsKey(row)) { dict.Add(row, ""); }
                        
                    }
                    else
                    {
                      //  if (skipInvalid) continue;
                        double result;
                        if(Double.TryParse(row, out result)) {
                            dict[currCusip] = row; //this gets updated until next cusip is read from the file...
                        }
                        //else { skipInvalid = true; }
                    }

                }
            }

                return currCusip;
        }

    }
}
