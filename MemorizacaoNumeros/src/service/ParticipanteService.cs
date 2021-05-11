using MemorizacaoNumeros.src.model;
using System.Collections.Generic;
using System.Linq;

namespace MemorizacaoNumeros.src.service {
	public class ParticipanteService : AbstractService {
		private static readonly string nomeTabela = "Participante";
		private static readonly List<string> colunas = GetNomeColunas(typeof(Participante));
		private static readonly string sqlInsert = GeraSqlInsert(nomeTabela, colunas);
		private static readonly string sqlUpdate = GeraSqlUpdate(nomeTabela, colunas);

		public static Participante GetById(long id) {
			return GetById<Participante>(id, nomeTabela);
		}

		public static List<Participante> GetAll() {
			return GetAll<Participante>(nomeTabela);
		}

		public static List<object> GetAllAsObj() {
			return GetAll().Cast<object>().ToList();
		}

		public static void Salvar(Participante participante) {
			SalvarSeNaoRepetido(participante, nomeTabela, sqlInsert, sqlUpdate, colunas);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(Participante participante) {
			Deletar(participante, nomeTabela);
		}
	}
}
