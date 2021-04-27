using MemorizacaoNumeros.src.util;


namespace MemorizacaoNumeros.src.model {
	public class Participante : Pessoa {

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
			set => escolaridade = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Escolaridade do Participante");
		}

		private string curso;
		public string Curso {
			get => curso;
			set => curso = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Curso do Participante");
		}
	}
}
