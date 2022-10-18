using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class Sample02
    {
        public class CommonResource
        {
            public int A { get; set; } = 0;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Application start ....");
            CommonResource resource = new CommonResource();
            Task1(resource);
            Task2(resource);
            Task3(resource);
            Console.WriteLine("Application end ....");

        }

        static void Task1(CommonResource resource)
        {
            for (int j = 0; j < 5; j++)
            {
                Thread t = new Thread((object o) =>
                {
                    if (o != null && o is CommonResource)
                    {
                        var commonResource = (CommonResource)o;
                        lock (commonResource)
                        {
                            commonResource.A = 1;
                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine($"res = {commonResource.A}; invoke in thread {Thread.CurrentThread.Name}");
                                commonResource.A++;
                                Thread.Sleep(100);
                            }
                        }
                        
                    }
                });
                t.Name = $"#{j + 1}";
                t.Start(resource);
            }
        }

        static void Task2(CommonResource resource)
        {
            for (int j = 0; j < 5; j++)
            {
                Thread t = new Thread((object o) =>
                {
                    if (o != null && o is CommonResource)
                    {
                        var commonResource = (CommonResource)o;
                        bool lockTacken = false;
                        try
                        {
                            Monitor.Enter(commonResource, ref lockTacken);
                            commonResource.A = 1;
                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine($"res2 = {commonResource.A}; invoke in thread {Thread.CurrentThread.Name}");
                                commonResource.A++;
                                Thread.Sleep(100);
                            }
                        }
                        finally
                        {
                            if(lockTacken)
                            Monitor.Exit(commonResource);
                        }  
                    }
                });
                t.Name = $"#{j + 1}";
                t.Start(resource);
            }
        }

        static void Task3(CommonResource resource)
        {
            AutoResetEvent waitHandler = new AutoResetEvent(true); //сиганльный режим вкл true 

            for (int j = 0; j < 5; j++)
            {
                Thread t = new Thread((object o) =>
                {
                    if (o != null && o is CommonResource)
                    {
                        var commonResource = (CommonResource)o;

                        waitHandler.WaitOne();

                        commonResource.A = 1;
                        for (int i = 0; i < 5; i++)
                        {
                            Console.WriteLine($"res3 = {commonResource.A}; invoke in thread {Thread.CurrentThread.Name}");
                            commonResource.A++;
                            Thread.Sleep(100);
                        }
                        waitHandler.Set();
                    }
                });
                t.Name = $"#{j + 1}";
                t.Start(resource);
            }
        }

        static void Task4 ()
        {
            AutoResetEvent waitHandler = new AutoResetEvent(false);
            Thread t = new Thread(() =>
            {
                Console.WriteLine($"Invoke in thread {Thread.CurrentThread.Name}");
                Thread.Sleep(3000);
                waitHandler.Set();
            });
            t.Start();
            waitHandler.WaitOne();
        }
    }
}
