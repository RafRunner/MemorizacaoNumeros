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

		public bool RegistrarResposta(bool acertou, bool certeza) {
			return false;
		}

		public string GerarNumero() {
			return "coca cola espumente";
		}
	}
}
