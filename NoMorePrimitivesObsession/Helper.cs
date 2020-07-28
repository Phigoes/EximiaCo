using System;
using System.Collections.Generic;
using System.Text;

namespace NoMorePrimitivesObsession
{
	class Helper
	{
		public static bool ValidarCPF(string sourceCPF)
		{
			static bool VerificarTodosValoresSaoIguais(ref Span<int> input)
			{
				for (int i = 0; i < 11; i++)
				{
					if (input[i] != input[0])
						return false;
				}

				return true;
			}

			if (string.IsNullOrWhiteSpace(sourceCPF))
				return false;

			Span<int> cpfArray = stackalloc int[11];
			var count = 0;

			foreach (var c in sourceCPF)
			{
				if (char.IsDigit(c))
				{
					if (count > 10)
					{
						return false;
					}

					cpfArray[count] = c - '0';
					count++;
				}
			}

			if (count != 11) return false;
			if (VerificarTodosValoresSaoIguais(ref cpfArray)) return false;

			int totalDigitoI = 0;
			int totalDigitoII = 0;
			int modI;
			int modII;

			for (int posicao = 0; posicao < cpfArray.Length - 2; posicao++)
			{
				totalDigitoI += cpfArray[posicao] * (10 - posicao);
				totalDigitoII += cpfArray[posicao] * (11 - posicao);
			}

			modI = totalDigitoI % 11;
			if (modI < 2)
				modI = 0;
			else
				modI = 11 - modI;

			if (cpfArray[9] != modI)
				return false;

			totalDigitoII += modI * 2;
			modII = totalDigitoII % 11;
			if (modII < 2)
				modII = 0;
			else
				modII = 11 - modII;

			return cpfArray[10] == modII;
		}
	}
}
