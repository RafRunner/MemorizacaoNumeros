using MemorizacaoNumeros.src.model;
using MemorizacaoNumeros.src.service;
using MemorizacaoNumeros.src.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemorizacaoNumeros {
	static class Program {
		/// <summary>
		/// Ponto de entrada principal para o aplicativo.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.ThreadException += new ThreadExceptionEventHandler(Form1_UIThreadException);

			var experimento = new ExperimentoUm {
				InstrucaoInicial = "Au au au",
				TempoTelaPretaInicial = 5,
				TempoTelaPretaITI = 10,
				TempoApresentacaoEstimulo = 15,
				TamanhoBlocoTentativas = 7,
				CriterioAcertoPreTreino = 7,
				CriterioTalvezLinhaDeBase = 60,
				CriterioReforcoFaseExperimental = 80
			};

			ExperimentoUmService.Salvar(experimento);

			Console.WriteLine(experimento.Id);

			ExperimentoUmService.GetAll().ForEach(e => {
				Console.WriteLine(e.ToString());
			});

			experimento.InstrucaoInicial = "Miau Miau Miau";

			ExperimentoUmService.Salvar(experimento);

			ExperimentoUmService.GetAll().ForEach(e => {
				Console.WriteLine(e.ToString());
			});

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MenuInicial());
		}

		private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t) {
			ShowThreadExceptionDialog("Erro", false, t.Exception);
		}

		private static void ShowThreadExceptionDialog(string title, bool debug, Exception e) {
			string errorMsg;
			if (debug) {
				errorMsg = "Ocorreu um erro! Mensagem:\n\n";
				errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
			}
			else {
				errorMsg = e.Message;
			}
			MessageBox.Show(errorMsg, title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
		}
	}
}
