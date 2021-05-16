using System;
using System.Collections.Generic;
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

		public static string GetCaminhoAbsoluto(string nomePasta, string nomeArquivo = "") {
			string caminhoPasta = GetDiretorioDeTrabalho() + @"\" + nomePasta;
			if (string.IsNullOrEmpty(nomeArquivo)) {
				return caminhoPasta;
			}
			return caminhoPasta + @"\" + nomeArquivo;
		}

		public static DirectoryInfo CriaDiretorioAmbiente(string diretorio) {
			var fullPath = GetCaminhoAbsoluto(diretorio);

			return Directory.CreateDirectory(fullPath);
		}

		public static List<string> LerArquivo(string caminhoArquivo) {
			var linhas = new List<string>();

			using (var sr = new StreamReader(caminhoArquivo)) {
				while (!sr.EndOfStream) {
					linhas.Add(sr.ReadLine());
				}
			}

			return linhas;
		}

		public static List<string> LerArquivoRelativo(string nomePasta, string nomeArquivo) {
			return LerArquivo(GetCaminhoAbsoluto(nomePasta, nomeArquivo));
		}
	}
}
