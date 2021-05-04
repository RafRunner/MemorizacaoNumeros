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
	}
}
