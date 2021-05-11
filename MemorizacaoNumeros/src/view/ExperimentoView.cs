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
		private readonly ExperimentoUm experimentoUm;
		private readonly ExperimentoDois experimentoDois;
		private readonly ExperimentoUmRealizado experimentoUmRealizado;
		private readonly ExperimentoDoisRealizado experimentoDoisRealizado;

		private int experimentoAtual = 1;

		private string inputAnterior = "";
		private float tamanhoFonteOriginal;

		// Variáveis do timer de fade
		private bool fadingIn;
		private Form whatToFade;

		public ExperimentoView(ExperimentoRealizado experimentoRealizado) {
			InitializeComponent();

			Location = new Point(0, 0);
			Size = new Size(width, height);

			var heightRatio = height / 1080.0;
			var widthRatio = width / 1920.0;

			this.experimentoUmRealizado = experimentoRealizado.ExperimentoUmRealizado;
			this.experimentoUm = this.experimentoUmRealizado.ExperimentoUm;

			this.experimentoDoisRealizado = experimentoRealizado.ExperimentoDoisRealizado;
			this.experimentoDois = this.experimentoDoisRealizado.ExperimentoDois;

			ViewUtils.CorrigeEscalaTodosOsFilhos(this, heightRatio, widthRatio);

			tamanhoFonteOriginal = lblNumero.Font.Size;

			IniciarNovaFase();
		}

		private async void IniciarNovaFase() {
			Opacity = 0;
			await Task.Delay(experimentoUm.TempoTelaPretaInicial * 1000);
			FadeIn(this, 1);

			IniciarNovoNumero();
		}

		private async void IniciarNovoNumero() {
			string novoNumero;

			if (experimentoAtual == 1) {
				novoNumero = experimentoUmRealizado.GeraNumero();

				// Iniciamos o experimento 2
				if (novoNumero == null) {
					experimentoAtual++;
					experimentoDoisRealizado.SetTamanhoSequencia(experimentoUmRealizado.tamanhoMaximoLinhaDeBase);
					experimentoDoisRealizado.SetTamanhoBlocoTentativas(experimentoUm.TamanhoBlocoTentativas);
					new TelaMensagem(experimentoDois.InstrucaoInicial, true).ShowDialog();
					IniciarNovaFase();
					return;
				}
			}
			else {
				novoNumero = experimentoDoisRealizado.GerarNumero();

				// Acabou o experimento
				if (novoNumero == null) {
					Close();
					return;
				}

				pnGrau.Visible = true;
			}

			tbInput.Text = "";
			pnNumero.Visible = true;
			btnCerteza.Enabled = true;
			btnTalvez.Enabled = true;
			pnCorreto.Visible = false;
			btnCerteza.Visible = false;
			btnTalvez.Visible = false;
			pnInput.Visible = false;

			lblNumero.Font = new Font(lblNumero.Font.Name, tamanhoFonteOriginal, lblNumero.Font.Style);
			lblNumero.Text = novoNumero;

			while (TextRenderer.MeasureText(lblNumero.Text, lblNumero.Font).Width >= pnNumero.Size.Width) {
				var novaFonte = new Font(lblNumero.Font.Name, lblNumero.Font.Size - 1, lblNumero.Font.Style);
				lblNumero.Font = novaFonte;
				tbInput.Font = novaFonte;
			}
			
			lblNumero.Location = new Point {
				Y = (pnNumero.Size.Height - lblNumero.Size.Height) / 2,
				X = (pnNumero.Size.Width - lblNumero.Size.Width) / 2
			};

			await Task.Delay(experimentoUm.TempoApresentacaoEstimulo * 1000);

			if (experimentoAtual == 1) {
				pnNumero.Visible = false;

				if (experimentoUmRealizado.faseAtual != 0) {
					SortearPosicaoBotoes();
					btnCerteza.Visible = true;
					btnTalvez.Visible = true;
				}
				else {
					pnInput.Visible = true;
					tbInput.Focus();
				}
			}
			else {
				SortearPosicaoBotoes();
				pnNumero.Visible = false;
				btnCerteza.Visible = true;
				btnTalvez.Visible = true;
			}
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

				var acertou = sequenciaDigitada == lblNumero.Text;
				var certeza = btnCerteza.Enabled;
				bool novaFase;

				if (experimentoAtual == 1) {
					novaFase = experimentoUmRealizado.RegistrarResposta(acertou, certeza);
				}
				else {
					novaFase = experimentoDoisRealizado.RegistrarResposta(acertou, certeza);

					lblGrau.Text = experimentoDoisRealizado.GrauAtual();
					lblGrau.Location = new Point {
						Y = lblGrau.Location.Y,
						X = (pnGrau.Width - lblGrau.Width) / 2
					};
				}

				tbInput.Enabled = false;
				if (acertou) {
					pnGrau.Visible = false;
					pnCorreto.Visible = true;
					await Task.Delay(experimentoUm.TempoTelaPretaITI * 1000);
				}
				else {
					FadeOut(this, 1);
					await Task.Delay(experimentoUm.TempoTelaPretaITI * 1000);
					FadeIn(this, 1);
				}
				tbInput.Enabled = true;

				if (novaFase) {
					IniciarNovaFase();
				} else {
					IniciarNovoNumero();
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
