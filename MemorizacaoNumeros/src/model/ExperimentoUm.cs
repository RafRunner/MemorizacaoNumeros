using MemorizacaoNumeros.src.util;
using System;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoUm : Experimento {

		public static string[] ordemColunas = new string[] {
			"InstrucaoInicial",
			"InstrucaoLinhaDeBase",
			"TempoTelaPretaInicial",
			"TempoTelaPretaITI",
			"TempoApresentacaoEstimulo",
			"TamanhoBlocoTentativas",
			"TamanhoSequenciaInicial",
			"CriterioAcertoPreTreino",
			"CriterioTalvezLinhaDeBase",
			"CriterioReforcoFaseExperimental",
			"NumeroBlocosFaseExperimental"
		};

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
			set => tamanhoBlocoTentativas = NumericUtils.ValidarNatural(value, "Tamanho do bloco de tentativas");
		}

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

		private string instrucaoLinhaDeBase;
		public string InstrucaoLinhaDeBase {
			get => instrucaoLinhaDeBase;
			set => instrucaoLinhaDeBase = StringUtils.ValideNaoNuloNaoVazioENormalize(value, "Instrução da Linha de Base");
		}

		private int criterioTalvezLinhaDeBase;
		public int CriterioTalvezLinhaDeBase { 
			get => criterioTalvezLinhaDeBase;
			set => criterioTalvezLinhaDeBase = NumericUtils.ValidarDentroDeLimite(value, 1, 100, "Critério talvez Linha de Base");
		}

		private int numeroBlocosFaseExperimental;
		public int NumeroBlocosFaseExperimental {
			get => numeroBlocosFaseExperimental;
			set => numeroBlocosFaseExperimental = NumericUtils.ValidarNatural(value, "Número de blocos na Fase Experimental");
		}

		private int criterioReforcoFaseExperimental;
		public int CriterioReforcoFaseExperimental { 
			get => criterioReforcoFaseExperimental;
			set => criterioReforcoFaseExperimental = NumericUtils.ValidarDentroDeLimite(value, 0, 100, "Critério Reforço Fase Experimental");
		}

		public int CalculaCriterioTalvezLinhaDeBase() {
			return Convert.ToInt32(Math.Floor(TamanhoBlocoTentativas * ((float)criterioTalvezLinhaDeBase / 100)));
		}

		public int CalculaCriterioReforcoFaseExperimental(int quantidadeReforcosFracos) {
			return Convert.ToInt32(Math.Ceiling(quantidadeReforcosFracos * ((float)criterioReforcoFaseExperimental / 100)));
		}

		public override string ToString() {
			return $"Instrução Inicial: {InstrucaoInicial}\n" +
				$"Tempo Tela Preta Inicial: {TempoTelaPretaInicial}s\n" +
				$"Tempo Tela Preta ITI: {TempoTelaPretaITI}s\n" +
				$"Tempo Apresentação Estímulo: {TempoApresentacaoEstimulo}s\n" +
				$"Tamanho Bloco Tentativas: {TamanhoBlocoTentativas}\n" +
				$"Tamanho Sequência Inicial: {TamanhoSequenciaInicial}\n" +
				$"Critério Acerto Pré Treino: {CriterioAcertoPreTreino} acertos consecutivos\n" +
				$"Critério Talvez Linha de Base: superior à {CriterioTalvezLinhaDeBase}%\n" +
				$"Número Blocos Fase Experimental: {NumeroBlocosFaseExperimental}\n" +
				$"Critério Repetição Fase Experimental: inferior à {CriterioReforcoFaseExperimental}%";
		}
	}
}
