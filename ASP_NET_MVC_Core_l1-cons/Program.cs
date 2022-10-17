using System;
using System.Threading;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(Print));

            thread1.Name = "#Test1";
            thread1.Start();

            Thread thread2 = new Thread(delegate () 
            {
                Console.WriteLine($"'delegate' Invoke in thread  {Thread.CurrentThread.Name}");
            });

            thread2.Name = "#Test2";
            thread2.Start();

            Thread thread3 = new Thread(() =>
            {
                Console.WriteLine($"'arrow function' invoke in thread  {Thread.CurrentThread.Name}");
            });

            thread3.Name = "#Test3";
            thread3.Start();

            Thread thread4 = new Thread(() =>            
                Console.WriteLine($"'arrow function' invoke in thread {Thread.CurrentThread.Name}")
            );

            thread4.Name = "#Test4";
            thread4.Start();


            Thread thread5 = new Thread(Sum);
            thread5.Name = "#Test5";
            thread5.Start(new int[] {2, 5});
        }

        static void Print()
        {
            Console.WriteLine($"Print() invoke in thread {Thread.CurrentThread.Name}");
        }

        static void Sum(object o)
        {
            if (o != null && o is int[] && ((int[])o).Length > 1)
            {
                var arr = ((int[])o);
                Console.WriteLine($"Sum ({arr[0]} + {arr[1]}) =  {arr[0] + arr[1]} invoke in thread {Thread.CurrentThread.Name}");
            }
        }
       
    }
}
