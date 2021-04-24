using System;
using System.IO;
using System.Text.RegularExpressions;

namespace MemorizacaoNumeros.src.arquivos {
	class Ambiente {
		public static string GetDiretorioDeTrabalho() {
			return Directory.GetCurrentDirectory();
		}

		public static string GetDesktop() {
			return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}

		public static string GetNomeArquivo(string caminhoArquivo) {
			Match nome = Regex.Match(caminhoArquivo, @"[^\\]+$");
			return nome.Value;
		}

		public static string GetFullPath(string nomePasta, string nomeArquivo = "") {
			string caminhoPasta = GetDiretorioDeTrabalho() + "\\" + nomePasta;
			if (string.IsNullOrEmpty(nomeArquivo)) {
				return caminhoPasta;
			}
			return caminhoPasta + "\\" + nomeArquivo;
		}

		public static DirectoryInfo CriaDiretorioSeNaoExistir(string diretorio) {
			var fullPath = GetFullPath(diretorio);

			return Directory.CreateDirectory(fullPath);
		}
	}
}
