using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsynAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //Task.Run(() => DoWorkAsync());

            Task task = DoWorkAsync();
            task.ContinueWith((x) =>
            {
                Console.WriteLine("Over");
            });

            Console.ReadLine();
        }

        static async Task DoWorkAsync()
        {
            Console.WriteLine("Thread Before Await: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() => DoWork());
           
            Console.WriteLine("Thread After Await: " + Thread.CurrentThread.ManagedThreadId);  
        }

        static void DoWork()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Thread in DoWork: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
