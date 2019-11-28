using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(Hello).Start();
            Task.Run(Hello);

            var task = Task.Run(() =>
            {
                return 5;
            });

            task.Wait();
            Console.WriteLine(task.Result);
        }

        static void Hello()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Hello");
        }
    }
}
