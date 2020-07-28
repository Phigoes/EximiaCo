using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace WebClient___Enumerator
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

		public static IEnumerable<string> DownloadContent(string[] urls)
			=> new EnumerableOfWebContents(urls);

		public class EnumerableOfWebContents : IEnumerable<string>
		{
			private readonly string[] _urls;
			public EnumerableOfWebContents(string[] urls)
				=> _urls = urls;
			public IEnumerator<string> GetEnumerator()
				=> new EnumeratorOfWebContents(_urls);
			IEnumerator IEnumerable.GetEnumerator()
				=> new EnumeratorOfWebContents(_urls);
		}

		public class EnumeratorOfWebContents : IEnumerator<string>
		{
			private readonly string[] _urls;
			private readonly WebClient _client;
			int index = -1;
			public EnumeratorOfWebContents(string[] urls)
			{
				_urls = urls;
				_client = new WebClient();
				_client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
			}

			private string _current;
			public string Current => _current;
			object IEnumerator.Current => _current;

			public void Dispose() => _client.Dispose();

			public bool MoveNext()
			{
				index++;
				if (index >= _urls.Length) return false;
				using var data = _client.OpenRead(_urls[index]);
				using var reader = new StreamReader(data);

				_current = reader.ReadToEnd();
				return true;
			}

			public void Reset() => index = -1;
		}

		public static IEnumerable<string> GetImageSources(IEnumerable<string> documents)
			=> new EnumerableOfImageSources(documents);

		public class EnumerableOfImageSources : IEnumerable<string>
		{
			private readonly IEnumerable<string> _documents;
			public EnumerableOfImageSources(IEnumerable<string> documents)
				=> _documents = documents;
			public IEnumerator<string> GetEnumerator()
				=> new EnumeratorOfImageSources(_documents);
			IEnumerator IEnumerable.GetEnumerator()
				=> new EnumeratorOfImageSources(_documents);
		}

		public class EnumeratorOfImageSources : IEnumerator<string>
		{
			private readonly IEnumerable<string> _documents;
			private readonly IEnumerator<string> _enumeratorOfDocuments;

			public EnumeratorOfImageSources(IEnumerable<string> documents)
			{
				_documents = documents;
				_enumeratorOfDocuments = documents.GetEnumerator();
			}

			private string _current;
			public string Current => _current;
			object IEnumerator.Current => _current;

			public void Dispose() => _enumeratorOfDocuments.Dispose();

			private HtmlNodeCollection _nodes;
			private int _inodes;
			public bool MoveNext()
			{
				if (_nodes == null || _inodes >= _nodes.Count)
				{
					if (!_enumeratorOfDocuments.MoveNext()) return false;

					var doc = new HtmlDocument();
					doc.LoadHtml(_enumeratorOfDocuments.Current);
					_nodes = doc.DocumentNode.SelectNodes(@"//img[@src]");
					_inodes = 0;
				}

				HtmlAttribute att = _nodes[_inodes].Attributes["src"];
				_current = att.Value;
				_inodes++;
				return true;
			}

			public void Reset()
			{
				_enumeratorOfDocuments.Reset();
				_nodes = null;
			}
		}
	}
}
