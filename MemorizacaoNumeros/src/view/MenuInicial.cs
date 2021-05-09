using MemorizacaoNumeros.src.arquivos;
using MemorizacaoNumeros.src.model;
using MemorizacaoNumeros.src.service;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MemorizacaoNumeros.src.view {
	public partial class MenuInicial : Form {

		private static string PASTA_CACHE = "cache";
		private static string ARQUIVO_ULTIMA_CONFIG = "ultima_config.txt";

		private long idParticipante;
		private long idExperimentador;
		private long idExperimentoUm;
		private long idExperimentoDois;

		public MenuInicial() {
			InitializeComponent();

			cbEscolaridadeParticipante.Items.AddRange(Participante.escolaridades);

			CarregarUltimaConfig();

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

		private void MenuInicial_FormClosing(object sender, FormClosingEventArgs e) {
			SalvarConfigAtual();
		}

		private void SalvarConfigAtual() {
			var configAtual = $"{idParticipante};${idExperimentador};${idExperimentoUm};{idExperimentoDois}";

			var arquivoCache = Ambiente.CriaDiretorioAmbiente(PASTA_CACHE) + $"\\{ARQUIVO_ULTIMA_CONFIG}";
			File.WriteAllText(arquivoCache, configAtual);
		}

		private void CarregarUltimaConfig() {
			if (!File.Exists(Ambiente.GetCaminhoAbsoluto(PASTA_CACHE, ARQUIVO_ULTIMA_CONFIG))) {
				return;
			}

			var configAnterior = Ambiente.LerArquivoRelativo(PASTA_CACHE, ARQUIVO_ULTIMA_CONFIG);
			var ids = configAnterior.First().Split(new char[] { ';' }).Select(id => Convert.ToInt32(id)).ToList();

			var participante = ParticipanteService.GetById(ids[0]);
			var experimentador = ExperimentadorService.GetById(ids[1]);
			var experimentoUm = ExperimentoUmService.GetById(ids[2]);
			var experimentoDois = ExperimentoDoisService.GetById(ids[3]);

			if (participante != null) {
				tbNomeParticipante.Text = participante.Nome;
				tbTelefoneParticipante.Text = participante.Telefone;
				tbEmailParticipante.Text = participante.Email;
				tbEnderecoParticipante.Text = participante.Endereco;
				nudIdadeParticipante.Value = participante.Idade;
				cbEscolaridadeParticipante.SelectedItem = participante.Escolaridade;
				tbCursoParticipante.Text = participante.Curso;
			}

			if (experimentador != null) {
				tbNomeParticipante.Text = experimentador.Nome;
				tbTelefoneParticipante.Text = experimentador.Telefone;
				tbEmailParticipante.Text = experimentador.Email;
			}

			if (experimentoUm != null) {
				 tbInstrucao1.Text = experimentoUm.InstrucaoInicial;
				 nudTelaPretaInicial1.Value = experimentoUm.TempoTelaPretaInicial;
				 nudTelaPretaITI.Value = experimentoUm.TempoTelaPretaITI;
				 nudTempoEstimulo.Value = experimentoUm.TempoApresentacaoEstimulo;
				 nudTamanhoBloco.Value = experimentoUm.TamanhoBlocoTentativas;
				 nudSequenciaInicial.Value = experimentoUm.TamanhoSequenciaInicial;
				 nudAcertosPreTreino.Value = experimentoUm.CriterioAcertoPreTreino;
				 nudTalvezLinhaDeBase.Value = experimentoUm.CriterioTalvezLinhaDeBase;
				 nudBlocosFaseExperimental.Value = experimentoUm.NumeroBlocosFaseExperimental;
				 nudReforcoFaseExperimental.Value = experimentoUm.CriterioReforcoFaseExperimental;
			}

			if (experimentoDois != null) {
				tbInstrucao2.Text = experimentoDois.InstrucaoInicial;
				nudBlocosLinhaDeBase.Value = experimentoDois.QuantidadeBlocosLinhaDeBase;
				nudBlocosCondicao1.Value = experimentoDois.QuantidadeBlocosCondicao1;
				nudTalvezErro1.Value = experimentoDois.PontosTalvezErro1;
				nudTalvezAcerto1.Value = experimentoDois.PontosTalvezAcerto1;
				nudCertezaErro1.Value = experimentoDois.PontosCertezaErro1;
				nudCertezaAcerto1.Value = experimentoDois.PontosCertezaAcerto1;
				nudTalvezErro2.Value = experimentoDois.PontosTalvezErro2;
				nudTalvezAcerto2.Value = experimentoDois.PontosTalvezAcerto2;
				nudCertezaErro2.Value = experimentoDois.PontosCertezaErro2;
				nudCertezaAcerto2.Value = experimentoDois.PontosCertezaAcerto2;
				nudPontosPorGrau.Value = experimentoDois.PontosPorGrau;
			}
		}

		private Participante CriaParticipantePelosCampos() {
			var participante =  new Participante {
				Nome = tbNomeParticipante.Text,
				Telefone = tbTelefoneParticipante.Text,
				Email = tbEmailParticipante.Text,
				Endereco = tbEnderecoParticipante.Text,
				Idade = Convert.ToInt32(nudIdadeParticipante.Value),
				Escolaridade = cbEscolaridadeParticipante.SelectedItem.ToString(),
				Curso = tbCursoParticipante.Text
			};

			ParticipanteService.Salvar(participante);
			idParticipante = participante.Id;

			return participante;
		}

		private Experimentador CriaExperimentadorPelosCampos() {
			var experimentador = new Experimentador {
				Nome = tbNomeExperimentador.Text,
				Telefone = tbTelefoneExperimentador.Text,
				Email = tbEmailExperimentador.Text
			};

			ExperimentadorService.Salvar(experimentador);
			idExperimentador = experimentador.Id;

			return experimentador;
		}

		private ExperimentoUm CriaExperimentoUmPelosCampos() {
			var ExperimentoUm = new ExperimentoUm {
				InstrucaoInicial = tbInstrucao1.Text,
				TempoTelaPretaInicial = Convert.ToInt32(nudTelaPretaInicial1.Value),
				TempoTelaPretaITI = Convert.ToInt32(nudTelaPretaITI.Value),
				TempoApresentacaoEstimulo = Convert.ToInt32(nudTempoEstimulo.Value),
				TamanhoBlocoTentativas = Convert.ToInt32(nudTamanhoBloco.Value),
				TamanhoSequenciaInicial = Convert.ToInt32(nudSequenciaInicial.Value),
				CriterioAcertoPreTreino = Convert.ToInt32(nudAcertosPreTreino.Value),
				CriterioTalvezLinhaDeBase = Convert.ToInt32(nudTalvezLinhaDeBase.Value),
				NumeroBlocosFaseExperimental = Convert.ToInt32(nudBlocosFaseExperimental.Value),
				CriterioReforcoFaseExperimental = Convert.ToInt32(nudReforcoFaseExperimental.Value)
			};

			ExperimentoUmService.Salvar(ExperimentoUm);
			idExperimentoUm = ExperimentoUm.Id;

			return ExperimentoUm;
		}

		private ExperimentoDois CriarExperimentoDoisPelosCampos(ExperimentoUm experimentoUm) {
			var experimentoDois = new ExperimentoDois {
				InstrucaoInicial = tbInstrucao2.Text,
				TempoTelaPretaInicial = experimentoUm.TempoTelaPretaInicial,
				TempoTelaPretaITI = experimentoUm.TempoTelaPretaITI,
				TempoApresentacaoEstimulo = experimentoUm.TempoApresentacaoEstimulo,
				TamanhoBlocoTentativas = experimentoUm.TamanhoBlocoTentativas,
				QuantidadeBlocosLinhaDeBase = Convert.ToInt32(nudBlocosLinhaDeBase.Value),
				QuantidadeBlocosCondicao1 = Convert.ToInt32(nudBlocosCondicao1.Value),
				PontosTalvezErro1 = Convert.ToInt32(nudTalvezErro1.Value),
				PontosTalvezAcerto1 = Convert.ToInt32(nudTalvezAcerto1.Value),
				PontosCertezaErro1 = Convert.ToInt32(nudCertezaErro1.Value),
				PontosCertezaAcerto1 = Convert.ToInt32(nudCertezaAcerto1.Value),
				PontosTalvezErro2 = Convert.ToInt32(nudTalvezErro2.Value),
				PontosTalvezAcerto2 = Convert.ToInt32(nudTalvezAcerto2.Value),
				PontosCertezaErro2 = Convert.ToInt32(nudCertezaErro2.Value),
				PontosCertezaAcerto2 = Convert.ToInt32(nudCertezaAcerto2.Value),
				PontosPorGrau = Convert.ToInt32(nudPontosPorGrau.Value)
			};

			ExperimentoDoisService.Salvar(experimentoDois);
			idExperimentoDois = experimentoDois.Id;

			return experimentoDois;
		}

		private void btnSalvarParticipante_Click(object sender, EventArgs e) {
			var participante = CriaParticipantePelosCampos();
			ParticipanteService.Salvar(participante);
			MessageBox.Show("Participante cadastrado com sucesso!", "Sucesso");
		}

		private void btnSalvarExperimentador_Click(object sender, EventArgs e) {
			var experimentador = CriaExperimentadorPelosCampos();
			ExperimentadorService.Salvar(experimentador);
			MessageBox.Show("Experimentador cadastrado com sucesso!", "Sucesso");
		}

		private void btnSalvarExperimento_Click(object sender, EventArgs e) {
			var experimentoUm = CriaExperimentoUmPelosCampos();
			var experimentoDois = CriarExperimentoDoisPelosCampos(experimentoUm);

			ExperimentoUmService.Salvar(experimentoUm);
			ExperimentoDoisService.Salvar(experimentoDois);

			MessageBox.Show("Experimento cadastrado com sucesso!", "Sucesso");
		}

		private void btnIniciarExperimento_Click(object sender, EventArgs e) {
			var participante = CriaParticipantePelosCampos();
			var experimentador = CriaExperimentadorPelosCampos();
			var experimentoUm = CriaExperimentoUmPelosCampos();
			var experimentoDois = CriarExperimentoDoisPelosCampos(experimentoUm);

			var experimentoUmRealizado = new ExperimentoUmRealizado() {
				ExperimentoUm = experimentoUm
			};

			var experimentoDoisRealizado = new ExperimentoDoisRealizado() {
				ExperimentoDois = experimentoDois
			};

			var experimentoRealizado = new ExperimentoRealizado {
				Participante = participante,
				Experimentador = experimentador,
				ExperimentoUmRealizado = experimentoUmRealizado,
				ExperimentoDoisRealizado = experimentoDoisRealizado
			};

			var telaBackgroud = new TelaMensagem("", false);
			telaBackgroud.BackColor = Color.Black;
			telaBackgroud.Show();

			experimentoRealizado.DateTimeInicio = DateTime.Now;

			new TelaMensagem(experimentoUm.InstrucaoInicial, true).ShowDialog();
			new ExperimentoView(experimentoRealizado).ShowDialog();
		}
	}
}
