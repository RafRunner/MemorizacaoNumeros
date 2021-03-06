using MemorizacaoNumeros.src.service;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MemorizacaoNumeros.src.model {
	public abstract class IExperimentoRealizado : EntidadeDeBanco {
		public string DataHoraInicio { get; set; }

		public DateTime DateTimeInicio {
			get => Convert.ToDateTime(DataHoraInicio, new CultureInfo("pt-BR"));
			set => DataHoraInicio = value.ToString(ExperimentoRealizado.FORMATO_DATE_TIME);
		}

		private List<Evento> eventos = new List<Evento>();

		public void RegistrarEvento(string descricao, bool debug = false) {
			var origem = debug ? "debug" : NomeFaseAtual;
			RegistrarEvento(new Evento(origem, descricao));
		}

		public void RegistrarEvento(Evento evento) {
			evento.Horario = Convert.ToInt64((DateTime.Now - DateTimeInicio).TotalMilliseconds);
			evento.Indice = eventos.Count;
			eventos.Add(evento);

			if (this.GetType() == typeof(ExperimentoUmRealizado)) {
				evento.IdExperimentoUmRealizado = Id;
			}
			else {
				evento.IdExperimentoDoisRealizado = Id;
			}
			EventoService.Salvar(evento);
		}

		public List<Evento> GetListaEventos() {
			return eventos;
		}

		public void SetListaEventos(List<Evento> eventos) {
			this.eventos = eventos;
		}

		public abstract string NomeFaseAtual { get; }
	}
}
