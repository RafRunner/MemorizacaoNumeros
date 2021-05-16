using MemorizacaoNumeros.src.service;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoDoisRealizado : EntidadeDeBanco {

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

		public string DataHoraInicio { get; set; }

		public DateTime DateTimeInicio {
			get => Convert.ToDateTime(DataHoraInicio, new CultureInfo("pt-BR"));
			set => DataHoraInicio = value.ToString(ExperimentoRealizado.FORMATO_DATE_TIME);
		}

		private List<Evento> eventos = new List<Evento>();

		public void RegistrarEvento(Evento evento) {
			evento.Horario = Convert.ToInt64((DateTime.Now - DateTimeInicio).TotalSeconds);
			eventos.Add(evento);
		}

		public List<Evento> GetListaEventos() {
			return eventos;
		}

		public void SetListaEventos(List<Evento> eventos) {
			this.eventos = eventos;
		}

		// Inicio da parte do comportamento do experimento

		private readonly GeradorNumeros geradorNumeros = new GeradorNumeros();

		// 0 - Linha de Base, 1 - Condição 1, 2 - Condição 2
		public int faseAtual = 0;
		public int pontos = 0;
		public int ultimosPontosGanhos = 0;

		// Eferivamente uma constante, não é constante pois não sabemos ao criar o objeto.
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

		public bool RegistrarResposta(bool acertou, bool certeza, string sequenciaModelo, string sequenciaDigitada) {
			// Linha de Base
			if (faseAtual == 0) {
				tentativaBlocoAtual++;

				if (tentativaBlocoAtual == tamanhoBlocoTentativas) {
					tentativaBlocoAtual = 0;
					blocoAtual++;

					if (blocoAtual == experimentoDois.QuantidadeBlocosLinhaDeBase) {
						blocoAtual = 0;
						faseAtual++;

						return true;
					}
				}
			}

			// Condição 1
			else if (faseAtual == 1) {
				if (acertou) {
					if (certeza) {
						SomarPontos(experimentoDois.PontosCertezaAcerto1);
					}
					else {
						SomarPontos(experimentoDois.PontosTalvezAcerto1);
					}
				}
				else {
					if (certeza) {
						SomarPontos(experimentoDois.PontosCertezaErro1);
					}
					else {
						SomarPontos(experimentoDois.PontosTalvezErro1);
					}
				}

				tentativaBlocoAtual++;

				if (tentativaBlocoAtual == tamanhoBlocoTentativas) {
					tentativaBlocoAtual = 0;
					blocoAtual++;

					if (blocoAtual == experimentoDois.QuantidadeBlocosCondicao1) {
						blocoAtual = 0;
						faseAtual++;

						return true;
					}
				}
			}

			// Condição 2
			else if (faseAtual == 2) {
				if (acertou) {
					if (certeza) {
						SomarPontos(experimentoDois.PontosCertezaAcerto2);
					}
					else {
						SomarPontos(experimentoDois.PontosTalvezAcerto2);
					}
				}
				else {
					if (certeza) {
						SomarPontos(experimentoDois.PontosCertezaErro2);
					}
					else {
						SomarPontos(experimentoDois.PontosTalvezErro2);
					}
				}

				tentativaBlocoAtual++;

				if (tentativaBlocoAtual == tamanhoBlocoTentativas) {
					tentativaBlocoAtual = 0;
					blocoAtual++;

					if (blocoAtual == experimentoDois.QuantidadeBlocosCondicao2) {
						blocoAtual = 0;
						faseAtual++;

						return true;
					}
				}
			}

			// Nunca deve acontecer
			else if (faseAtual > 2) {
				return true;
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
			return ExperimentoDois.graus[Math.Min(pontos / experimentoDois.PontosPorGrau, ExperimentoDois.graus.Length)];
		}
	}
}
