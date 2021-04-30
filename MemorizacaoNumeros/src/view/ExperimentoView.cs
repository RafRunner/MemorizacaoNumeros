using MemorizacaoNumeros.src.model;
using MemorizacaoNumeros.src.util;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemorizacaoNumeros.src.view {
	public partial class ExperimentoView : Form {

		private readonly int height = Screen.PrimaryScreen.Bounds.Height;
		private readonly int width = Screen.PrimaryScreen.Bounds.Width;

		private readonly Random random = new Random();
		private readonly Experimento experimento;
		private readonly ExperimentoUmRealizado experimentoRealizado;

		private string inputAnterior = "";

		// Variáveis do timer de fade
		private bool fadingIn;
		private Form whatToFade;

		public ExperimentoView(Experimento experimento) {
			InitializeComponent();

			Location = new Point(0, 0);
			Size = new Size(width, height);

			var heightRatio = height / 1080.0;
			var widthRatio = width / 1920.0;

			this.experimento = experimento;

			var experimentoRealizado = new ExperimentoUmRealizado();
			experimentoRealizado.ExperimentoUm = (ExperimentoUm) experimento;

			this.experimentoRealizado = experimentoRealizado;

			ViewUtils.CorrigeEscalaTodosOsFilhos(this, heightRatio, widthRatio);

			IniciarNovaFase();
		}

		private async void IniciarNovaFase() {
			if (experimento.TempoTelaPretaInicial > 0) {
				Opacity = 0;
				await Task.Delay(experimento.TempoTelaPretaInicial * 1000);
				FadeIn(this, 1);
			}

			IniciarNovoNumero();
		}

		private async void IniciarNovoNumero() {
			tbInput.Text = "";
			pnNumero.Visible = true;
			btnCerteza.Enabled = true;
			btnTalvez.Enabled = true;
			pnCorreto.Visible = false;
			btnCerteza.Visible = false;
			btnTalvez.Visible = false;
			pnInput.Visible = false;
			SortearPosicaoBotoes();

			var novoNumero = experimentoRealizado.GeraNumero();

			// Acabou o experimento
			if (novoNumero == null) {
				Close();
				return;
			}

			lblNumero.Text = novoNumero;
			lblNumero.Location = new Point {
				Y = lblNumero.Location.Y,
				X = (pnNumero.Size.Width - lblNumero.Size.Width) / 2
			};

			await Task.Delay(experimento.TempoApresentacaoEstimulo * 1000);

			pnNumero.Visible = false;
			btnCerteza.Visible = true;
			btnTalvez.Visible = true;
		}

		private void btnCerteza_Click(object sender, EventArgs e) {
			if (!btnTalvez.Enabled) return;

			btnTalvez.Enabled = false;
			pnInput.Visible = true;
			tbInput.Focus();
		}

		private void btnTalvez_Click(object sender, EventArgs e) {
			if (!btnTalvez.Enabled) return;

			btnCerteza.Enabled = false;
			pnInput.Visible = true;
			tbInput.Focus();
		}

		private void SortearPosicaoBotoes() {
			var deveTrocar = random.Next(2) == 1;
			
			if (deveTrocar) {
				var temp = btnCerteza.Location;
				btnCerteza.Location = btnTalvez.Location;
				btnTalvez.Location = temp;
			}
		}

		private async void tbInput_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyData == Keys.Enter) {
				// Enter foi pressionado
				var sequenciaDigitada = tbInput.Text;

				if (string.IsNullOrWhiteSpace(sequenciaDigitada)) return;

				e.Handled = true;
				e.SuppressKeyPress = true;

				var faseAnterior = experimentoRealizado.faseAtual;

				experimentoRealizado.RegistrarResposta(sequenciaDigitada == lblNumero.Text, btnCerteza.Enabled);

				var novaFase = experimentoRealizado.faseAtual;

				// TODO o comportamente aqui vai depender da fase atual
				if (experimento.TempoTelaPretaITI > 0) {
					tbInput.Enabled = false;
					if (sequenciaDigitada == lblNumero.Text) {
						pnCorreto.Visible = true;
						await Task.Delay(experimento.TempoTelaPretaITI * 1000);
						pnCorreto.Visible = true;
					}
					else {
						FadeOut(this, 1);
						await Task.Delay(experimento.TempoTelaPretaITI * 1000);
						FadeIn(this, 1);
					}
					tbInput.Enabled = true;
				}

				if (faseAnterior == novaFase) {
					IniciarNovoNumero();
				} else {
					IniciarNovaFase();
				}
			}
		}

		private void tbInput_TextChanged(object sender, EventArgs e) {
			if (tbInput.Text != "" && Regex.IsMatch(tbInput.Text, "[^0-9]")) {
				var startAnterior = tbInput.SelectionStart - 1;
				var lengthAnterior = tbInput.SelectionLength;
				tbInput.Text = inputAnterior;
				tbInput.SelectionStart = startAnterior;
				tbInput.SelectionLength = lengthAnterior;
			} else {
				inputAnterior = tbInput.Text;
			}
		}

		// TODO: melhorar essa parte do fade para ser mais genérica, útil e segura
		private void FadeIn(Form whatToFade, int seconds) {
			fadingIn = true;
			Fade(whatToFade, seconds);
		}

		private void FadeOut(Form whatToFade, int seconds) {
			fadingIn = false;
			Fade(whatToFade, seconds);
		}

		private void Fade(Form whatToFade, int seconds) {
			this.whatToFade = whatToFade;
			timerFade.Interval = seconds * 1000 / (int)(1 / 0.025);
			timerFade.Start();
		}

		private void timerFade_Tick(object sender, EventArgs e) {
			if (fadingIn) {
				if (whatToFade.Opacity < 1.0) {
					whatToFade.Opacity += 0.025;
				}
				else {
					timerFade.Stop();
				}
			}
			else {
				if (whatToFade.Opacity > 0) {
					whatToFade.Opacity -= 0.025;
				}
				else {
					timerFade.Stop();
				}
			}
		}
	}
}
