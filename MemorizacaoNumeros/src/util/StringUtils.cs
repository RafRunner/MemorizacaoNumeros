using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MemorizacaoNumeros.src.util {
	public class StringUtils {
		public static string Normalize(string sequencia) {
			return sequencia?.Trim();
		}

		public static string ValideNaoNuloNaoVazioENormalize(string sequencia, string nomeCampo) {
			string normalizada = Normalize(sequencia);
			if (string.IsNullOrEmpty(normalizada)) {
				throw new Exception($"O campo {nomeCampo} não pode ficar em branco! Por favor, insera um valor válido.");
			}
			return normalizada;
		}

		public static string ValideEmail(string email) {
			email = Normalize(email);
			string emailRegex = string.Format("{0}{1}",
				 @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
				 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

			if (!Regex.IsMatch(email, emailRegex)) {
				throw new Exception($"Email '{email}' inválido! Por favor, insera um email válido.");
			}
			return email;
		}

		public static string ValideTelefone(string telefone) {
			telefone = Normalize(telefone);

			if (!Regex.IsMatch(telefone, @"\+?(55)?((\d{2})|(\(\d{2}\)))?\d{4,5}-?\d{4}")) {
				throw new Exception($"Telefone '{telefone}' inválido! Por favor insira um telefone válido");
			}

			return telefone;
		}

		public static string ValidarSeNaLista(string valor, string[] valoresValidos, string nomeCampo) {
			if (!new List<string>(valoresValidos).Contains(valor)) {
				throw new Exception($"O campo {nomeCampo} deve ser um entre: {valoresValidos}.");
			}

			return valor;
		}

		public static bool EhNumero(string val) {
			return Regex.IsMatch(val, "^[0-9]+$");
		}

		public static bool HasUniqueCharacters(string str) {
			char[] chArray = str.ToCharArray();

			Array.Sort(chArray);

			for (int i = 0; i < chArray.Length - 1; i++) {
				if (chArray[i] != chArray[i + 1]) {
					continue;
				}
				else {
					return false;
				}
			}

			return true;
		}
	}
}
