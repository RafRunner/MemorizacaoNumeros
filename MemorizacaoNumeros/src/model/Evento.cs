using System;

namespace MemorizacaoNumeros.src.model {
	public class Evento : EntidadeDeBanco, IComparable<Evento> {
		//Construtor vazio para o Dapper
		public Evento() { }

		public Evento(string origem, string texto) {
			Texto = texto;
			Origem = origem;
		}

		public string Texto { get; set; }

		public string Origem { get; set; }

		public long IdExperimentoUmRealizado { get; set; }

		public long IdExperimentoDoisRealizado { get; set; }

		//Na verdade é apenas o offset em segundos a partir do início do experimento
		public long Horario { get; set; }

		// Para ordenar os eventos por id e, logo, cronologicamente
		public int CompareTo(Evento other) {
			if (other == null) {
				return 1;
			}

			return Id.CompareTo(other.Id);
		}
	}
}
