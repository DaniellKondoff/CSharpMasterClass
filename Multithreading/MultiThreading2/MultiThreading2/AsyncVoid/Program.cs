using System;
using System.Threading.Tasks;

namespace AsyncVoid
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ThrowException().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Task.Run(async () =>
            {
                await ThrowException();
            });
        }

        static async Task ThrowException()
        {
            throw new ArgumentException();
        }
    }
}
