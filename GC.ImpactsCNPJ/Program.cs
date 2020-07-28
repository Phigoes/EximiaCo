using System;
using System.Diagnostics;

namespace GC.ImpactsCNPJ
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = new Stopwatch();
			var before2 = System.GC.CollectionCount(2);
			var before1 = System.GC.CollectionCount(1);
			var before0 = System.GC.CollectionCount(0);
			Func<string, bool> sut = Version3.ValidarCNPJ;

			sw.Start();
			for (int i = 0; i < 1_000_000; i++)
			{
				if (!sut("22.006.951/0001-00"))
					throw new Exception("Error!");

				if (sut("22.006.951/0001-01"))
					throw new Exception("Error!");
			}
			sw.Stop();

			Console.WriteLine($"\nTime .: {sw.ElapsedMilliseconds}ms");
			Console.WriteLine($"# Gen0: {System.GC.CollectionCount(0) - before0}");
			Console.WriteLine($"# Gen1: {System.GC.CollectionCount(1) - before1}");
			Console.WriteLine($"# Gen2: {System.GC.CollectionCount(2) - before2}");
			Console.WriteLine($"Memory: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} MB");
		}
	}
}
