using MemorizacaoNumeros.src.util;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoUm : Experimento {

		private int criterioAcertoPreTreino;
		public int CriterioAcertoPreTreino {
			get => criterioAcertoPreTreino;
			set => criterioAcertoPreTreino = NumericUtils.ValidarNatural(value, "Critério acerto Pré-Treino");
		}

		private int criterioTalvezLinhaDeBase;
		public int CriterioTalvezLinhaDeBase { 
			get => criterioTalvezLinhaDeBase;
			set => criterioTalvezLinhaDeBase = NumericUtils.ValidarNaturalDentroDeLimite(value, 100, "Critério talvez Linha de Base");
		}

		private int criterioReforcoFaseExperimental;
		public int CriterioReforcoFaseExperimental { 
			get => criterioReforcoFaseExperimental;
			set => criterioReforcoFaseExperimental = NumericUtils.ValidarNaturalDentroDeLimite(value, 100, "Critério Reforço Fase Experimental");
		}
	}
}
