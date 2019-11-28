using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WhaitAll
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task<Thread>> tasks = new List<Task<Thread>>();

            for (int i = 0; i < 50; i++)
            {
                tasks.Add(Task.Run(Hello));
            }

            int index = Task.WaitAny(tasks.ToArray());
            Console.WriteLine("First index is: " + tasks[index].Result.ManagedThreadId);
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Finish");
        }

        static async Task<Thread> Hello()
        {
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            var rand = new Random();
            await Task.Delay(rand.Next(1000, 5000));
            //Console.WriteLine("First");
            return Thread.CurrentThread;
        }
    }
}
