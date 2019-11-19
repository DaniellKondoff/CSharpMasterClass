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
            var primes = new Primes();

            Console.WriteLine("Sequance");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < times; i++)
            {
                primes.CalculatePrimes(new Tuple<int, int>(i * 1000, (i * 1000) + 1000));
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(primes.primes.Count);

            sw.Reset();
            Console.WriteLine("using threds");
            sw.Start();
            primes = new Primes();
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < times; i++)
            {
                //int j = i;
                var t = new Thread(primes.CalculatePrimes);
                t.Start(new Tuple<int, int>(i * 1000, (i * 1000) + 1000));
                threads.Add(t);
            }
            threads.ForEach((t) => t.Join());
            
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(primes.primes.Count);
        }

        public static  void PrintNumbers()
        {
            Console.WriteLine("Print");
            Console.WriteLine("Printed from: " + Thread.CurrentThread.ManagedThreadId);
        }

        
    }
}
