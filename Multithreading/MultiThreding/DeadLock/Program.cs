using System;
using System.Collections.Generic;
using System.Threading;

namespace DeadLock
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Thread(DeadLock).Start();
            //new Thread(DeadLock2).Start();

            var act1 = new Account();
            var act2 = new Account();
            List<Thread> threads = new List<Thread>();

            var t1 = new Thread(() =>
            {
                Transfer(act1, act2);
            });
            t1.Start();

            var t2 = new Thread(() =>
            {
                Transfer(act2, act1);
            });
            t2.Start();

            threads.Add(t1);
            threads.Add(t2);

            threads.ForEach((t) => t.Join());

            Console.WriteLine(act1.amount);
            Console.WriteLine(act2.amount);
        }

        public static object first = new object();
        public static object second = new object();

        public static void DeadLock()
        {
            lock (first)
            {
                Console.WriteLine("In first");
                Thread.Sleep(1000);
                lock (second)
                {
                    Console.WriteLine("In second");
                }
            }
        }

        public static void DeadLock2()
        {
            lock (second)
            {
                Console.WriteLine("In second");
                Thread.Sleep(1000);
                lock (first)
                {
                    Console.WriteLine("In first");
                }
            }
        }

        public static void Transfer(Account first, Account second)
        {
            lock (first)
            {
                lock (second)
                {
                    first.amount += 50;
                    second.amount -= 50;
                }
            }
        }
    }

    public class Account
    {
        public decimal amount = 0;
    }
}
