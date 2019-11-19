using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MultithreadingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started from: " + Thread.CurrentThread.ManagedThreadId);
            //new Thread(PrintNumbers).Start();

            int times = 8;
            Console.WriteLine("Sequance");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < times; i++)
            {
                Primes.CalculatePrimes();
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            sw.Reset();
            Console.WriteLine("using threds");
            sw.Start();

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < times; i++)
            {
                threads.Add(new Thread(Primes.CalculatePrimes));
                threads[i].Start();
               //new Thread(Primes.CalculatePrimes).Start();
            }
            threads.ForEach((t) => t.Join());
            
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        public static  void PrintNumbers()
        {
            Console.WriteLine("Print");
            Console.WriteLine("Printed from: " + Thread.CurrentThread.ManagedThreadId);
        }

        
    }
}
