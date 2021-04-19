﻿using MemorizacaoNumeros.src.model;
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

			var g = new GeradorNumeros();

			for (int i = 0; i < 1000; i++) {
				Console.WriteLine(g.GerarNumero(2));
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MenuInicial());
		}

		private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t) {
			try {
				ShowThreadExceptionDialog("Erro", false, t.Exception);
			}
			catch {
				try {
					MessageBox.Show("Erro fatal", "Um erro fatal inexperado ocorreu e o aplicativo será encerrado. Entre em contato com os desenvolvedores.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
				finally {
					Application.Exit();
				}
			}
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
