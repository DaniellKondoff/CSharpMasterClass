using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoops
{
    class Program
    {
        static ConcurrentDictionary<int, List<int>> dict = new ConcurrentDictionary<int, List<int>>();
        static ConcurrentDictionary<int, List<string>> dict2 = new ConcurrentDictionary<int, List<string>>();

        static void Main(string[] args)
        {
            var nums = Enumerable.Range(0, 100).ToList();
            var partitioner = Partitioner.Create(0, 100);

            Parallel.ForEach(partitioner, (startEnd) =>
            {
                var id = Thread.CurrentThread.ManagedThreadId;
                if (!dict.ContainsKey(id))
                {
                    dict.TryAdd(id, new List<int>());
                }

                if (!dict2.ContainsKey(id))
                {
                    dict2.TryAdd(id, new List<string>());
                }

                //dict[id].Add(el);
                dict2[id].Add(startEnd.Item1 + " : " + startEnd.Item2);
            });

            foreach (var item in dict)
            {
                item.Value.Sort();
                Console.WriteLine($"Thread {item.Key}: {string.Join(",", item.Value)}");
            }
        }

        private static void ParrlalFors()
        {
            var list = Enumerable.Range(0, 1000000).ToList();
            Stopwatch watch = Stopwatch.StartNew();

            foreach (var item in list)
            {
                for (int i = 0; i < 100; i++)
                {
                    var num = 5;
                }
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);

            watch.Reset();
            watch.Start();

            Parallel.ForEach(list, (el) =>
            {
                for (int i = 0; i < 100; i++)
                {
                    var num = 5;
                }
            });

            Parallel.For(0, list.Count, (j) =>
            {
                for (int i = 0; i < 100; i++)
                {
                    var num = 5;
                }
            });

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}
