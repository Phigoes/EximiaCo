using System;
using System.Diagnostics;

namespace GC.Impacts
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = new Stopwatch();
			var before2 = System.GC.CollectionCount(2);
			var before1 = System.GC.CollectionCount(1);
			var before0 = System.GC.CollectionCount(0);
			Func<string, bool> sut = Version5.ValidarCPF;

			sw.Start();
			for (int i = 0; i < 1_000_000; i++)
			{
				if (!sut("771.189.500-33"))
					throw new Exception("Error!");

				if (sut("771.189.500-34"))
					throw new Exception("Error!");
			}
			sw.Stop();

			Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
			Console.WriteLine($"GC Gen #2 : {System.GC.CollectionCount(2) - before2}");
			Console.WriteLine($"GC Gen #1 : {System.GC.CollectionCount(1) - before1}");
			Console.WriteLine($"GC Gen #0 : {System.GC.CollectionCount(0) - before0}");
			Console.WriteLine("Done");
		}
	}
}
