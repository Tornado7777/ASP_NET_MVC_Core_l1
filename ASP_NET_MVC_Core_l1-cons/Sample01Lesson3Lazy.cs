using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class Sample01Lesson3Lazy
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(o =>
                {
                    lock(LazySingleton.Instance)
                    {
                        LazySingleton.Instance.Counter = 0;
                        for(int j = 0; j < 10; j++)
                        {
                            Console.Write($"{++LazySingleton.Instance.Counter}; ");
                            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
                        }
                    }
                }));
            }
            Console.ReadKey(true);
        }
    }
    public sealed class LazySingleton
    {
        private static readonly Lazy<LazySingleton> _instance =
        new Lazy<LazySingleton>(() => new LazySingleton(), true);
        public int Counter { get; set; } = 1;

        LazySingleton() { }
        public static LazySingleton Instance { get { return _instance.Value; } }
    }
}
