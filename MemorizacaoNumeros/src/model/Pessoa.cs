using MemorizacaoNumeros.src.util;

namespace MemorizacaoNumeros.src.model {
	public abstract class Pessoa : EntidadeDeBanco {

		private string nome;
		public string Nome {
			get => nome;
			set => nome = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Nome");
		}

		private string telefone;
		public string Telefone {
			get => telefone;
			set => telefone = StringUtils.ValideTelefone(value);
		}

		private string email;
		public string Email {
			get => email;
			set => email = StringUtils.ValideEmail(value);
		}
	}
}
