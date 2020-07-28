using System;
using System.Diagnostics.CodeAnalysis;

namespace EximiaCo
{
	class Program
	{
		static void Main(string[] args)
		{
			var fn1 = new FullName("Philipp", "Góes");
			var fn2 = fn1.WithLastName("G.");
			var fn3 = fn2.WithLastName("Góes");

			Console.WriteLine(fn1.Equals(fn2));
			Console.WriteLine(fn1.Equals(fn3));
		}
	}

	internal class FullName : IEquatable<FullName>
	{
		public string FirstName { get; }
		public string LastName { get; }

		public FullName(string firstName, string lastName) =>
			(FirstName, LastName) = (firstName, lastName);

		public bool Equals([AllowNull] FullName other)
		{
			if (other == null)
				return false;

			return
				FirstName == other.FirstName &&
				LastName == other.LastName;
		}

		public FullName WithFirstName(string firstName) =>
			new FullName(firstName, LastName);
		public FullName WithLastName(string lastName) =>
			new FullName(FirstName, lastName);
	}
}