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
			var experimento = GetById<ExperimentoDoisRealizado>(id, nomeTabela);

			if (experimento == null) {
				return null;
			}

			var eventos = EventoService.GetAllByExperimentoDoisRealizado(experimento);

			experimento.SetListaEventos(eventos);

			return experimento;
		}

		public static List<ExperimentoDoisRealizado> GetAll() {
			var experimentos = GetAll<ExperimentoDoisRealizado>(nomeTabela);

			foreach (var experimento in experimentos) {
				var eventos = EventoService.GetAllByExperimentoDoisRealizado(experimento);
				eventos.Sort();
				experimento.SetListaEventos(eventos);
			}

			return experimentos;
		}

		public static void Salvar(ExperimentoDoisRealizado experimento) {
			Salvar(experimento, nomeTabela, sqlInsert, sqlUpdate);

			foreach (var evento in experimento.GetListaEventos()) {
				EventoService.Salvar(evento);
			}
		}

		public static void DeletarPorId(long id) {
			Deletar(GetById(id));
		}

		public static void Deletar(ExperimentoDoisRealizado experimento) {
			foreach (var evento in experimento.GetListaEventos()) {
				EventoService.Deletar(evento);
			}

			Deletar(experimento, nomeTabela);
		}
	}
}
