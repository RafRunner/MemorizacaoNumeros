using System;
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
				throw new Exception("Este email é inválido! Por favor, insera um email válido.");
			}
			return email;
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
