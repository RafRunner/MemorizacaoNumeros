using MemorizacaoNumeros.src.model;
using System.Collections.Generic;

namespace MemorizacaoNumeros.src.service {
	public class ParticipanteService : AbstractService {
		private static readonly string nomeTabela = "Participante";
		private static readonly string sqlInsert = GeraSqlInsert(typeof(Participante));
		private static readonly string sqlUpdate = GeraSqlUpdate(typeof(Participante));

		public static Participante GetById(long id) {
			return GetById<Participante>(id, nomeTabela);
		}

		public static List<Participante> GetAll() {
			return GetAll<Participante>(nomeTabela);
		}

		public static void Salvar(Participante participante) {
			Salvar(participante, nomeTabela, sqlInsert, sqlUpdate);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(Participante participante) {
			Deletar(participante, nomeTabela);
		}
	}
}
