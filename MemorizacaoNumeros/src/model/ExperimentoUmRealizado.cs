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

		private GeradorNumeros geradorNumeros = new GeradorNumeros();

		// 0 - Pré treino, 1 - Linha de Base, 2 - Fase Experimental
		public int faseAtual = 0;

		private int corretasConsecutivasPreTreino = 0;
		private int tamanhoAtualSequencia;
		private int tamanhoMaximoLinhaDeBase;
		private int talvezUltimoBlocoLinhaDeBase = 0;
		private int tentativaBlocoAtual = 0;

		public string GeraNumero(bool novaFase, bool acertou, bool certeza) {
			// Pré treino
			if (faseAtual == 0) {
				tamanhoAtualSequencia = ExperimentoUm.TamanhoSequenciaInicial;

				if (novaFase) {
					return geradorNumeros.GerarNumero(tamanhoAtualSequencia);
				}

				if (acertou) {
					corretasConsecutivasPreTreino++;

					if (corretasConsecutivasPreTreino == ExperimentoUm.CriterioAcertoPreTreino) {
						corretasConsecutivasPreTreino = 0;
						faseAtual++;

						return null;
					}
				}
				else {
					corretasConsecutivasPreTreino = 0;
				}

				return geradorNumeros.GerarNumero(tamanhoAtualSequencia);
			}
			// Linha de Base
			if (faseAtual == 1) {
				if (novaFase) {
					return geradorNumeros.GerarNumero(tamanhoAtualSequencia);
				}

				tentativaBlocoAtual++;

				if (!certeza) {
					talvezUltimoBlocoLinhaDeBase++;
				}

				// Acabamos de terminar um bloco
				if (tentativaBlocoAtual == ExperimentoUm.TamanhoBlocoTentativas) {
					// O participante escolheu talvez acima do trashold
					if (talvezUltimoBlocoLinhaDeBase >= Math.Ceiling(ExperimentoUm.TamanhoBlocoTentativas * ((float)ExperimentoUm.CriterioTalvezLinhaDeBase / 100))) {
						tentativaBlocoAtual = 0;
						talvezUltimoBlocoLinhaDeBase = 0;
						tamanhoMaximoLinhaDeBase = tamanhoAtualSequencia;
						faseAtual++;

						return null;
					}
					// O tamanho da sequência aumenta
					else {
						tamanhoAtualSequencia++;
						tentativaBlocoAtual = 0;
						talvezUltimoBlocoLinhaDeBase = 0;

						return geradorNumeros.GerarNumero(tamanhoAtualSequencia);
					}
				}

				return geradorNumeros.GerarNumero(tamanhoAtualSequencia);
			}

			// Experimento acabou
			return "acabou";
		}
	}
}
