using MemorizacaoNumeros.src.model;
using System.Collections.Generic;

namespace MemorizacaoNumeros.src.service {
	public class ExperimentoUmRealizadoService : AbstractService {

		private static readonly string nomeTabela = "ExperimentoUmRealizado";
		private static readonly List<string> colunas = new List<string>() {
			"IdExperimentoUm",
			"DataHoraInicio",
		};
		private static readonly string sqlInsert = GeraSqlInsert(nomeTabela, colunas);
		private static readonly string sqlUpdate = GeraSqlUpdate(nomeTabela, colunas);

		public static ExperimentoUmRealizado GetById(long id) {
			return GetById<ExperimentoUmRealizado>(id, nomeTabela);
		}

		public static List<ExperimentoUmRealizado> GetAll() {
			return GetAll<ExperimentoUmRealizado>(nomeTabela);
		}

		public static void Salvar(ExperimentoUmRealizado experimento) {
			Salvar(experimento, nomeTabela, sqlInsert, sqlUpdate);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(ExperimentoUmRealizado experimento) {
			Deletar(experimento, nomeTabela);
		}
	}
}
