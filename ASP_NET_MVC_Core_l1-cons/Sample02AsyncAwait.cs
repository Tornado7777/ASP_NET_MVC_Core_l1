using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class Sample02AsyncAwait
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Application start ....");
            var res = await DoProcess(20);
            Console.WriteLine("Application terminate ....");
            Console.ReadKey(true);
        }

        static async Task<ProcessResult> DoProcess(int Count)
        {
            Console.WriteLine("Start some operation ...");
            Console.WriteLine("Process some operation ...");
            await Task.Delay(3000);
            Console.WriteLine("Process complete ...");
            return new ProcessResult();
        }
    }

    class ProcessResult
    {

    }
}
