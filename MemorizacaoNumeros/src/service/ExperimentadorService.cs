using MemorizacaoNumeros.src.model;
using System.Collections.Generic;
using System.Linq;

namespace MemorizacaoNumeros.src.service {
	public class ExperimentadorService : AbstractService {
		private static readonly string nomeTabela = "Experimentador";
		private static readonly List<string> colunas = GetNomeColunas(typeof(Experimentador));
		private static readonly string sqlInsert = GeraSqlInsert(nomeTabela, colunas);
		private static readonly string sqlUpdate = GeraSqlUpdate(nomeTabela, colunas);

		public static Experimentador GetById(long id) {
			return GetById<Experimentador>(id, nomeTabela);
		}

		public static List<Experimentador> GetAll() {
			return GetAll<Experimentador>(nomeTabela);
		}

		public static List<object> GetAllAsObj() {
			return GetAll().Cast<object>().ToList();
		}

		public static void Salvar(Experimentador experimentador) {
			SalvarSeNaoRepetido(experimentador, nomeTabela, sqlInsert, sqlUpdate, colunas);
		}

		public static void DeletarPorId(long id) {
			DeletarPorId(id, nomeTabela);
		}

		public static void Deletar(Experimentador experimentador) {
			Deletar(experimentador, nomeTabela);
		}
	}
}
