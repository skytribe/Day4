using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hamlet;
using System.Text.RegularExpressions;

namespace CatCounter
{
    class Program
    {
        static void Main(string[] args)
        {

            var sw = new System.Diagnostics.Stopwatch();

            string searchfor = "the";

            var s = ExtractTextFromWebPage("http://shakespeare.mit.edu/hamlet/full.html");
            Console.WriteLine("JUST TO VERIFY STRING LOOKS GOOD");
            Console.WriteLine(s);

            // **************************************************************
            sw.Restart();
            int x = CheckString(s, searchfor);
            Console.WriteLine(x.ToString());
            DisplayStopWatchTimer(sw);

            // **************************************************************
            sw.Restart();
            x = CheckString2(searchfor, s);
            Console.WriteLine(x.ToString());
            DisplayStopWatchTimer(sw);

            // **************************************************************
            sw.Restart();
            x = CheckString3(s, searchfor);
            Console.WriteLine(x.ToString());
            DisplayStopWatchTimer(sw);


#if DEBUG

            Console.ReadLine();
#endif


        }

        private static void DisplayStopWatchTimer(System.Diagnostics.Stopwatch sw)
        {
            sw.Stop();
            Console.WriteLine("{0}", sw.ElapsedMilliseconds);
        }


        static int CheckString(string s, string searchfor)
        {
            //Count number of times something appears in a string
            // character by character method so will be slow....
            int count1 = 0;
            int lengthsearchstring = searchfor.Length;

            for (int i = 0; i < ((s.Length) - searchfor.Length); i++)
            {
                if (s.Substring(i, lengthsearchstring) == searchfor)
                {
                    count1++;
                }
            }
            return count1;

        }

        static int CheckString2(string s, string searchfor)
        {
            // using string replace to count occurences
            // located a method but not sure its a good way but cleaner than the first method.
            return (searchfor.Length - searchfor.Replace(s, "").Length) / s.Length;
        }

        static int CheckString3(string s, string searchfor)
        {
            // using Regular expressions
            return Regex.Matches(s, searchfor).Count;
        }


        /// <summary>
        /// Canned functionality to turn a HTML web page returned into a Text document        
        /// </summary>
        /// <param name="uRL">URL of web page to get content from</param>
        /// <returns></returns>
        private static string ExtractTextFromWebPage(string uRL)
        {
            HtmlToText convert = new HtmlToText();
            string s = convert.Convert(new System.Net.WebClient().DownloadString(uRL));
            return s;
        }

    }
}
