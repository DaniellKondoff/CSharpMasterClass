using System;
using System.Threading;

namespace RaceConditionDemo
{
    class Program
    {
        static int nums = 0;
        static object obj = new object();

        static void Main(string[] args)
        {
            for (int i = 0; i < 8; i++)
            {
                new Thread(Increment).Start();
            }

            Console.ReadLine();
            Console.WriteLine(nums);
        }

        static void Increment()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (obj)
                {
                    nums++;
                }
            }

            Console.WriteLine("Finished");
        }
    }
}
