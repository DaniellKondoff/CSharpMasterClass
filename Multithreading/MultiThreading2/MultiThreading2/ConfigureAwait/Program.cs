using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigureAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(Hello);
        }

        static async Task Hello()
        {
            await DoLongWork().ConfigureAwait(false);
        }

        static async Task DoLongWork()
        {
            Console.WriteLine($"Work starting on thred: {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(1000);
            Console.WriteLine($"Work ending on thred: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
