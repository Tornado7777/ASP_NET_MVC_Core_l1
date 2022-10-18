using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ASP_NET_MVC_Core_l1_cons
{
    /*
     * 1. Используя знания о пуле потоков, напишите свой микро пул с использованием новых структур
          данных.
     * 2. Добавьте в ваш пул настройку для максимального количества регистрируемых потоков или
          кидайте ошибку.
     */
    internal class hw2
    {
        public class CommonResource
        {
            public int A { get; set; } = 0;
        }

        public class ThreadControl
        {
            public CommonResource CommonResource { get; set; }

            public AutoResetEvent WaitHandle { get; set; }

            public readonly int _workerThreads = 0;// общее возможное кол-во потоков
            public readonly int _completionPortThreads = 0;// общее возможное кол-во потоков в рамках неуправляемых ресурсов (файл ...)

            public ThreadControl(CommonResource commonResource, AutoResetEvent waitHandle)
            {
                CommonResource = commonResource;
                WaitHandle = waitHandle;
                ThreadPool.GetAvailableThreads(out _workerThreads, out _completionPortThreads);
                if (_workerThreads == 0 || _completionPortThreads == 0)
                    throw new Exception($"Is not generate thread pool. _workerThreads = {_workerThreads}, _completionPortThreads = {_completionPortThreads} ");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Application start ....");

            AutoResetEvent[] waitHandles = new AutoResetEvent[10];

            CommonResource commonResource = new CommonResource();

            ThreadControl[] threadControls = new ThreadControl[10];

            for (int i = 0; i < waitHandles.Length; i++)
            {
                waitHandles[i] = new AutoResetEvent(false);
                ThreadPool.QueueUserWorkItem(new WaitCallback(Task), threadControls[i] = new ThreadControl(commonResource, waitHandles[i]));
                Console.WriteLine($"Общее возможное кол-во потоков для waitHandles[{i}]: {threadControls[i]._workerThreads}");
                Console.WriteLine($"Общее возможное кол-во потоков в рамках неуправляемых ресурсов (файл ...) для waitHandles[{i}]: {threadControls[i]._workerThreads}");
            }

            Console.WriteLine("All tasks queud.");
            WaitHandle.WaitAll(waitHandles);

            
            

            Console.WriteLine("Application terminate ....");
            Console.ReadKey(true);
        }

        static void Task(object o)
        {
            if (o != null && o is ThreadControl)
            {
                var threadControl = (ThreadControl)o;
                lock (threadControl.CommonResource)
                {
                    threadControl.CommonResource.A = 1;
                    for (int i = 0; i < 5; i++)
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
