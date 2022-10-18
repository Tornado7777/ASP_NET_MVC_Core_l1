using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class Sample01ThreadPool
    {
        public class CommonResource
        {
            public int A { get; set; } = 0;
        }

        public class ThreadControl
        {
            public CommonResource CommonResource { get; set; }

            public AutoResetEvent WaitHandle { get; set; }

            public ThreadControl (CommonResource commonResource, AutoResetEvent waitHandle)
            {
                CommonResource = commonResource;
                WaitHandle = waitHandle;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Application start ....");

            AutoResetEvent[] waitHandles = new AutoResetEvent[10];

            CommonResource commonResource = new CommonResource();

            for(int i = 0; i < waitHandles.Length; i++)
            {
                waitHandles[i] = new AutoResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(Task), new ThreadControl(commonResource, waitHandles[i]));
            }

            Console.WriteLine("All tasks queud.");
            WaitHandle.WaitAll(waitHandles);

            int workerThreads; // общее возможное кол-во потоков
            int completionPortThreads; // общее возможное кол-во потоков в рамках неуправляемых ресурсов (файл ...)
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            Console.WriteLine($"Общее возможное кол-во потоков: {workerThreads}");
            Console.WriteLine($"Общее возможное кол-во потоков в рамках неуправляемых ресурсов (файл ...): {workerThreads}");

            Console.WriteLine("Application terminate ....");
            Console.ReadKey(true);
        }

        static void Task(object o)
        {
            if(o != null && o is ThreadControl)
            {
                var threadControl = (ThreadControl)o;
                lock(threadControl.CommonResource)
                {
                    threadControl.CommonResource.A = 1;
                    for (int i= 0; i < 5; i++)
                    {
                        Console.WriteLine($"res = {threadControl.CommonResource.A}; invoke int thread {Thread.CurrentThread.ManagedThreadId}");
                        threadControl.CommonResource.A++;
                        Thread.Sleep(100);
                    }

                    threadControl.WaitHandle.Set();
                }
            }
        }
    }
}
