using MemorizacaoNumeros.src.model;
using System.Collections.Generic;

namespace MemorizacaoNumeros.src.service {
	public class ExperimentoUmService : AbstractService {

		private static readonly string nomeTabela = "ExperimentoUm";
		private static readonly string sqlInsert = GeraSqlInsert(typeof(ExperimentoUm));
		private static readonly string sqlUpdate = GeraSqlUpdate(typeof(ExperimentoUm));

		public static ExperimentoUm GetById(long id) {
			return GetById<ExperimentoUm>(id, nomeTabela);
		}

		public static List<ExperimentoUm> GetAll() {
			return GetAll<ExperimentoUm>(nomeTabela);
		}

		public static void Salvar(ExperimentoUm experimento) {
			Salvar(experimento, nomeTabela, sqlInsert, sqlUpdate);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(ExperimentoUm experimento) {
			Deletar(experimento, nomeTabela);
		}
	}
}
