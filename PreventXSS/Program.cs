using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace PreventXSS
{
    /// <summary>
    /// Main Program to test PreventXSS Code
    /// </summary>
    public class Program
    {
        private static List<string> validHtmlTags = new List<string>{"p","/p","strong","/string","ul","/ul","b","/b","em","/em"};

        /// <summary>
        /// Main Calling Code - Entry point into the application
        /// </summary>
        /// <param name="args">Command Line Parameters - Not Used</param>
        public static void Main(string[] args)
        {        
            var source = "<b>hello</b><script>evil</script>";

            // Search string for all the occurences of <
            // If it then is followed by something on the white list then leave
            // if it isnt then find the next > and change these to %gt and &lt
            // “<b>hello</b>&lt;script&gt;evil&lt/script&gt;”
            var s = SanitizeHTML_Core.SanitizeHTML(source, validHtmlTags);

            Console.WriteLine("RESULT");
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}