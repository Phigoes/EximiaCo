using System;
using System.Diagnostics;
using System.Threading;

namespace Paralelismo___Version_5
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
			const long chunckSize = 100;
			var completed = 0;
			var allDone = new ManualResetEvent(initialState: false);

			var chunks = (end - start) / chunckSize;

			for (long i = 0; i < chunks; i++)
			{
				var chunkStart = start + i * chunckSize;
				var chunkEnd = (i == (chunks - 1)) ? end : chunkStart + chunckSize;
				ThreadPool.QueueUserWorkItem(_ =>
				{
					for (long number = chunkStart; number < chunkEnd; number++)
					{
						if (IsPrime(number))
						{
							Interlocked.Increment(ref result);
						}
					}

					if (Interlocked.Increment(ref completed) == chunks)
					{
						allDone.Set();
					}
				});
			}

			allDone.WaitOne();

			return result;
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
