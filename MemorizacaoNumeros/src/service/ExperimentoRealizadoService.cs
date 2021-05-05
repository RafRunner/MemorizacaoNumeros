using MemorizacaoNumeros.src.model;
using System.Collections.Generic;

namespace MemorizacaoNumeros.src.service {
	public class ExperimentoRealizadoService : AbstractService {

		private static readonly string nomeTabela = "ExperimentoRealizado";
		private static readonly List<string> colunas = new List<string>() {
			"IdExperimentador",
			"IdParticipante",
			"IdExperimentoUmRealizado",
			"IdExperimentoDoisRealizado",
			"DataHoraInicio"
		};
		private static readonly string sqlInsert = GeraSqlInsert(nomeTabela, colunas);
		private static readonly string sqlUpdate = GeraSqlUpdate(nomeTabela, colunas);

		public static ExperimentoRealizado GetById(long id) {
			return GetById<ExperimentoRealizado>(id, nomeTabela);
		}

		public static List<ExperimentoRealizado> GetAll() {
			return GetAll<ExperimentoRealizado>(nomeTabela);
		}

		public static void Salvar(ExperimentoRealizado experimento) {
			Salvar(experimento, nomeTabela, sqlInsert, sqlUpdate);
		}

		public static void DeletarPorId(long id) {
			Deletar(GetById(id));
		}

		public static void Deletar(ExperimentoRealizado experimento) {
			ExperimentoUmRealizadoService.Deletar(experimento.ExperimentoUmRealizado);
			ExperimentoDoisRealizadoService.Deletar(experimento.ExperimentoDoisRealizado);
			Deletar(experimento, nomeTabela);
		}
	}
}
