using MemorizacaoNumeros.src.model;
using System.Collections.Generic;

namespace MemorizacaoNumeros.src.service {
	public class EventoService : AbstractService {

		private static readonly string nomeTabela = "Evento";
		private static readonly string sqlInsert = GeraSqlInsert(typeof(Evento));
		private static readonly string sqlUpdate = GeraSqlUpdate(typeof(Evento));

		public static Evento GetById(long id) {
			return GetById<Evento>(id, nomeTabela);
		}

		public static List<Evento> GetAll() {
			return GetAll<Evento>(nomeTabela);
		}

		public static List<Evento> GetAllByExperimentoUmRealizado(ExperimentoUmRealizado experimento) {
			return GetByObj<Evento>($"SELECT * FROM {nomeTabela} WHERE IdExpeimentoUmRealizado = @Id", experimento);
		}

		public static List<Evento> GetAllByExperimentoDoisRealizado(ExperimentoDoisRealizado experimento) {
			return GetByObj<Evento>($"SELECT * FROM {nomeTabela} WHERE IdExpeimentoDoisRealizado = @Id", experimento);
		}

		public static void Salvar(Evento experimento) {
			Salvar(experimento, nomeTabela, sqlInsert, sqlUpdate);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(Evento experimento) {
			Deletar(experimento, nomeTabela);
		}
	}
}
