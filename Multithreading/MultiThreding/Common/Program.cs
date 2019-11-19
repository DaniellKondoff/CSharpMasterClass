using System;
using System.Threading;

namespace Common
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            Console.WriteLine(st);
            new Thread(Hey).Start();
            new Thread(Throw).Start();
        }

        static void Hey()
        {
            Console.WriteLine("Hey from Thread");
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} \n Stack: {st}");
            Thread.Sleep(15000);
        }

        static void Throw()
        {
            try
            {
                throw new ArgumentException();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
