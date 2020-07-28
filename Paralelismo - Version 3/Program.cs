using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Paralelismo___Version_3
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
			var range = end - start;
			var numberOfThreads = (long)Environment.ProcessorCount;

			var threads = new Thread[numberOfThreads];
			var results = new long[numberOfThreads];

			var chunckSize = range / numberOfThreads;

			for (long i = 0; i < numberOfThreads; i++)
			{
				var chunkStart = start + i * chunckSize;
				var chunkEnd = (i == (numberOfThreads - 1)) ? end : chunkStart + chunckSize;
				var current = i;

				threads[i] = new Thread(() =>
				{
					results[current] = 0;
					for (long number = chunkStart; number < chunkEnd; number++)
					{
						if (IsPrime(number))
						{
							results[current]++;
						}
					}
				});

				threads[i].Start();
			}

			foreach (var thread in threads)
			{
				thread.Join();
			}

			return results.Sum();
		}

		static bool IsPrime(long number)
		{
			if (number == 2) return true;
			if (number % 2 == 0) return false;
			for (long divisor = 3; divisor < (number / 2); divisor += 2)
			{
				if (number % divisor == 0)
					return false;
			}

			return true;
		}
	}
}
