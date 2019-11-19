using System;
using System.Threading;

namespace MutexDemo
{
    class Program
    {
        //static Mutex mutex = new Mutex(false, "TestMutex");
        static void Main(string[] args)
        {
            Console.WriteLine("Program started");
            new Thread(FromProcesses).Start();
        }

        public static void FromProcesses()
        {
            Mutex mutex;
            Mutex.TryOpenExisting("TestMutex", out mutex);

            if (mutex == null)
            {
                mutex = new Mutex(true, "TestMutex");
            }
            else
            {
                mutex.WaitOne();
            }
            Console.WriteLine("Inside of Mutex");
            Console.ReadLine();
            mutex.ReleaseMutex();
            Console.WriteLine("Mutex Released");
        }
    }
}
