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

		// Para poderem ser ordenados
		public int Indice { get; set; }

		public long? IdExperimentoUmRealizado { get; set; }

		public long? IdExperimentoDoisRealizado { get; set; }

		//Na verdade é apenas o offset em milisegundos a partir do início do experimento
		public long Horario { get; set; }

		public int CompareTo(Evento other) {
			if (other == null) {
				return 1;
			}

			return Indice.CompareTo(other.Indice);
		}
	}
}
