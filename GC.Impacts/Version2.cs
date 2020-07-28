using System;
using System.Collections.Generic;
using System.Text;

namespace GC.Impacts
{
	public static class Version2
	{
		public static bool ValidarCPF(string sourceCPF)
		{
			if (String.IsNullOrWhiteSpace(sourceCPF))
				return false;

			string clearCPF;
			clearCPF = sourceCPF.Trim();
			clearCPF = clearCPF.Replace("-", "");
			clearCPF = clearCPF.Replace(".", "");

			if (clearCPF.Length != 11)
				return false;

			int totalDigitoI = 0;
			int totalDigitoII = 0;
			int modI;
			int modII;

			if (clearCPF.Equals("00000000000") ||
				clearCPF.Equals("11111111111") ||
				clearCPF.Equals("22222222222") ||
				clearCPF.Equals("33333333333") ||
				clearCPF.Equals("44444444444") ||
				clearCPF.Equals("55555555555") ||
				clearCPF.Equals("66666666666") ||
				clearCPF.Equals("77777777777") ||
				clearCPF.Equals("88888888888") ||
				clearCPF.Equals("99999999999"))
			{
				return false;
			}

			foreach (char c in clearCPF)
			{
				if (!char.IsNumber(c))
					return false;
			}

			for (int posicao = 0; posicao < clearCPF.Length - 2; posicao++)
			{
				totalDigitoI += (clearCPF[posicao] - '0') * (10 - posicao);
				totalDigitoII += (clearCPF[posicao] - '0') * (11 - posicao);
			}

			modI = totalDigitoI % 11;
			if (modI < 2)
				modI = 0;
			else
				modI = 11 - modI;

			if ((clearCPF[9] - '0') != modI)
				return false;

			totalDigitoII += modI * 2;
			modII = totalDigitoII % 11;
			if (modII < 2)
				modII = 0;
			else
				modII = 11 - modII;

			if ((clearCPF[10] - '0') != modII)
				return false;

			return true;
		}
	}
}
