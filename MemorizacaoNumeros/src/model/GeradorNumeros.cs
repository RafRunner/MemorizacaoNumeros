using System;
using System.Collections.Generic;

namespace MemorizacaoNumeros.src.model {
	public class GeradorNumeros {

		private readonly Dictionary<int, List<string>> numerosJaGerados = new Dictionary<int, List<string>>();
		private readonly Random random = new Random();
		private readonly List<int> digitos = new List<int>();
		
		public GeradorNumeros() {
			ResetDigitos();
		}
		
		private void ResetDigitos() {
			digitos.Clear();
			for (int i = 0; i < 10; i++) {
				digitos.Add(i);
			}
		}

		public string GerarNumero(int tamanho) {
			if (!numerosJaGerados.ContainsKey(tamanho)) {
				numerosJaGerados[tamanho] = new List<string>();
			}

			var jaGerados = numerosJaGerados[tamanho];
			
			var garantirNaoRepetidos = 9;
			if (tamanho >= 3) {
				garantirNaoRepetidos = 100;
			}
			else if (tamanho >= 2) {
				garantirNaoRepetidos = 30;
			}

			if (jaGerados.Count >= garantirNaoRepetidos) {
				jaGerados.Clear();
			}

			var num = "";

			while (num == "") {
				for (int i = 0; i < tamanho; i++) {
					if (digitos.Count == 0) {
						ResetDigitos();
					}
					var novoDigito =  digitos[random.Next(0, digitos.Count)];
					
					if ((num.Length > 0 && digitos.Count > 2 && Math.Abs(Convert.ToInt32(num.Substring(i - 1, 1)) - novoDigito) < 2) || 
						(i == 0 && novoDigito == 0)) {
						i--;
						continue;
					}

					num += novoDigito;
					digitos.Remove(novoDigito);
				}

				if (jaGerados.Contains(num)) {
					num = "";
					ResetDigitos();
				}
			}

			jaGerados.Add(num);
			ResetDigitos();
			return num;
		}
	}
}
