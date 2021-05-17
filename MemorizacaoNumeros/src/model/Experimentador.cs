namespace MemorizacaoNumeros.src.model {
	public class Experimentador : Pessoa {

		public static string[] odemColunasGrid = new string[] {
			"Nome", "Email", "Telefone"
		};

		public override string ToString() {
			return $"Nome: {Nome}\nEmail: {Email}\nTelefone: {Telefone}";
		}
	}
}
