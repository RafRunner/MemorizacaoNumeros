using MemorizacaoNumeros.src.model;
using MemorizacaoNumeros.src.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MemorizacaoNumeros.src.arquivos {
	public class GeradorRelatorios {

		public static readonly string nomePasta = "relatórios";

		private static readonly string formatoHora = "HH:mm:ss";

		private ExperimentoRealizado experimento;
		private StringBuilder relatorio = new StringBuilder();

		public GeradorRelatorios(ExperimentoRealizado experimento) {
			this.experimento = experimento;
		}

		public void GerarRelatorio() {
			Ambiente.CriaDiretorioAmbiente(nomePasta);
			var nomeArquivo = experimento.GetNomeArquivo() + ".txt";
			var caminhoCompleto = Ambiente.GetCaminhoAbsoluto(nomePasta, nomeArquivo);

			GeraCabecalho();
			RegistrarEventos();
			RegistrarResumo();

			File.WriteAllText(caminhoCompleto, relatorio.ToString());
		}

		private void AppendSeparador() {
			relatorio.AppendLine()
			.AppendLine(@"////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////")
			.AppendLine();
		}

		private void GeraCabecalho() {
			relatorio.Append("Relatório do experimento realizado em ").AppendLine(experimento.DataHoraInicio)
			.AppendLine("\nParticipante:").AppendLine(experimento.Participante.ToString())
			.AppendLine("\nExperimentador:").AppendLine(experimento.Experimentador.ToString())
			.AppendLine("\nDetalhes da configuração do experimento:")
			.AppendLine("\nExperimento Um:").AppendLine(experimento.ExperimentoUmRealizado.ExperimentoUm.ToString())
			.AppendLine("\nExperimento Dois").AppendLine(experimento.ExperimentoDoisRealizado.ExperimentoDois.ToString());

			AppendSeparador();
		}

		private void AppendEventos(List<Evento> eventos, DateTime horaInicio) {
			foreach (var evento in eventos) {
				if (StringUtils.EhNumero(evento.Origem.Substring(0, 1))) continue;

				var horarioEvento = horaInicio.AddSeconds(evento.Horario);

				relatorio.Append(horarioEvento.ToString(formatoHora)).Append(" - ").AppendLine(evento.Texto);
			}
		}

		private void RegistrarEventos() {
			var eventosExperimentoUm = experimento.ExperimentoUmRealizado.GetListaEventos();
			var eventosExperimentoDois = experimento.ExperimentoDoisRealizado.GetListaEventos();

			var inicioExperimentoUm = experimento.ExperimentoUmRealizado.DateTimeInicio;

			relatorio.Append("Experimento Um iniciado (após leitura da instrução) às ").AppendLine(inicioExperimentoUm.ToString(formatoHora))
			.AppendLine("Eventos do Experimento Um:\n");

			AppendEventos(eventosExperimentoUm, inicioExperimentoUm);

			AppendSeparador();

			var inicioExperimentoDois = experimento.ExperimentoUmRealizado.DateTimeInicio;

			relatorio.Append("Experimento Dois iniciado (após leitura da instrução) às ").AppendLine(inicioExperimentoDois.ToString(formatoHora))
			.AppendLine("Eventos do Experimento Dois:\n");

			AppendEventos(eventosExperimentoDois, inicioExperimentoDois);

			AppendSeparador();
		}

		private void AppendEventosResumo(List<Evento> eventos) {
			foreach (var evento in eventos) {
				if (!StringUtils.EhNumero(evento.Origem.Substring(0, 1))) continue;

				relatorio.Append(evento.Origem).Append(" - ").AppendLine(evento.Texto);
			}
		}

		private void RegistrarResumo() {
			var eventosExperimentoUm = experimento.ExperimentoUmRealizado.GetListaEventos();
			var eventosExperimentoDois = experimento.ExperimentoDoisRealizado.GetListaEventos();

			relatorio.AppendLine("Resumo dos resultados:")
			.AppendLine("\nExperimento Um:");

			AppendEventosResumo(eventosExperimentoUm);

			relatorio.AppendLine("\nExperimento Dois:");

			AppendEventosResumo(eventosExperimentoDois);
		}
	}
}
