using MemorizacaoNumeros.src.model;
using System.Collections.Generic;

namespace MemorizacaoNumeros.src.service {
	public class ExperimentoDoisService : AbstractService {

		private static readonly string nomeTabela = "ExperimentoDois";
		private static readonly string sqlInsert = GeraSqlInsert(typeof(ExperimentoDois));
		private static readonly string sqlUpdate = GeraSqlUpdate(typeof(ExperimentoDois));

		public static ExperimentoDois GetById(long id) {
			return GetById<ExperimentoDois>(id, nomeTabela);
		}

		public static List<ExperimentoDois> GetAll() {
			return GetAll<ExperimentoDois>(nomeTabela);
		}

		public static void Salvar(ExperimentoDois experimento) {
			Salvar(experimento, nomeTabela, sqlInsert, sqlUpdate);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(ExperimentoDois experimento) {
			Deletar(experimento, nomeTabela);
		}
	}
}
