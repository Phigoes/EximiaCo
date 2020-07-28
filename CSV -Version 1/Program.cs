using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace CSV__Version_1
{
	class Program
	{
		static void Main(string[] args)
		{
			var before2 = System.GC.CollectionCount(2);
			var before1 = System.GC.CollectionCount(1);
			var before0 = System.GC.CollectionCount(0);

			var sw = new Stopwatch();
			sw.Start();

			Run(@"C:\Users\phi_g\Downloads\ml-25m\ratings.csv");

			sw.Stop();

			Console.WriteLine($"\nTime .: {sw.ElapsedMilliseconds} ms"); 
			Console.WriteLine($"# Gen0: {System.GC.CollectionCount(0) - before0}");
			Console.WriteLine($"# Gen1: {System.GC.CollectionCount(1) - before1}");
			Console.WriteLine($"# Gen2: {System.GC.CollectionCount(2) - before2}");
			Console.WriteLine($"Memory: {Process.GetCurrentProcess().WorkingSet64 / 1024  / 1024} MB");
		}

		public static void Run(string filePath)
		{
			var lines = File.ReadAllLines(filePath);
			var sum = 0d;
			var count = 0;

			foreach (var line in lines)
			{
				var parts = line.Split(',');

				if (parts[1] == "110")
				{
					sum += double.Parse(parts[2], CultureInfo.InvariantCulture);
					count++;
				}
			}

			Console.WriteLine($"Average rate for BraveHeart is {sum / count} ({count}) votes.");
		}
	}
}
