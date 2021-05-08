using MemorizacaoNumeros.src.util;


namespace MemorizacaoNumeros.src.model {
	public class Participante : Pessoa {

		public static string[] escolaridades = new string[] { 
		"Fundamental Incompleto",
		"Fundamental Completo",
		"Ensino Médio Incompleto",
		"Ensino Médio Completo",
		"Superior Incompleto",
		"Superior Completo",
		"Pós Graduação",
		"Mestrado",
		"Doutorado",
		"Pós Doutorado"
		};

		private string endereco;
		public string Endereco {
			get => endereco;
			set => endereco = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Endereço do Participante");
		}

		private int idade;
		public int Idade {
			get => idade;
			set => idade = NumericUtils.ValidarNatural(value, "Idade do Participante");
		}

		private string escolaridade;
		public string Escolaridade {
			get => escolaridade;
			set => escolaridade = StringUtils.ValidarSeNaLista(value, escolaridades, "Escolaridade do Participante");
		}

		private string curso;
		public string Curso {
			get => curso;
			set => curso = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Curso do Participante");
		}
	}
}
