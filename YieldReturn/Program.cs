using System;
using System.Collections;
using System.Collections.Generic;

namespace YieldReturn
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Antes de chamar 'Foo'...!");
			var foo = Foo();
			Console.WriteLine("Depois de chamar 'Foo'...!");

			foreach (var elem in foo)
			{
				Console.WriteLine($"Antes de imprimir 'elem' {elem}...");
				Console.WriteLine(elem);
				Console.WriteLine($"Depois de imprimir 'elem' {elem}...");
			}
		}

		public static IEnumerable<int> Foo()
		{
			Console.WriteLine("Antes de iniciar o 'loop for'...");
			for (int i = 0; i < 4; i++)
			{
				Console.WriteLine($"Antes do 'yield return {i}'...");
				yield return i;
				Console.WriteLine($"Depois do 'yield return {i}'...");
			}
			Console.WriteLine("Depois de encerrar 'loop for'...");
		}
	}
}
