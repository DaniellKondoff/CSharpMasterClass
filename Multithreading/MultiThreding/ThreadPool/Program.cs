using System;
using System.Threading;

namespace ThreadPoool
{
    class Program
    {
        static void Main(string[] args)
        {
            //int max, num;
            //ThreadPool.GetAvailableThreads(out max, out num);
            //Console.WriteLine("Available Threads: " + max);
            //ThreadPool.GetMinThreads(out max, out num);
            //Console.WriteLine("Min Threads: " + max);
            //ThreadPool.GetMaxThreads(out max, out num);
            //Console.WriteLine("Max Threads: " + max);

            ThreadPool.QueueUserWorkItem((object obj) =>
            {
                Print();
                
            });
        }

        public static void Print()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
