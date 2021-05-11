using MemorizacaoNumeros.src.model;
using System.Collections.Generic;
using System.Linq;

namespace MemorizacaoNumeros.src.service {
	public class ExperimentoDoisService : AbstractService {

		private static readonly string nomeTabela = "ExperimentoDois";
		private static readonly List<string> colunas = GetNomeColunas(typeof(ExperimentoDois));
		private static readonly string sqlInsert = GeraSqlInsert(nomeTabela, colunas);
		private static readonly string sqlUpdate = GeraSqlUpdate(nomeTabela, colunas);

		public static ExperimentoDois GetById(long id) {
			return GetById<ExperimentoDois>(id, nomeTabela);
		}

		public static List<ExperimentoDois> GetAll() {
			return GetAll<ExperimentoDois>(nomeTabela);
		}

		public static List<object> GetAllAsObj() {
			return GetAll().Cast<object>().ToList();
		}

		public static void Salvar(ExperimentoDois experimento) {
			SalvarSeNaoRepetido(experimento, nomeTabela, sqlInsert, sqlUpdate, colunas);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(ExperimentoDois experimento) {
			Deletar(experimento, nomeTabela);
		}
	}
}
