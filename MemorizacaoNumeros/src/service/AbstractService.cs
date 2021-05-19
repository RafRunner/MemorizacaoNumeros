using Dapper;
using MemorizacaoNumeros.src.model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace MemorizacaoNumeros.src.service {
	public class AbstractService {
		protected static string GetConnectionString(string id = "Default") {
			return ConfigurationManager.ConnectionStrings[id].ConnectionString;
		}

		protected static T GetById<T>(long id, string nomeTabela) where T : EntidadeDeBanco {
			if (id == 0) {
				return default(T);
			}
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				IEnumerable<T> data = cnn.Query<T>($"SELECT * FROM {nomeTabela} WHERE Id = @Id", new { Id = id });
				return data.Count() > 0 ? data.Single() : default(T);
			}
		}

		protected static List<T> GetByObj<T>(string sql, object obj) {
			if (obj == null) {
				return new List<T>();
			}

			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				return cnn.Query<T>(sql, obj).ToList();
			}
		}

		protected static List<T> GetAll<T>(string nomeTabela) where T : EntidadeDeBanco {
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				return cnn.Query<T>($"SELECT * FROM {nomeTabela}").ToList<T>();
			}
		}

		protected static void Salvar<T>(T objeto, string nomeTabela, string sqlInsert, string sqlUpdate) where T : EntidadeDeBanco {
			T objetoExistente = GetById<T>(objeto.Id, nomeTabela);

			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				if (objetoExistente == null) {
					long id = cnn.Query<long>(sqlInsert + "; SELECT CAST(last_insert_rowid() as int)", objeto).Single();
					objeto.Id = id;
				}
				else {
					cnn.Execute(sqlUpdate, objeto);
				}
			}
		}

		protected static void SalvarSeNaoRepetido<T>(T objeto, string nomeTabela, string sqlInsert, string sqlUpdate, List<string> colunas) where T : EntidadeDeBanco {
			if (GetObjetosIdenticos(objeto, nomeTabela, colunas).Count != 0) {
				return;
			}

			Salvar(objeto, nomeTabela, sqlInsert, sqlUpdate);
		}

		protected static void Deletar(EntidadeDeBanco objeto, string nomeTabela) {
			if (objeto == null) {
				return;
			}

			DeletarPorId(objeto.Id, nomeTabela);
			objeto.Id = 0;
		}

		protected static void DeletarPorId(long id, string nomeTabela) {
			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				cnn.Execute($"DELETE FROM {nomeTabela} WHERE Id = {id}");
			}
		}

		public static List<object> FilterDataTable(List<object> itens, string textoDeBusca) {
			if (itens.Count == 0) return itens;

			var type = itens.First().GetType();

			return itens.FindAll(item => {
				foreach (var p in type.GetProperties()) {
					if (p.GetValue(item).ToString().ToLower().Contains(textoDeBusca.ToLower())) {
						return true;
					}
				}

				return false;
			});
		}

		protected static List<T> GetObjetosIdenticos<T>(T objeto, string nomeTabela, List<string> colunas) where T : EntidadeDeBanco {
			var sqlSelect = $"SELECT * FROM {nomeTabela} WHERE ";

			for (int i = 0; i < colunas.Count; i++) {
				var coluna = colunas[i];
				sqlSelect += $"{coluna} = @{coluna}";

				if (i != colunas.Count - 1) {
					sqlSelect += " AND ";
				}
			}

			using (IDbConnection cnn = new SQLiteConnection(GetConnectionString())) {
				return cnn.Query<T>(sqlSelect, objeto).ToList();
			}
		}

		protected static List<string> GetNomeColunas(Type type) {
			var colunas = new List<string>();

			foreach (var p in type.GetProperties()) {
				if (p.Name != "Id") {
					colunas.Add(p.Name);
				}
			}

			return colunas;
		}

		protected static string GeraSqlInsert(Type type) {
			return GeraSqlInsert(type.Name, GetNomeColunas(type));
		}

		protected static string GeraSqlUpdate(Type type) {
			return GeraSqlUpdate(type.Name, GetNomeColunas(type));
		}

		protected static string GeraSqlInsert(string nomeTabela, List<string> colunas) {
			var parteDireita = "";
			var parteEsquerda = "";

			for (int i = 0; i < colunas.Count; i++) {
				var coluna = colunas[i];

				parteDireita += coluna;
				parteEsquerda += "@" + coluna;

				if (i != colunas.Count - 1) {
					parteDireita += ", ";
					parteEsquerda += ", ";
				}
			}

			return $"INSERT INTO {nomeTabela} ({parteDireita}) VALUES ({parteEsquerda})";
		}

		protected static string GeraSqlUpdate(string nomeTabela, List<string> colunas) {
			var sqlUpdate = $"UPDATE {nomeTabela} SET ";

			for (int i = 0; i < colunas.Count; i++) {
				var coluna = colunas[i];

				sqlUpdate += coluna + " = @" + coluna;

				if (i != colunas.Count - 1) {
					sqlUpdate += ", ";
				}
			}

			return sqlUpdate + " WHERE Id = @Id";
		}
	}
}
