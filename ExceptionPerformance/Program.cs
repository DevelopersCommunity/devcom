using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionPerformance
{
    class Program
    {
        static Func<string, bool> MethodToBenchmark = ValidateNumberWithException;

        static void Main(string[] args)
        {
            const int iterations = 100000;
            const string number = "123devcom";

            MethodToBenchmark(number);

            Stopwatch c = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
            {
                MethodToBenchmark(number);
            }

            c.Stop();
            Console.WriteLine(c.ElapsedMilliseconds + "ms");

            //Console.ReadLine();
        }

        private static bool ValidateNumberWithException(string number)
        {
            try
            {
                int.Parse(number);
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateNumberWithoutException(string number)
        {
            int value;
            return int.TryParse(number, out value);
        }
    }
}
