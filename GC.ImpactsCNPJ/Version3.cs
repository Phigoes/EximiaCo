using System;

namespace GC.ImpactsCNPJ
{
	class Version3
	{
		static int[] MULTIPLICADOR1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
		static int[] MULTIPLICADOR2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

		public static bool ValidarCNPJ(string cnpj)
		{
			if (string.IsNullOrWhiteSpace(cnpj))
				return false;

			Span<int> tempCnpj = stackalloc int[14];
			int pos = 0;
			var todosIguais = true;

			for (int i = 0; i < cnpj.Length; i++)
			{
				if (char.IsDigit(cnpj[i]))
				{
					if (pos > 13) return false;
					tempCnpj[pos] = cnpj[i] - '0';
					if (todosIguais && (pos > 0))
					{
						todosIguais = tempCnpj[pos] == tempCnpj[pos - 1];
					}
					pos++;
				}
			}
			if (pos != 14) return false;
			if (todosIguais) return false;

			int soma1 = 0;
			int soma2 = 0;

			for (int i = 0; i < 12; i++)
			{
				soma1 += tempCnpj[i] * MULTIPLICADOR1[i];
				soma2 += tempCnpj[i] * MULTIPLICADOR2[i];
			}

			int resto = soma1 % 11;

			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;

			int digito1 = resto;
			if (tempCnpj[12] != digito1) return false;

			soma2 += digito1 * MULTIPLICADOR2[12];

			resto = soma2 % 11;

			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;

			return tempCnpj[13] == resto;
		}
	}
}
