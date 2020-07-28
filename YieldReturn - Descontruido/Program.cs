using System;
using System.Collections;
using System.Collections.Generic;

namespace YieldReturn___Descontruido
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Antes de chamar 'Foo'...!");
			var foo = Foo();
			Console.WriteLine("Depois de chamar 'Foo'...!");

			using (var enumerator = foo.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					var elem = enumerator.Current;
					Console.WriteLine($"Antes de imprimir 'elem' {elem}...");
					Console.WriteLine(elem);
					Console.WriteLine($"Depois de imprimir 'elem' {elem}...");
				}
			}
		}

		public static IEnumerable<int> Foo() => new MyEnumerable();

		public class MyEnumerable : IEnumerable<int>, IDisposable
		{
			public void Dispose() { }

			public IEnumerator<int> GetEnumerator() => new MyEnumerator();

			IEnumerator IEnumerable.GetEnumerator() => new MyEnumerator();
		}

		public class MyEnumerator : IEnumerator<int>
		{
			public MyEnumerator()
			{
				Console.WriteLine("Antes de iniciar o 'loop for'...");
			}
			int _current = -1;
			public int Current => _current;

			object IEnumerator.Current => _current;

			public void Dispose()
			{
				Console.WriteLine("Depois de encerrar 'loop for'...");
			}

			public bool MoveNext()
			{
				if (_current >= 0)
				{
					Console.WriteLine($"Depois do 'yield return {_current}'...");
				}
				if (_current >= 4) return false;
				Console.WriteLine($"Antes do 'yield return {_current}'...");
				_current++;
				return true;

			}

			public void Reset()
			{
				_current = -1;
			}
		}
	}
}
