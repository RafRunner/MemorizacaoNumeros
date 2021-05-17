using MemorizacaoNumeros.src.util;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoDois : Experimento {

		public static readonly string[] graus = new string[] { "Iniciante", "Aprendiz", "Graduado", "Mestre" };

		public static string[] ordemColunas = new string[] {
			"InstrucaoInicial",
			"QuantidadeBlocosLinhaDeBase",
			"QuantidadeBlocosCondicao1",
			"QuantidadeBlocosCondicao2",
			"PontosTalvezErro1",
			"PontosTalvezAcerto1",
			"PontosCertezaErro1",
			"PontosCertezaAcerto1",
			"PontosTalvezErro2",
			"PontosTalvezAcerto2",
			"PontosCertezaErro2",
			"PontosCertezaAcerto2",
			"PontosPorGrau",
		};

		private int quantidadeBlocosLinhaDeBase;
		public int QuantidadeBlocosLinhaDeBase {
			get => quantidadeBlocosLinhaDeBase;
			set => quantidadeBlocosLinhaDeBase = NumericUtils.ValidarNatural(value, "Quantidade Blocos Linha de Base");
		}

		private int quantidadeBlocosCondicao1;
		public int QuantidadeBlocosCondicao1 {
			get => quantidadeBlocosCondicao1;
			set => quantidadeBlocosCondicao1 = NumericUtils.ValidarNatural(value, "Quantidade Blocos da Condição 1");
		}

		private int pontosTalvezErro1;
		public int PontosTalvezErro1 {
			get => pontosTalvezErro1;
			set => pontosTalvezErro1 = NumericUtils.ValidarNaoNegativo(value, "Pontos Talvez Erro Condição 1");
		}

		private int pontosTalvezAcerto1;
		public int PontosTalvezAcerto1 {
			get => pontosTalvezAcerto1;
			set => pontosTalvezAcerto1 = NumericUtils.ValidarNaoNegativo(value, "Pontos Talvez Acerto Condição 1");
		}

		private int pontosCertezaErro1;
		public int PontosCertezaErro1 {
			get => pontosCertezaErro1;
			set => pontosCertezaErro1 = NumericUtils.ValidarNaoNegativo(value, "Pontos Certeza Erro Condição 1");
		}

		private int pontosCertezaAcerto1;
		public int PontosCertezaAcerto1 {
			get => pontosCertezaAcerto1;
			set => pontosCertezaAcerto1 = NumericUtils.ValidarNaoNegativo(value, "Pontos Certeza Acerto Condição 1");
		}

		private int quantidadeBlocosCondicao2;
		public int QuantidadeBlocosCondicao2 {
			get => quantidadeBlocosCondicao2;
			set => quantidadeBlocosCondicao2 = NumericUtils.ValidarNatural(value, "Quantidade Blocos da Condição 2");
		}

		private int pontosTalvezErro2;
		public int PontosTalvezErro2 {
			get => pontosTalvezErro2;
			set => pontosTalvezErro2 = NumericUtils.ValidarNaoNegativo(value, "Pontos Talvez Erro Condição 2");
		}

		private int pontosTalvezAcerto2;
		public int PontosTalvezAcerto2 {
			get => pontosTalvezAcerto2;
			set => pontosTalvezAcerto2 = NumericUtils.ValidarNaoNegativo(value, "Pontos Talvez Acerto Condição 2");
		}

		private int pontosCertezaErro2;
		public int PontosCertezaErro2 {
			get => pontosCertezaErro2;
			set => pontosCertezaErro2 = NumericUtils.ValidarNaoNegativo(value, "Pontos Certeza Erro Condição 2");
		}

		private int pontosCertezaAcerto2;
		public int PontosCertezaAcerto2 {
			get => pontosCertezaAcerto2;
			set => pontosCertezaAcerto2 = NumericUtils.ValidarNaoNegativo(value, "Pontos Certeza Acerto Condição 2");
		}

		private int pontosPorGrau;
		public int PontosPorGrau {
			get => pontosPorGrau;
			set => pontosPorGrau = NumericUtils.ValidarNatural(value, "Pontos por Grau");
		}

		public override string ToString() {
			return $"Instrução Inicial: {InstrucaoInicial}\n" +
				$"Número Blocos Linha de Base: {QuantidadeBlocosLinhaDeBase}\n" +
				$"Número Blocos Condição 1: {QuantidadeBlocosCondicao1}\n" +
				$"Pontos Acerto Certeza 1: {PontosCertezaAcerto1}\n" +
				$"Pontos Erro Certeza 1: {PontosCertezaErro1}\n" +
				$"Pontos Acerto Talvez 1: {PontosTalvezAcerto1}\n" +
				$"Pontos Erro Talvez 1: {PontosTalvezErro1}\n" +
				$"Número Blocos Condição 2: {QuantidadeBlocosCondicao2}\n" +
				$"Pontos Acerto Certeza 2: {PontosCertezaAcerto2}\n" +
				$"Pontos Erro Certeza 2: {PontosCertezaErro2}\n" +
				$"Pontos Acerto Talvez 2: {PontosTalvezAcerto2}\n" +
				$"Pontos Erro Talvez 2: {PontosTalvezErro2}\n" +
				$"Pontos Por Grau: {PontosPorGrau}";
		}
	}
}
