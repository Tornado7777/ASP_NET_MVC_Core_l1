using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class Sample03Lesson3FabricMethod
    {
        static void Main(string[] args)
        {
            var a = ProductFactory.Create<FirstProduct>();
            var b = ProductFactory.Create<SecondProduct>();

            Console.ReadKey(true);
        }
    }

    public abstract class Product
    {
        protected internal abstract void PostConstruction();
    }

    public class FirstProduct : Product
    {
        public FirstProduct()
        {

        }
        protected internal override void PostConstruction()
        {
            Console.WriteLine("... process first product ...."); ;
        }
    }
    public class SecondProduct : Product
    {
        public SecondProduct()
        {

        }
        public SecondProduct(int x)
        {

        }
        protected internal override void PostConstruction()
        {
            Console.WriteLine("... process second product ...."); ;
        }
    }

    public static class ProductFactory
    {
        public static T Create<T>() where T: Product, new()
        {
            T t = new T();
            t.PostConstruction();
            return t;
        }
    }
}
