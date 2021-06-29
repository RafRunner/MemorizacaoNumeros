using System;

namespace MemorizacaoNumeros.src.util {
	public class NumericUtils {

		public static int ValidarDentroDeLimite(int numero, int limiteInf, int limiteSup, string nomeCampo) {
			if (numero < limiteInf) {
				throw new Exception($"Campo numérico {nomeCampo} não pode ser menor que {limiteInf}! Por favor, insera um valor válido.");
			}
			if (numero > limiteSup) {
				throw new Exception($"Campo numérico {nomeCampo} não pode ser maior que {limiteSup}! Por favor, insera um valor válido.");
			}
			return numero;
		}

		public static int ValidarNatural(int numero, string nomeCampo) {
			return ValidarDentroDeLimite(numero, 1, int.MaxValue, nomeCampo);
		}
	}
}
