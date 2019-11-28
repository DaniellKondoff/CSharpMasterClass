using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedular
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskFactory = new TaskFactory(new SImpleSchedular());

            for (int i = 0; i < 10; i++)
            {
                taskFactory.StartNew(() =>
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                });
            }

            Console.ReadLine();
        }
    }
}
