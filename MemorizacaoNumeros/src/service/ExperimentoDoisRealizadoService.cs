using MemorizacaoNumeros.src.model;
using System.Collections.Generic;

namespace MemorizacaoNumeros.src.service {
	public class ExperimentoDoisRealizadoService : AbstractService {

		private static readonly string nomeTabela = "ExperimentoDoisRealizado";
		private static readonly List<string> colunas = new List<string>() {
			"IdExperimentoDois",
			"DataHoraInicio",
		};
		private static readonly string sqlInsert = GeraSqlInsert(nomeTabela, colunas);
		private static readonly string sqlUpdate = GeraSqlUpdate(nomeTabela, colunas);

		public static ExperimentoDoisRealizado GetById(long id) {
			return GetById<ExperimentoDoisRealizado>(id, nomeTabela);
		}

		public static List<ExperimentoDoisRealizado> GetAll() {
			return GetAll<ExperimentoDoisRealizado>(nomeTabela);
		}

		public static void Salvar(ExperimentoDoisRealizado experimento) {
			Salvar(experimento, nomeTabela, sqlInsert, sqlUpdate);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(ExperimentoDoisRealizado experimento) {
			Deletar(experimento, nomeTabela);
		}
	}
}
