using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CSV___Version_4
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

			var lookingFor = Encoding.UTF8.GetBytes("110").AsSpan();
			var rawBuffer = new byte[1024 * 1024];
			using (var fs = File.OpenRead(filePath))
			{
				var bytesBuffered = 0;
				var bytesConsumed = 0;

				while (true)
				{
					var bytesRead = fs.Read(rawBuffer, bytesBuffered, rawBuffer.Length - bytesBuffered);

					if (bytesRead == 0) break;
					bytesBuffered += bytesRead;

					int linePosition;

					do
					{
						linePosition = Array.IndexOf(rawBuffer, (byte)'\n', bytesConsumed, bytesBuffered - bytesConsumed);

						if (linePosition >= 0)
						{
							var lineLength = linePosition - bytesConsumed;
							var line = new Span<byte>(rawBuffer, bytesConsumed, lineLength);
							bytesConsumed += lineLength + 1;

							var span = line.Slice(line.IndexOf((byte)',') + 1);

							var firstCommaPos = span.IndexOf((byte)',');
							var movieId = span.Slice(0, firstCommaPos);
							if (!movieId.SequenceEqual(lookingFor)) continue;

							span = span.Slice(firstCommaPos + 1);
							firstCommaPos = span.IndexOf((byte)',');
							var rating = double.Parse(Encoding.UTF8.GetString(span.Slice(0, firstCommaPos)));

							sum += rating;
							count++;
						}
					} while (linePosition >= 0);

					Array.Copy(rawBuffer, bytesConsumed, rawBuffer, 0, (bytesBuffered - bytesConsumed));
					bytesBuffered -= bytesConsumed;
					bytesConsumed = 0;
				}
			}
			
			Console.WriteLine($"Average rate for BraveHeart is {sum / count} ({count}) votes.");
		}
	}
}
