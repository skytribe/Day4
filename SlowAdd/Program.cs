using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowAdd
{
    /// <summary>
    /// Main entry Point
    /// </summary>
    public class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "args")]
        public static void Main(string[] args)
        {
            CallSlowAdd();

            // CallSlowAdd2().Wait();
            Console.WriteLine("Completed");
            Console.ReadLine();
        }

       public static async void CallSlowAdd()
        {
            var a = await SlowAdd(1, 2);
            Console.WriteLine(a);
        }

        public static async Task<int> SlowAdd(int x, int y)
        {
            await Task.Delay(5000);   // Blocking to create async scenario of slowing the down
            return x + y;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static async Task CallSlowAdd2()
        {
            var a = await SlowAdd(1, 2);
            Console.WriteLine(a);
        }
    }
}
