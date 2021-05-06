using MemorizacaoNumeros.src.model;
using MemorizacaoNumeros.src.service;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MemorizacaoNumeros.src.view {
	public partial class MenuInicial : Form {
		public MenuInicial() {
			InitializeComponent();

			var experimentosUm = ExperimentoUmService.GetAll();
			var experimentosDois = ExperimentoDoisService.GetAll();

			var experimentoUm = experimentosUm.First();
			var experimentoUmRealizado = new ExperimentoUmRealizado() {
				ExperimentoUm = experimentoUm
			};

			var experimentoDois = experimentosDois.First();
			var experimentoDoisRealizado = new ExperimentoDoisRealizado() {
				ExperimentoDois = experimentoDois
			};

			var experimentoRealizado = new ExperimentoRealizado {
				ExperimentoUmRealizado = experimentoUmRealizado,
				ExperimentoDoisRealizado = experimentoDoisRealizado
			};

			var telaBackgroud = new TelaMensagem("", false);
			telaBackgroud.BackColor = Color.Black;
			telaBackgroud.Show();
			new TelaMensagem(experimentosUm.First().InstrucaoInicial, true).ShowDialog();
			new ExperimentoView(experimentoRealizado).ShowDialog();
		}
	}
}
