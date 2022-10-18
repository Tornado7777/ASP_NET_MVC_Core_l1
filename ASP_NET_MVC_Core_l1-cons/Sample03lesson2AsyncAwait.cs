using System;
using System.Threading;
using System.Threading.Tasks;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class Sample03lesson2AsyncAwait
    {
        static async Task Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            
            Console.WriteLine("Application start ....");
            cancellationTokenSource.CancelAfter(4000);
            try
            {
                var res = await DoProcess(-1, cancellationTokenSource);
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            Console.WriteLine("Application terminate ....");
            Console.ReadKey(true);
        }

        static async Task<ProcessResult> DoProcess(int count, CancellationTokenSource cancellationTokenSource)
        {
            if(count < 0) 
                throw new ArgumentException($"Parametr 'count'={count} cannot by less then zero.");
            Console.WriteLine("Start some operation ...");
            Console.WriteLine("Process some operation ...");
            for (int i = 0; i < count; i++)
            {
                if(cancellationTokenSource.IsCancellationRequested) 
                    break;
                await Task.Delay(1000);
            }
            Console.WriteLine("Process complete ...");
            return new ProcessResult();
        }
    }
}

