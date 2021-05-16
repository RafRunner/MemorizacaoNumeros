using MemorizacaoNumeros.src.service;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoUmRealizado : EntidadeDeBanco {

		public long IdExperimentoUm { get; set; }
		private ExperimentoUm experimentoUm;
		public ExperimentoUm ExperimentoUm {
			get {
				if (experimentoUm == null) experimentoUm = ExperimentoUmService.GetById(IdExperimentoUm);
				return experimentoUm;
			}
			set {
				IdExperimentoUm = GetId(value);
				experimentoUm = value;
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
		private readonly Random random = new Random();


		// 0 - Pré treino, 1 - Linha de Base, 2 - Fase Experimental
		public int faseAtual = 0;

		private int tamanhoAtualSequencia = 0;
		private int tentativaBlocoAtual = 0;

		private int corretasConsecutivasPreTreino = 0;

		public int tamanhoMaximoLinhaDeBase;
		private int talvezUltimoBlocoLinhaDeBase = 0;

		private int digitosASeremVariados = 1;
		private int quantidadeEstimulosFracos = 0;
		private int talvezEstimulosFracos = 0;
		private int blocosExecutados = 0;

		// Retorna true se terminamos uma fase/fomos para uma nova
		public bool RegistrarResposta(bool acertou, bool certeza, string sequenciaModelo, string sequenciaDigitada) {
			// Supostamente nunca deve acontecer
			if (faseAtual > 2) {
				return true;
			}

			var origem = $"1{faseAtual}";
			var comparacaoSequencias = $"Sequência modelo: {sequenciaModelo}, Sequência digitada: {sequenciaDigitada}.";
			var acerto = acertou ? "acertou" : "errou";
			var cert = certeza ? "certeza" : "talvez";

			// Pré treino
			if (faseAtual == 0) {
				tamanhoAtualSequencia = ExperimentoUm.TamanhoSequenciaInicial;

				if (acertou) {
					corretasConsecutivasPreTreino++;
					//RegistrarEvento(new Evento(origem, $"Participante acertou. Acertos consecutivos: {corretasConsecutivasPreTreino}. {comparacaoSequencias}"));

					// O participante acertou quantidade necessária de vezes consecutivas, vamos para a próxima fase
					if (corretasConsecutivasPreTreino == ExperimentoUm.CriterioAcertoPreTreino) {
						//RegistrarEvento(new Evento(origem, $"Acertos consecutivos necessários para finalizar Pré treino alcançados."));
						corretasConsecutivasPreTreino = 0;
						faseAtual++;
						return true;
					}
				}
				else {
					//RegistrarEvento(new Evento(origem, $"Participante errou. {comparacaoSequencias}"));
					corretasConsecutivasPreTreino = 0;
				}
			}
			// Linha de Base
			else if (faseAtual == 1) {
				tentativaBlocoAtual++;

				if (!certeza) {
					talvezUltimoBlocoLinhaDeBase++;
				}

				//RegistrarEvento(new Evento(origem, $"Participante {acerto}. Participante selecionou {cert}. Talvez selecionados: {talvezUltimoBlocoLinhaDeBase}. {comparacaoSequencias}"));

				// Acabamos de terminar um bloco
				if (tentativaBlocoAtual == ExperimentoUm.TamanhoBlocoTentativas) {
					tentativaBlocoAtual = 0;

					// O participante escolheu talvez acima do trashold
					if (talvezUltimoBlocoLinhaDeBase >= ExperimentoUm.CalculaCriterioTalvezLinhaDeBase()) {
						tamanhoMaximoLinhaDeBase = tamanhoAtualSequencia;
						talvezUltimoBlocoLinhaDeBase = 0;
						faseAtual++;
						return true;
					}
					// O tamanho da sequência aumenta
					else {
						tamanhoAtualSequencia++;
						talvezUltimoBlocoLinhaDeBase = 0;
					}
				}
			}

			// Fase Experimental
			else if (faseAtual == 2) {
				tentativaBlocoAtual++;

				if (tamanhoAtualSequencia > tamanhoMaximoLinhaDeBase) {
					quantidadeEstimulosFracos++;
					if (!certeza) {
						talvezEstimulosFracos++;
					}
				}

				// Acabamos de terminar um bloco
				if (tentativaBlocoAtual == ExperimentoUm.TamanhoBlocoTentativas) {
					blocosExecutados++;
					tentativaBlocoAtual = 0;

					// Terminamos o número de blocos previstos
					if (blocosExecutados == ExperimentoUm.NumeroBlocosFaseExperimental) {
						blocosExecutados = 0;

						if (talvezEstimulosFracos < ExperimentoUm.CalculaCriterioReforcoFaseExperimental(quantidadeEstimulosFracos)) {
							quantidadeEstimulosFracos = 0;
							talvezEstimulosFracos = 0;
							digitosASeremVariados++;
						}
						else {
							quantidadeEstimulosFracos = 0;
							talvezEstimulosFracos = 0;
							faseAtual++;
							return true;
						}
					}
				}
			}

			return false;
		}

		// Função que vai gerando os números para o experimento. Se retornar null, o experimento acabou (talvez mudar isso)
		public string GeraNumero() {
			// Acabou o experimento
			if (faseAtual > 2) {
				return null;
			}

			// Inicializando o tamanho da sequência
			if (faseAtual == 0 && tamanhoAtualSequencia == 0) {
				tamanhoAtualSequencia = ExperimentoUm.TamanhoSequenciaInicial;
			}

			// Fase Experimental
			if (faseAtual == 2) {
				tamanhoAtualSequencia = tamanhoMaximoLinhaDeBase + digitosASeremVariados * (random.Next(2) == 1 ? -1 : 1);
				if (tamanhoAtualSequencia < 1) {
					tamanhoAtualSequencia = 1;
				}
			}

			// Experimento acabou
			return geradorNumeros.GerarNumero(tamanhoAtualSequencia);
		}
	}
}
