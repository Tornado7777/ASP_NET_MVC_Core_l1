using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class ThreadControl
    {
        public bool IsActive { get; set; }
    }
    internal class Sample01
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Application start ....");
            Task1();
            Console.WriteLine("Application end ....");

        }

        static void Task1()
        {
            var threadControl = new ThreadControl { IsActive = true };
            Thread t = new Thread((object o) =>
            {
                if (o != null && o is ThreadControl)
                {
                    var threadController = (ThreadControl)o;

                    while (threadController.IsActive)
                    {
                        try
                        {
                            Console.WriteLine($"Invoke in thread {Thread.CurrentThread.Name}");
                            Thread.Sleep(50000);
                        }
                        catch (ThreadInterruptedException e) 
                        {
                            Console.WriteLine($"Error {e.Message}");
                        }
                    }
                }
            });
            t.Name = "#TestThread";
            t.Start(threadControl);

            Console.ReadKey(true);
            Thread.Sleep(5000);
            threadControl.IsActive = false;
            t.Interrupt();
        }
    }
}
