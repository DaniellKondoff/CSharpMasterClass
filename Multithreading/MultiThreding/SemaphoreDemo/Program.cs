using System;
using System.Threading;

namespace SemaphoreDemo
{
    class Program
    {
        static Semaphore semaphore = new Semaphore(3, 3);

        static void Main(string[] args)
        {
            while (true)
            {
                var t = new Thread(Bouncer);
                
                new Thread(Bouncer).Start();
            }
        }

        static void Bouncer()
        {
            semaphore.WaitOne();
            Console.WriteLine($"I am bouncing: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(2000);
            semaphore.Release();
            Console.WriteLine($"Ending Bouncing: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
