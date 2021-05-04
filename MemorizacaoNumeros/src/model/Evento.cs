namespace MemorizacaoNumeros.src.model {
	public class Evento : EntidadeDeBanco {
		//Construtor vazio para o Dapper
		public Evento() { }

		public Evento(string texto, string origem) {
			Texto = texto;
			Origem = origem;
		}

		public string Texto { get; set; }

		public string Origem { get; set; }

		public long IdExperimento { get; set; }
		private ExperimentoRealizado experimento;
		//TODO terminar depois
		public ExperimentoRealizado ExperimentoRealizado {
			get;
			set;
		}

		//Na verdade é apenas o offset em segundos a partir do início do experimento
		public long Horario { get; set; }
	}
}
