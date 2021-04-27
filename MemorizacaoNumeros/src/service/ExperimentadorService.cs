using MemorizacaoNumeros.src.model;
using System.Collections.Generic;

namespace MemorizacaoNumeros.src.service {
	public class ExperimentadorService : AbstractService {
		private static readonly string nomeTabela = "Experimentador";
		private static readonly string sqlInsert = GeraSqlInsert(typeof(Experimentador));
		private static readonly string sqlUpdate = GeraSqlUpdate(typeof(Experimentador));

		public static Experimentador GetById(long id) {
			return GetById<Experimentador>(id, nomeTabela);
		}

		public static List<Experimentador> GetAll() {
			return GetAll<Experimentador>(nomeTabela);
		}

		public static void Salvar(Experimentador experimentador) {
			Salvar(experimentador, nomeTabela, sqlInsert, sqlUpdate);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(Experimentador experimentador) {
			Deletar(experimentador, nomeTabela);
		}
	}
}
