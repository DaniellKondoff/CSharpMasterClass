using System;
using System.Collections.Generic;

namespace MultithreadingDemo
{
    public class Primes
    {
        public List<int> primes = new List<int>();
        public object obj = new object();
        public void CalculatePrimes(object data)
        {
            int start, end;
            var input = (Tuple<int, int>)data;
            start = input.Item1;
            end = input.Item2;

            for (int i = start; i < end; i++)
            {
                bool isPrime = true;

                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (!isPrime)
                {
                    lock (obj)
                    {
                        primes.Add(i);
                    }  
                }
            }
        }
    }
}
