using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TasksPitfalls
{
    class Program
    {
        static void Main(string[] args)
        {
            var delta = GC.GetTotalMemory(false);
            Stopwatch w = new Stopwatch();
            w.Start();
            for (int i = 0; i < 100000; i++)
            {
                GetTestAsync();
            }
            w.Stop();
            Console.WriteLine(w.ElapsedMilliseconds);
            delta = GC.GetTotalMemory(false) - delta;
            Console.WriteLine(delta);
        }

        static async Task GetTestAsync()
        {

        }

        static void GetTest()
        {

        }
    }
}
