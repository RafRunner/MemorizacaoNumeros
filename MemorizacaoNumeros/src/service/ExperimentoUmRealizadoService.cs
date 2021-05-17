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
			var experimento = GetById<ExperimentoUmRealizado>(id, nomeTabela);

			if (experimento == null) {
				return null;
			}

			var eventos = EventoService.GetAllByExperimentoUmRealizado(experimento);

			experimento.SetListaEventos(eventos);

			return experimento;
		}

		public static List<ExperimentoUmRealizado> GetAll() {
			var experimentos = GetAll<ExperimentoUmRealizado>(nomeTabela);

			foreach (var experimento in experimentos) {
				var eventos = EventoService.GetAllByExperimentoUmRealizado(experimento);
				eventos.Sort();
				experimento.SetListaEventos(eventos);
			}

			return experimentos;
		}

		public static void Salvar(ExperimentoUmRealizado experimento) {
			Salvar(experimento, nomeTabela, sqlInsert, sqlUpdate);

			foreach (var evento in experimento.GetListaEventos()) {
				EventoService.Salvar(evento);
			}
		}

		public static void DeletarPorId(long id) {
			Deletar(GetById(id));
		}

		public static void Deletar(ExperimentoUmRealizado experimento) {
			foreach (var evento in experimento.GetListaEventos()) {
				EventoService.Deletar(evento);
			}

			Deletar(experimento, nomeTabela);
		}
	}
}
