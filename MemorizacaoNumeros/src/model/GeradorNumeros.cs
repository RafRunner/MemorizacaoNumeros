using MemorizacaoNumeros.src.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemorizacaoNumeros.src.model {
	public class GeradorNumeros {

		private readonly Dictionary<int, List<string>> numerosJaGerados = new Dictionary<int, List<string>>();
		private readonly Random random = new Random();

		public string GerarNumero(int tamanho) {
			if (!numerosJaGerados.ContainsKey(tamanho)) {
				numerosJaGerados[tamanho] = new List<string>();
			}

			var jaGerados = numerosJaGerados[tamanho];

			if (jaGerados.Count == 9) {
				numerosJaGerados[tamanho].Clear();
				jaGerados.Clear();
			}

			var num = "";

			while (num == "") {

				for (int i = 0; i < tamanho; i++) {
					var novoDigito = random.Next(0, 10);

					if ((i == 0 && novoDigito == 0) || (tamanho < 10 && num.Contains(novoDigito.ToString()))) {
						i--;
						continue;
					}

					num += novoDigito;
				}

				if (jaGerados.Contains(num)) {
					num = "";
				}
			}

			numerosJaGerados[tamanho].Add(num);
			return num;
		}
	}
}
