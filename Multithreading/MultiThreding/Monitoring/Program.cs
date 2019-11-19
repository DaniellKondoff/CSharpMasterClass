using System;
using System.Threading;

namespace Monitoring
{
    class Program
    {
        public static object obj = new object();
        static void Main(string[] args)
        {
            new Thread(Boss).Start();
            Thread.Sleep(300);
            new Thread(Workers).Start();
        }

        public static void Salvation()
        {
            Monitor.Enter(obj);
            Console.WriteLine("Locked");
            Monitor.Exit(obj);
        }

        public static void Boss()
        {
            while (true)
            {
                Monitor.Enter(obj);
                Console.WriteLine("I am inside boos");
                Thread.Sleep(1000);
                Console.WriteLine("boos starting to wait");
                Monitor.Wait(obj);
                Console.WriteLine("I am outside boos");
                Monitor.Exit(obj);
            }
        }

        public static void Workers()
        {
            while (true)
            {
                Monitor.Enter(obj);            
                Console.WriteLine("I am inside worker");
                Thread.Sleep(1000);
                Monitor.PulseAll(obj);
                Console.WriteLine("I am outside worker");
                Monitor.Exit(obj);
            }
        }
    }
}
