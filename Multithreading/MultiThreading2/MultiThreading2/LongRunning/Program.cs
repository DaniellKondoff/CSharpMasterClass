using System;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunning
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; i++)
            {
                Task.Factory.StartNew(DoLongWoork, TaskCreationOptions.LongRunning);
                
            }

            Console.ReadLine();
        }

        static void DoLongWoork()
        {
            Console.WriteLine($"Work starting on thred: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(20000);
        }
    }
}
