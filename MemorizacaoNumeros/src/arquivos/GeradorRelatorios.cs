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

		public bool ExperimentoJaTemRelatorioNaPasta() {
			return File.Exists(Ambiente.GetCaminhoAbsoluto(nomePasta, experimento.GetNomeArquivo()));
		}

		public void GerarRelatorio() {
			Ambiente.CriaDiretorioAmbiente(nomePasta);
			var nomeArquivo = experimento.GetNomeArquivo();
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

				var horarioEvento = horaInicio.AddMilliseconds(evento.Horario);

				relatorio.Append(horarioEvento.ToString(formatoHora)).Append(" - ").Append(evento.Origem).Append(" - ").AppendLine(evento.Texto);
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

			var inicioExperimentoDois = experimento.ExperimentoDoisRealizado.DateTimeInicio;

			relatorio.Append("Experimento Dois iniciado (após leitura da instrução) às ").AppendLine(inicioExperimentoDois.ToString(formatoHora))
			.AppendLine("Eventos do Experimento Dois:\n");

			AppendEventos(eventosExperimentoDois, inicioExperimentoDois);

			AppendSeparador();
		}

		private void AppendEventosResumoExp1(List<Evento> eventos) {
			foreach (var evento in eventos) {
				if (!StringUtils.EhNumero(evento.Origem.Substring(0, 1))) continue;

				var faseAtual = Convert.ToInt32(evento.Origem);

				relatorio.Append(ExperimentoUmRealizado.GetNomeResumoFase(faseAtual)).Append(" - ").AppendLine(evento.Texto);
			}
		}

		private void AppendEventosResumoExp2(List<Evento> eventos) {
			foreach (var evento in eventos) {
				if (!StringUtils.EhNumero(evento.Origem.Substring(0, 1))) continue;

				var faseAtual = Convert.ToInt32(evento.Origem);

				relatorio.Append(ExperimentoDoisRealizado.GetNomeResumoFase(faseAtual)).Append(" - ").AppendLine(evento.Texto);
			}
		}

		private void RegistrarResumo() {
			var eventosExperimentoUm = experimento.ExperimentoUmRealizado.GetListaEventos();
			var eventosExperimentoDois = experimento.ExperimentoDoisRealizado.GetListaEventos();

			relatorio.AppendLine("Resumo dos resultados:")
			.AppendLine("\nExperimento Um:");

			AppendEventosResumoExp1(eventosExperimentoUm);

			relatorio.AppendLine("\nExperimento Dois:");

			AppendEventosResumoExp2(eventosExperimentoDois);
		}
	}
}
