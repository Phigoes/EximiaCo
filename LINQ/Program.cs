using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace WebClient___Original
{
	class Program
	{
		static void Main(string[] args)
		{
			var sw = new Stopwatch();
			sw.Start();

			var urls = new string[]
			{
				"https://elemarjr.com/pt",
				"https://eximia.co/pt",
				"https://eximia.tech/pt",
				"https://eximia.ms/pt",
				"https://microsoft.com",
			};

			var pages = DownloadContent(urls);
			var srcs = GetImageSources(pages);

			foreach (var result in srcs)
			{
				Console.WriteLine($"Time: {sw.ElapsedMilliseconds / 1000.0} seg.");
				Console.WriteLine(result);
			}
		}

		private static string[] DownloadContent(string[] urls)
		{
			var result = new string[urls.Length];

			var client = new WebClient();
			client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

			for (int i = 0; i < urls.Length; i++)
			{
				using var data = client.OpenRead(urls[i]);
				using var reader = new StreamReader(data);

				result[i] = reader.ReadToEnd();
			}

			return result;
		}
		private static List<string> GetImageSources(string[] documents)
		{
			var result = new List<string>();

			for (int i = 0; i < documents.Length; i++)
			{
				var doc = new HtmlDocument();
				doc.LoadHtml(documents[i]);
				var nodes = doc.DocumentNode.SelectNodes(@"//img[@src]");

				foreach (var img in nodes)
				{
					HtmlAttribute att = img.Attributes["src"];
					result.Add(att.Value);
				}
			}

			return result;
		}
	}
}
