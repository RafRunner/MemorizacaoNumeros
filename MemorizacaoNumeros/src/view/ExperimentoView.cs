using MemorizacaoNumeros.src.arquivos;
using MemorizacaoNumeros.src.model;
using MemorizacaoNumeros.src.service;
using MemorizacaoNumeros.src.util;
using System;
using System.Drawing;
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
		private readonly ExperimentoRealizado experimentoRealizado;

		private int experimentoAtual = 1;

		private float tamanhoFonteOriginal;
		private bool tbInputEnabled = false;

		// Variáveis do timer de fade
		private bool fadingIn;
		private Form whatToFade;

		public ExperimentoView(ExperimentoRealizado experimentoRealizado) {
			InitializeComponent();

			Location = new Point(0, 0);
			Size = new Size(width, height);

			var heightRatio = height / 1080.0;
			var widthRatio = width / 1920.0;

			this.experimentoRealizado = experimentoRealizado;

			this.experimentoUmRealizado = experimentoRealizado.ExperimentoUmRealizado;
			this.experimentoUm = this.experimentoUmRealizado.ExperimentoUm;

			this.experimentoDoisRealizado = experimentoRealizado.ExperimentoDoisRealizado;
			this.experimentoDois = this.experimentoDoisRealizado.ExperimentoDois;

			Opacity = 0;
			btnCerteza.Visible = false;
			btnTalvez.Visible = false;
			pnInput.Visible = false;
			tbInput.Text = "";

			ViewUtils.CorrigeEscalaTodosOsFilhos(this, heightRatio, widthRatio);

			tamanhoFonteOriginal = lblNumero.Font.Size;

			experimentoUmRealizado.DateTimeInicio = DateTime.Now;

			IniciarNovaFase();
		}

		private async void IniciarNovaFase() {
			Opacity = 0;
			await Task.Delay(experimentoUm.TempoTelaPretaInicial * 1000);

			if (experimentoAtual == 1 && experimentoUmRealizado.faseAtual == 1) {
				new TelaMensagem(experimentoUm.InstrucaoLinhaDeBase, true).ShowDialog();
			}

			IniciarNovoNumero();
		}

		private async void IniciarNovoNumero() {
			pnNumero.Visible = true;

			btnCerteza.Enabled = true;
			btnTalvez.Enabled = true;

			FadeIn(this, 1);

			string novoNumero;

			if (experimentoAtual == 1) {
				novoNumero = experimentoUmRealizado.GeraNumero();

				// Iniciamos o experimento 2
				if (novoNumero == null) {
					experimentoAtual++;

					experimentoDoisRealizado.SetTamanhoSequencia(experimentoUmRealizado.tamanhoMaximoLinhaDeBase);
					experimentoDoisRealizado.SetTamanhoBlocoTentativas(experimentoUm.TamanhoBlocoTentativas);
					experimentoDoisRealizado.DateTimeInicio = DateTime.Now;

					new TelaMensagem(experimentoDois.InstrucaoInicial, true).ShowDialog();

					IniciarNovaFase();
					return;
				}
			}
			else {
				novoNumero = experimentoDoisRealizado.GerarNumero();

				// Acabou o experimento
				if (novoNumero == null) {
					ExperimentoRealizadoService.Salvar(experimentoRealizado);

					var geradorRelatorio = new GeradorRelatorios(experimentoRealizado);
					geradorRelatorio.GerarRelatorio();

					Close();
					return;
				}

				if (experimentoDoisRealizado.faseAtual > 0) {
					MostrarMensagem(experimentoDoisRealizado.GrauAtual());
				}
			}

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

			RegistrarEvento($"Inciando apresentação de um novo número: {novoNumero}");

			await Task.Delay(experimentoUm.TempoApresentacaoEstimulo * 1000);

			pnNumero.Visible = false;

			if (experimentoAtual == 1 && experimentoUmRealizado.faseAtual == 0) {
				PreparaParaReceberInput();
			}
			else {
				SortearPosicaoBotoes();
				btnCerteza.Visible = true;
				btnTalvez.Visible = true;
				btnInvisivel.Focus();
			}
		}

		private void PreparaParaReceberInput() {
			tbInputEnabled = true;
			pnInput.Visible = true;
			tbInput.Focus();
		}

		private void btnCerteza_Click(object sender, EventArgs e) {
			if (!btnTalvez.Enabled) {
				tbInput.Focus();
				return;
			}

			RegistrarEvento("Participante selecionou 'Certeza'");

			btnTalvez.Enabled = false;
			PreparaParaReceberInput();
		}

		private void btnTalvez_Click(object sender, EventArgs e) {
			if (!btnCerteza.Enabled) {
				tbInput.Focus();
				return;
			}
			RegistrarEvento("Participante selecionou 'Talvez'");

			btnCerteza.Enabled = false;
			PreparaParaReceberInput();
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
			// Deixandoas teclas de função serem lidadas pelo sistema
			if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F9) {
				return;
			}

			// Enable à mão
			if (!tbInputEnabled) {
				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}

			// Todo input que não for enter ou número é ignorado. Isso inclui navegação e correção da sequência
			if (e.KeyData != Keys.Enter && !((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))) {
				e.Handled = true;
				e.SuppressKeyPress = true;
				return;
			}

			if (e.KeyData == Keys.Enter) {
				// Enter foi pressionado
				var sequenciaDigitada = tbInput.Text;

				if (string.IsNullOrWhiteSpace(sequenciaDigitada)) return;

				ApagarResposta();

				tbInputEnabled = false;
				e.Handled = true;
				e.SuppressKeyPress = true;

				var sequenciaModelo = lblNumero.Text;
				var acertou = sequenciaDigitada == sequenciaModelo;
				var certeza = btnCerteza.Enabled;
				bool novaFase;

				if (experimentoAtual == 1) {
					novaFase = experimentoUmRealizado.RegistrarResposta(acertou, certeza, sequenciaModelo, sequenciaDigitada);

					if (acertou || !certeza) {
						await MostrarMensagemTempo("Correto!", experimentoUm.TempoTelaPretaITI);
					}
					else {
						FadeOut(this, 1);
						await Task.Delay(experimentoUm.TempoTelaPretaITI * 1000);
					}
				}
				else {
					var faseAtual = experimentoDoisRealizado.faseAtual;
					novaFase = experimentoDoisRealizado.RegistrarResposta(acertou, certeza, sequenciaModelo, sequenciaDigitada);

					if (faseAtual > 0) {
						await MostrarMensagemTempo($"+{experimentoDoisRealizado.ultimosPontosGanhos} pontos", experimentoUm.TempoTelaPretaITI);
					}
					else if (acertou) {
						await MostrarMensagemTempo("Correto!", experimentoUm.TempoTelaPretaITI);
					}

					if (!acertou && certeza) {
						FadeOut(this, 1);
						await Task.Delay(experimentoUm.TempoTelaPretaITI * 1000);
					}
				}
				
				if (novaFase) {
					IniciarNovaFase();
				} else {
					IniciarNovoNumero();
				}
			}
		}

		private async void ApagarResposta() {
			btnInvisivel.Focus();
			await Task.Delay(1000);
			tbInput.Text = "";
			pnInput.Visible = false;
			btnCerteza.Visible = false;
			btnTalvez.Visible = false;
		}

		private void tbInput_TextChanged(object sender, EventArgs e) {
			if (tbInput.Text.Length == 0) return;
			RegistrarEvento($"{tbInput.Text[tbInput.Text.Length - 1]} digitado");
		}

		private void RegistrarEvento(string evento) {
			if (experimentoAtual == 1) {
				experimentoUmRealizado.RegistrarEvento(evento);
			}
			else {
				experimentoDoisRealizado.RegistrarEvento(evento);
			}
		}

		private void MostrarMensagem(string mensagem) {
			pnMensagem.Visible = true;

			lblMensagem.Text = mensagem;
			lblMensagem.Location = new Point {
				Y = lblMensagem.Location.Y,
				X = (pnMensagem.Width - lblMensagem.Width) / 2
			};
		}

		private void EsconderMensagem() {
			pnMensagem.Visible = false;
		}

		private async Task MostrarMensagemTempo(string mensagem, int tempo) {
			MostrarMensagem(mensagem);
			await Task.Delay(tempo * 1000);
			EsconderMensagem();
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
