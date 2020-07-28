using System;
using System.Diagnostics;
using System.IO;

namespace CSV___Version_3
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
			Console.WriteLine($"Memory: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} MB");
		}

		public static void Run(string filePath)
		{
			var sum = 0d;
			var count = 0;
			string line;

			var lookingFor = "110".AsSpan();

			using (var fs = File.OpenRead(filePath))
			using (var reader = new StreamReader(fs))
			while ((line = reader.ReadLine()) != null)
			{
				var span = line.AsSpan(line.IndexOf(',') + 1);

				var firstCommaPos = span.IndexOf(',');
				var movieId = span.Slice(0, firstCommaPos);
				if (!movieId.SequenceEqual(lookingFor)) continue;

				span = span.Slice(firstCommaPos + 1);
				firstCommaPos = span.IndexOf(',');
				var rating = double.Parse(span.Slice(0, firstCommaPos));

				sum += rating;
				count++;
			}

			Console.WriteLine($"Average rate for BraveHeart is {sum / count} ({count}) votes.");
		}
	}
}
