using System.Collections.Generic;

namespace MultithreadingDemo
{
    public class Primes
    {
        
        public static void CalculatePrimes()
        {
            for (int i = 0; i < 100000; i++)
            {
                bool isPrime = true;

                for (int j = 2; j < i; j++)
                {
                    if(i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            }
        }
    }
}
