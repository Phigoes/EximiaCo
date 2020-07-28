using System;
using System.Diagnostics;

namespace Paralelismo___Version_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = new Stopwatch();
			sw.Start();
			var result = PrimeInRange(200, 800000);
			sw.Stop();
			Console.WriteLine($"{result} prime numbers found in {sw.ElapsedMilliseconds / 1000} seconds ({Environment.ProcessorCount})");
		}

		public static long PrimeInRange(long start, long end)
		{
			long result = 0;
			for (long number = start; number < end; number++)
			{
				if (IsPrime(number))
				{
					result++;
				}
			}

			return result;
		}

		static bool IsPrime(long number) 
		{
			if (number == 2) return true;
			if (number % 2 == 0) return false;
			for (long divisor = 3; divisor < (number / 2); divisor+=2)
			{
				if (number % divisor == 0)
					return false;
			}

			return true;
		}
	}
}
