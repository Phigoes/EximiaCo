using System;
using System.Collections.Generic;
using System.Text;

namespace GC.ImpactsCNPJ
{
	class Version2
	{
		static int[] MULTIPLICADOR1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
		static int[] MULTIPLICADOR2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

		public static bool ValidarCNPJ(string cnpj)
		{
			if (string.IsNullOrWhiteSpace(cnpj))
				return false;

			Span<int> tempCnpj = stackalloc int[14];
			int pos = 0;
			for (int i = 0; i < cnpj.Length; i++)
			{
				if (char.IsDigit(cnpj[i]))
				{
					if (pos > 13) return false;
					tempCnpj[pos] = cnpj[i] - '0';
					pos++;
				}
			}
			if (pos != 14) return false;

			int soma;
			int resto;

			var todosIguais = true;
			for (int i = 1; i < tempCnpj.Length; i++)
			{
				if (tempCnpj[i] != tempCnpj[i - 1])
				{
					todosIguais = false;
					break;
				}
			}
			if (todosIguais) return false;

			soma = 0;

			for (int i = 0; i < 12; i++)
				soma += tempCnpj[i] * MULTIPLICADOR1[i];

			resto = soma % 11;

			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;

			int digito1 = resto;

			soma = 0;

			for (int i = 0; i < 12; i++)
				soma += tempCnpj[i] * MULTIPLICADOR2[i];
			soma += digito1 * MULTIPLICADOR2[12];

			resto = soma % 11;

			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;

			return tempCnpj[12] == digito1 && tempCnpj[13] == resto;
		}
	}
}
