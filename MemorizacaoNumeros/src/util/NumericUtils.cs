using System;

namespace MemorizacaoNumeros.src.util {
	public class NumericUtils {

		public static int ValidarNaoNegativoDentroDeLimite(int numero, int limite, string nomeCampo) {
			if (numero < 0) {
				throw new Exception($"Campo numérico {nomeCampo} não pode ser negativo! Por favor, insera um valor válido.");
			}
			if (numero > limite) {
				throw new Exception($"Campo numérico {nomeCampo} não pode ser maior que {limite}! Por favor, insera um valor válido.");
			}
			return numero;
		}

		public static int ValidarNaturalDentroDeLimite(int numero, int limite, string nomeCampo) {
			ValidarNatural(numero, nomeCampo);
			if (numero > limite) {
				throw new Exception($"Campo numérico {nomeCampo} não pode ser maior que {limite}! Por favor, insera um valor válido.");
			}
			return numero;
		}

		public static int ValidarNatural(int numero, string nomeCampo) {
			if (numero <= 0) {
				throw new Exception($"Campo numérico {nomeCampo} deve ser maior que 0! Por favor, insera um valor válido.");
			}
			return numero;
		}

		public static int ValidarNaoNegativo(int numero, string nomeCampo) {
			if (numero < 0) {
				throw new Exception($"Campo numérico {nomeCampo} não pode ser negativo!");
			}
			return numero;
		}
	}
}
