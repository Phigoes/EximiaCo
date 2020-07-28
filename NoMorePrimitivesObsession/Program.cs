using System;

namespace NoMorePrimitivesObsession
{
	class Program
	{
		static void Main(string[] args)
		{
			Cpf cpf = "574.649.437-24";
			Console.WriteLine($"CPF: {cpf}");

			var e = new Employee
			{
				Cpf = "574.649.437-24"
			};
			Console.WriteLine($"CPF: {e.Cpf}");
		}

		public class Employee
		{
			public string Id { get; set; }
			public string FullName { get; set; }
			public Cpf Cpf { get; set; }
		}

		public struct Cpf
		{
			private readonly string _value;
			private Cpf(string value)
			{
				_value = value;
			}

			public static Cpf Parse(string value)
			{
				if (TryParse(value, out var result))
				{
					return result;
				}
				throw new ArgumentException("Erro");
			}

			public static bool TryParse(string value, out Cpf cpf)
			{
				cpf = new Cpf(value);
				return Helper.ValidarCPF(value);
			}

			public override string ToString()
				=> _value;

			public static implicit operator Cpf(string input)
				=> Parse(input);
		}
	}
}
