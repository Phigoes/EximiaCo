using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ClassOrStruct___Version_1
{
	class Program
	{
		static void Main(string[] args)
		{
			const int numberOfPoints = 10_000_000;

			var points = new List<Point3>(numberOfPoints);
			for (int i = 0; i < numberOfPoints; i++)
			{
				points.Add(new Point3
				{
					X = i,
					Y = i,
					Z = i
				});
			}

			Console.WriteLine($"{points.Count} points created.");
			Console.WriteLine($"{Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} MB.");

			var before = GC.CollectionCount(0);
			var pointToFind = new Point3 { X = -1, Y = -1, Z = -1 };

			var sw = Stopwatch.StartNew();
			var contains = points.Contains(pointToFind);
			sw.Stop();

			Console.WriteLine($"Time .: {sw.ElapsedMilliseconds} ms");
			Console.WriteLine($"# Gen0: {GC.CollectionCount(0) - before}");

			Console.WriteLine("Press Any Key to Exit");
			Console.ReadLine();
		}

		public class Point3
		{
			public double X;
			public double Y;
			public double Z;

			public override bool Equals(object obj)
			{
				if (!(obj is Point3 other))
					return false;

				return
					Math.Abs(X - other.X) < 0.0001 &&
					Math.Abs(Y - other.Y) < 0.0001 &&
					Math.Abs(Z - other.Z) < 0.0001;
			}
		}
	}
}
