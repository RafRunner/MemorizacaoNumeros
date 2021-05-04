using MemorizacaoNumeros.src.util;

namespace MemorizacaoNumeros.src.model {
	public class Experimento : EntidadeDeBanco {

		private string instrucaoInicial;
		public string InstrucaoInicial { 
			get => instrucaoInicial;
			set => instrucaoInicial = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Instrução Inicial"); 
		}

		private int tempoTelaPretaInicial;
		public int TempoTelaPretaInicial { 
			get => tempoTelaPretaInicial;
			set => tempoTelaPretaInicial = NumericUtils.ValidarNatural(value, "Tempo tela preta inicial"); 
		}

		private int tempoTelaPretaITI;
		public int TempoTelaPretaITI {
			get => tempoTelaPretaITI;
			set => tempoTelaPretaITI = NumericUtils.ValidarNatural(value, "Tempo tela preta ITI");
		}

		private int tempoApresentacaoEstimulo;
		public int TempoApresentacaoEstimulo {
			get => tempoApresentacaoEstimulo;
			set => tempoApresentacaoEstimulo = NumericUtils.ValidarNatural(value, "Tempo apresentação estímulo");
		}

		private int tamanhoBlocoTentativas;
		public int TamanhoBlocoTentativas {
			get => tamanhoBlocoTentativas;
			set => tamanhoBlocoTentativas =  NumericUtils.ValidarNatural(value, "Tamanho do bloco de tentativas");
		}
	}
}
