using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Day4
{
    class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)"), 
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "args")]
        static void Main(string[] args)
        {

                callDoSomething();

                Console.WriteLine("Completed");

                 Console.ReadLine();
     

        }
        async static  void callDoSomething()
        {
            await DoSomething();
            int x = await DoSomething2();          
        }

        public async static Task DoSomething()
        {
            var client = new HttpClient();
            var page = await client.GetStringAsync("http://www.microsoft.com");
            Console.WriteLine(page);
        }
 
        public async static Task<int> DoSomething2()
        {
            var client = new HttpClient();
            var page = await client.GetStringAsync("http://www.microsoft.com");
            Console.WriteLine(page);

            return 1;


        }
    }
}
