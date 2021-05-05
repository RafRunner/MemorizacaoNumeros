using MemorizacaoNumeros.src.service;
using System;
using System.Globalization;

namespace MemorizacaoNumeros.src.model {
	public class ExperimentoRealizado : EntidadeDeBanco {

		public static readonly string FORMATO_DATE_TIME = "yyyy-MM-dd HH:mm:ss";
		private static readonly string FORMATO_DATE_TIME_ARQUIVO = "yyyy-MM-dd HH-mm-ss";

		public long IdExperimentador { get; set; }
		private Experimentador experimentador;
		public Experimentador Experimentador {
			get {
				if (experimentador == null) ExperimentadorService.GetById(IdExperimentador);
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
				if (participante == null) participante = ParticipanteService.GetById(IdParticipante);
				return participante;
			}
			set {
				IdParticipante = GetId(value);
				participante = value;
			}
		}

		public long IdExperimentoUmRealizado { get; set; }
		private ExperimentoUmRealizado experimentoUmRealizado;
		public ExperimentoUmRealizado ExperimentoUmRealizado {
			get {
				if (experimentoUmRealizado == null) experimentoUmRealizado = ExperimentoUmRealizadoService.GetById(IdExperimentoUmRealizado);
				return experimentoUmRealizado;
			}
			set {
				IdExperimentoUmRealizado = GetId(value);
				experimentoUmRealizado = value;
			}
		}

		public long IdExperimentoDoisRealizado { get; set; }
		private ExperimentoDoisRealizado experimentoDoisRealizado;
		public ExperimentoDoisRealizado ExperimentoDoisRealizado {
			get {
				if (experimentoDoisRealizado == null) experimentoDoisRealizado = ExperimentoDoisRealizadoService.GetById(IdExperimentoDoisRealizado);
				return experimentoDoisRealizado;
			}
			set {
				IdExperimentoDoisRealizado = GetId(value);
				experimentoDoisRealizado = value;
			}
		}

		public string DataHoraInicio { get; set; }

		public DateTime DateTimeInicio {
			get => Convert.ToDateTime(DataHoraInicio, new CultureInfo("pt-BR"));
			set => DataHoraInicio = value.ToString(FORMATO_DATE_TIME);
		}

		public string GetNomeArquivo() {
			return $"{DateTimeInicio.ToString(FORMATO_DATE_TIME_ARQUIVO)} - {Participante.Nome} - {Experimentador.Nome}";
		}

		public string Nome {
			get => $"{DataHoraInicio} - {Participante.Nome} - {Experimentador.Nome}";
		}
	}
}
