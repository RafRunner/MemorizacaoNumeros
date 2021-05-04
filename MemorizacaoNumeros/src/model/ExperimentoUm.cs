using MemorizacaoNumeros.src.util;
using System;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoUm : Experimento {

		private int tamanhoSequenciaInicial;
		public int TamanhoSequenciaInicial {
			get => tamanhoSequenciaInicial;
			set => tamanhoSequenciaInicial = NumericUtils.ValidarNatural(value, "Tamanho da sequência inicial");
		}

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

		private int numeroBlocosFaseExperimental;
		public int NumeroBlocosFaseExperimental {
			get => numeroBlocosFaseExperimental;
			set => numeroBlocosFaseExperimental = NumericUtils.ValidarNatural(value, "Número de blocos na Fase Experimental");
		}

		private int criterioReforcoFaseExperimental;
		public int CriterioReforcoFaseExperimental { 
			get => criterioReforcoFaseExperimental;
			set => criterioReforcoFaseExperimental = NumericUtils.ValidarNaturalDentroDeLimite(value, 100, "Critério Reforço Fase Experimental");
		}

		public int CalculaCriterioTalvezLinhaDeBase() {
			return Convert.ToInt32(Math.Floor(TamanhoBlocoTentativas * ((float)criterioTalvezLinhaDeBase / 100)));
		}

		public int CalculaCriterioReforcoFaseExperimental(int quantidadeReforcosFracos) {
			return Convert.ToInt32(Math.Ceiling(quantidadeReforcosFracos * ((float)criterioReforcoFaseExperimental / 100)));
		}
	}
}
