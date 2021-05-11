using MemorizacaoNumeros.src.util;

namespace MemorizacaoNumeros.src.model {
	public class Experimento : EntidadeDeBanco {

		private string instrucaoInicial;
		public string InstrucaoInicial { 
			get => instrucaoInicial;
			set => instrucaoInicial = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Instrução Inicial"); 
		}
	}
}
