using MemorizacaoNumeros.src.service;
using System;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoDoisRealizado : IExperimentoRealizado {

		public long IdExperimentoDois { get; set; }
		private ExperimentoDois experimentoDois;
		public ExperimentoDois ExperimentoDois {
			get {
				if (experimentoDois == null) experimentoDois = ExperimentoDoisService.GetById(IdExperimentoDois);
				return experimentoDois;
			}
			set {
				IdExperimentoDois = GetId(value);
				experimentoDois = value;
			}
		}

		// Inicio da parte do comportamento do experimento

		private readonly GeradorNumeros geradorNumeros = new GeradorNumeros();

		// 0 - Linha de Base, 1 - Condição 1, 2 - Condição 2
		public int faseAtual = 0;
		public int pontos = 0;
		public int ultimosPontosGanhos = 0;

		// Efetivamente uma constante, não é constante pois não sabemos ao criar o objeto.
		private int tamanhoSequencia = 0;
		// Deve ser informado pelo experimento Um
		private int tamanhoBlocoTentativas = 0;
		private int tentativaBlocoAtual = 0;

		private int blocoAtual = 0;

		public void SetTamanhoSequencia(int tamanho) {
			if (tamanhoSequencia == 0) {
				tamanhoSequencia = tamanho;
			}
		}

		public void SetTamanhoBlocoTentativas(int tamanho) {
			if (tamanhoBlocoTentativas == 0) {
				tamanhoBlocoTentativas = tamanho;
			}
		}

		private void SomarPontos(int quantidade) {
			pontos += quantidade;
			ultimosPontosGanhos = quantidade;
		}

		// Variáveis para o resumo do experimento

		private int acertosCertezaLinhaDeBase;
		private int errosCertezaLinhaDeBase;
		private int acertosTalvezLinhaDeBase;
		private int errosTalvezLinhaDeBase;

		private int acertosCertezaCondicao1;
		private int errosCertezaCondicao1;
		private int acertosTalvezCondicao1;
		private int errosTalvezCondicao1;

		private int acertosCertezaCondicao2;
		private int errosCertezaCondicao2;
		private int acertosTalvezCondicao2;
		private int errosTalvezCondicao2;

		public override string NomeFaseAtual {
			get {
				if (faseAtual == 0) {
					return "Linha de Base";
				}
				if (faseAtual == 1) {
					return "Condição 1";
				}
				if (faseAtual == 2) {
					return "Condição 2";
				}
				return "Fim do Experimento";
			}
		}

		public static string GetNomeResumoFase(int numeroFase) {
			if (numeroFase == 0) {
				return "LB";
			}
			else if (numeroFase == 1) {
				return "C1";
			}
			return "C2";
		}

		public bool RegistrarResposta(bool acertou, bool certeza, string sequenciaModelo, string sequenciaDigitada) {
			// Nunca deve acontecer
			if (faseAtual > 2) {
				return true;
			}

			var origemResumo = faseAtual.ToString();
			var comparacaoSequencias = $"Sequência modelo: {sequenciaModelo}, Sequência digitada: {sequenciaDigitada}.";
			var acerto = acertou ? "acertou" : "errou";
			var cert = certeza ? "certeza" : "talvez";

			// Linha de Base
			if (faseAtual == 0) {
				tentativaBlocoAtual++;

				if (acertou) {
					if (certeza) {
						acertosCertezaLinhaDeBase++;
					}
					else {
						acertosTalvezLinhaDeBase++;
					}
				}
				else {
					if (certeza) {
						errosCertezaLinhaDeBase++;
					}
					else {
						errosTalvezLinhaDeBase++;
					}
				}
				RegistrarEvento($"Participante {acerto}, selecionou {cert}. {comparacaoSequencias}");

				if (tentativaBlocoAtual >= tamanhoBlocoTentativas) {
					tentativaBlocoAtual = 0;
					blocoAtual++;

					RegistrarEvento($"Fim do bloco de número {blocoAtual}/{experimentoDois.QuantidadeBlocosLinhaDeBase}");

					if (blocoAtual >= experimentoDois.QuantidadeBlocosLinhaDeBase) {
						blocoAtual = 0;
						faseAtual++;

						RegistrarEvento("Fim da Linha de Base");

						RegistrarEvento(new Evento(origemResumo, $"Acertos certeza;Erros certeza: {acertosCertezaLinhaDeBase};{errosCertezaLinhaDeBase}"));
						RegistrarEvento(new Evento(origemResumo, $"Acertos talvez;Erros talvez: {acertosTalvezLinhaDeBase};{errosTalvezLinhaDeBase}"));

						return true;
					}
				}
			}

			// Condição 1
			else if (faseAtual == 1) {
				if (acertou) {
					if (certeza) {
						acertosCertezaCondicao1++;
						SomarPontos(experimentoDois.PontosCertezaAcerto1);
					}
					else {
						acertosTalvezCondicao1++;
						SomarPontos(experimentoDois.PontosTalvezAcerto1);
					}
				}
				else {
					if (certeza) {
						errosCertezaCondicao1++;
						SomarPontos(experimentoDois.PontosCertezaErro1);
					}
					else {
						errosTalvezCondicao1++;
						SomarPontos(experimentoDois.PontosTalvezErro1);
					}
				}
				RegistrarEvento($"Participante {acerto}, selecionou {cert}, ganhou {ultimosPontosGanhos} pontos. {comparacaoSequencias}");

				tentativaBlocoAtual++;

				if (tentativaBlocoAtual >= tamanhoBlocoTentativas) {
					tentativaBlocoAtual = 0;
					blocoAtual++;

					RegistrarEvento($"Fim do bloco de número {blocoAtual}/{experimentoDois.QuantidadeBlocosCondicao1}");

					if (blocoAtual >= experimentoDois.QuantidadeBlocosCondicao1) {
						blocoAtual = 0;
						faseAtual++;

						RegistrarEvento("Fim da Condição 1");

						RegistrarEvento(new Evento(origemResumo, $"Acertos certeza;Erros certeza: {acertosCertezaCondicao1};{errosCertezaCondicao1}"));
						RegistrarEvento(new Evento(origemResumo, $"Acertos talvez;Erros talvez: {acertosTalvezCondicao1};{errosTalvezCondicao1}"));

						return true;
					}
				}
			}

			// Condição 2
			else if (faseAtual == 2) {
				if (acertou) {
					if (certeza) {
						acertosCertezaCondicao2++;
						SomarPontos(experimentoDois.PontosCertezaAcerto2);
					}
					else {
						acertosTalvezCondicao2++;
						SomarPontos(experimentoDois.PontosTalvezAcerto2);
					}
				}
				else {
					if (certeza) {
						errosCertezaCondicao2++;
						SomarPontos(experimentoDois.PontosCertezaErro2);
					}
					else {
						errosTalvezCondicao2++;
						SomarPontos(experimentoDois.PontosTalvezErro2);
					}
				}
				RegistrarEvento($"Participante {acerto}, selecionou {cert}, ganhou {ultimosPontosGanhos} pontos. {comparacaoSequencias}");

				tentativaBlocoAtual++;

				if (tentativaBlocoAtual >= tamanhoBlocoTentativas) {
					tentativaBlocoAtual = 0;
					blocoAtual++;

					RegistrarEvento($"Fim do bloco de número {blocoAtual}/{experimentoDois.QuantidadeBlocosCondicao2}");

					if (blocoAtual >= experimentoDois.QuantidadeBlocosCondicao2) {
						blocoAtual = 0;
						faseAtual++;

						RegistrarEvento("Fim da Condição 2");

						RegistrarEvento(new Evento(origemResumo, $"Acertos certeza;Erros certeza: {acertosCertezaCondicao2};{errosCertezaCondicao2}"));
						RegistrarEvento(new Evento(origemResumo, $"Acertos talvez;Erros talvez: {acertosTalvezCondicao2};{errosTalvezCondicao2}"));

						return true;
					}
				}
			}

			return false;
		}

		public string GerarNumero() {
			if (faseAtual <= 2) {
				return geradorNumeros.GerarNumero(tamanhoSequencia);
			}

			return null;
		}

		public string GrauAtual() {
			return ExperimentoDois.graus[Math.Min(pontos / experimentoDois.PontosPorGrau, ExperimentoDois.graus.Length - 1)];
		}
	}
}
