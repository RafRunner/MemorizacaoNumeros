using MemorizacaoNumeros.src.service;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoUmRealizado : EntidadeDeBanco {

		private static readonly string FORMATO_DATE_TIME = "yyyy-MM-dd HH:mm:ss";
		private static readonly string FORMATO_DATE_TIME_ARQUIVO = "yyyy-MM-dd HH-mm-ss";

		public long IdExperimentoUm { get; set; }
		private ExperimentoUm experimentoUm;
		public ExperimentoUm ExperimentoUm {
			get {
				if (experimentoUm == null) {
					experimentoUm = ExperimentoUmService.GetById(IdExperimentoUm);
				}
				return experimentoUm;
			}
			set {
				IdExperimentoUm = GetId(value);
				experimentoUm = value;
			}
		}

		public long IdExperimentador { get; set; }
		private Experimentador experimentador;
		public Experimentador Experimentador {
			get {
				if (experimentador == null) {
					ExperimentadorService.GetById(IdExperimentador);
				}
				return experimentador;
			}
			set {
				IdExperimentador = GetId(value);
				experimentador = value;
			}
		}

		public long IdParticipante { get; set; }
		private Participante participante;
		public Participante Participante {
			get {
				if (participante == null) {
					participante = ParticipanteService.GetById(IdParticipante);
				}
				return participante;
			}
			set {
				IdParticipante = GetId(value);
				participante = value;
			}
		}

		public string DataHoraInicio { get; set; }

		public DateTime DateTimeInicio {
			get => Convert.ToDateTime(DataHoraInicio, new CultureInfo("pt-BR"));
			set => DataHoraInicio = value.ToString(FORMATO_DATE_TIME);
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

		public string GetNomeArquivo() {
			return $"{DateTimeInicio.ToString(FORMATO_DATE_TIME_ARQUIVO)} - {Participante.Nome} - {Experimentador.Nome}";
		}

		public string Nome {
			get => $"{DataHoraInicio} - {Participante.Nome} - {Experimentador.Nome}";
		}

		// Inicio da parte do comportamento do experimento

		private readonly GeradorNumeros geradorNumeros = new GeradorNumeros();
		private readonly Random random = new Random();


		// 0 - Pré treino, 1 - Linha de Base, 2 - Fase Experimental
		public int faseAtual = 0;

		private int tamanhoAtualSequencia = 0;
		private int tentativaBlocoAtual = 0;

		private int corretasConsecutivasPreTreino = 0;

		private int tamanhoMaximoLinhaDeBase;
		private int talvezUltimoBlocoLinhaDeBase = 0;

		private int digitosASeremVariados = 1;
		private int quantidadeEstimulosFracos = 0;
		private int talvezEstimulosFracos = 0;
		private int blocosExecutados = 0;

		// Retorna true se terminamos uma fase/fomos para uma nova
		public bool RegistrarResposta(bool acertou, bool certeza) {
			// Pré treino
			if (faseAtual == 0) {
				tamanhoAtualSequencia = ExperimentoUm.TamanhoSequenciaInicial;

				if (acertou) {
					corretasConsecutivasPreTreino++;

					// O participante acertou quantidade necessária de vezes consecutivas, vamos para a próxima fase
					if (corretasConsecutivasPreTreino == ExperimentoUm.CriterioAcertoPreTreino) {
						corretasConsecutivasPreTreino = 0;
						faseAtual++;
						return true;
					}
				}
				else {
					corretasConsecutivasPreTreino = 0;
				}
			}
			// Linha de Base
			else if (faseAtual == 1) {
				tentativaBlocoAtual++;

				if (!certeza) {
					talvezUltimoBlocoLinhaDeBase++;
				}

				// Acabamos de terminar um bloco
				if (tentativaBlocoAtual == ExperimentoUm.TamanhoBlocoTentativas) {
					// O participante escolheu talvez acima do trashold
					if (talvezUltimoBlocoLinhaDeBase >= ExperimentoUm.CalculaCriterioTalvezLinhaDeBase()) {
						tentativaBlocoAtual = 0;
						talvezUltimoBlocoLinhaDeBase = 0;
						tamanhoMaximoLinhaDeBase = tamanhoAtualSequencia;
						faseAtual++;
						return true;
					}
					// O tamanho da sequência aumenta
					else {
						tamanhoAtualSequencia++;
						tentativaBlocoAtual = 0;
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

					// Terminamos o número de blocos previstos
					if (blocosExecutados == ExperimentoUm.NumeroBlocosFaseExperimental) {
						if (talvezEstimulosFracos < ExperimentoUm.CalculaCriterioReforcoFaseExperimental(quantidadeEstimulosFracos)) {
							digitosASeremVariados++;
							tentativaBlocoAtual = 0;
							blocosExecutados = 0;
							quantidadeEstimulosFracos = 0;
							talvezEstimulosFracos = 0;

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
				return geradorNumeros.GerarNumero(tamanhoAtualSequencia);
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
