namespace MemorizacaoNumeros.src.model {
	public class EntidadeDeBanco {
		// Id tem que ter set para, ao deletar um objeto, o métodod possa settar seu Id como zero para que se possa saber que aquele registro não existe mais no banco de dados
		// Além disso ao salvar uma nova pessoa no banco seu id passa de 0 para seu id no banco
		public long Id { get; set; }

		protected long GetId(EntidadeDeBanco value) {
			if (value == null) {
				return 0;
			}
			else {
				return value.Id;
			}
		}
	}
}
