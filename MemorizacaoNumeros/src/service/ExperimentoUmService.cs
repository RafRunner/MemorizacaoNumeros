using MemorizacaoNumeros.src.model;
using System.Collections.Generic;
using System.Linq;

namespace MemorizacaoNumeros.src.service {
	public class ExperimentoUmService : AbstractService {

		private static readonly string nomeTabela = "ExperimentoUm";
		private static readonly List<string> colunas = GetNomeColunas(typeof(ExperimentoUm));
		private static readonly string sqlInsert = GeraSqlInsert(nomeTabela, colunas);
		private static readonly string sqlUpdate = GeraSqlUpdate(nomeTabela, colunas);

		public static ExperimentoUm GetById(long id) {
			return GetById<ExperimentoUm>(id, nomeTabela);
		}

		public static List<ExperimentoUm> GetAll() {
			return GetAll<ExperimentoUm>(nomeTabela);
		}

		public static List<object> GetAllAsObj() {
			return GetAll().Cast<object>().ToList();
		}

		public static void Salvar(ExperimentoUm experimento) {
			SalvarSeNaoRepetido(experimento, nomeTabela, sqlInsert, sqlUpdate, colunas);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(ExperimentoUm experimento) {
			Deletar(experimento, nomeTabela);
		}
	}
}
