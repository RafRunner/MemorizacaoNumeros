using MemorizacaoNumeros.src.model;
using MemorizacaoNumeros.src.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemorizacaoNumeros.src.view {
	public partial class MenuInicial : Form {
		public MenuInicial() {
			InitializeComponent();

			List<ExperimentoUm> experimentos = ExperimentoUmService.GetAll();

			var telaBackgroud = new TelaMensagem("", false);
			telaBackgroud.BackColor = Color.Black;
			telaBackgroud.Show();
			new TelaMensagem(experimentos.First().InstrucaoInicial, true).ShowDialog();
			new ExperimentoUmView(experimentos.First()).ShowDialog();
		}
	}
}
