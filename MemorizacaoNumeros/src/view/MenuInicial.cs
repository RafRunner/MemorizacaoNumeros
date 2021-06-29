using MemorizacaoNumeros.src.arquivos;
using MemorizacaoNumeros.src.model;
using MemorizacaoNumeros.src.service;
using System;
using System.Diagnostics;
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
			cbEscolaridadeParticipante.SelectedIndex = 0;

			CarregarUltimaConfig();
		}

		private void MenuInicial_FormClosing(object sender, FormClosingEventArgs e) {
			SalvarConfigAtual();
		}

		private void SalvarConfigAtual() {
			var configAtual = $"{idParticipante};{idExperimentador};{idExperimentoUm};{idExperimentoDois}";

			var arquivoCache = Ambiente.CriaDiretorioAmbiente(PASTA_CACHE) + $@"\{ARQUIVO_ULTIMA_CONFIG}";
			File.WriteAllText(arquivoCache, configAtual);
		}

		private void CarregarUltimaConfig() {
			if (!File.Exists(Ambiente.GetCaminhoAbsoluto(PASTA_CACHE, ARQUIVO_ULTIMA_CONFIG))) {
				return;
			}

			var configAnterior = Ambiente.LerArquivoRelativo(PASTA_CACHE, ARQUIVO_ULTIMA_CONFIG);
			var ids = configAnterior.First().Split(new char[] { ';' }).Select(id => Convert.ToInt32(id)).ToList();

			CarregaInfoParticipante(ids[0]);
			CarregaInfoExperimentador(ids[1]);
			CarregaInfoExperimentoUm(ids[2]);
			CarregaInfoExperimentoDois(ids[3]);
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
			if (participante.Id != 0) {
				idParticipante = participante.Id;
			}
			else {
				participante.Id = idParticipante;
			}

			return participante;
		}

		private Experimentador CriaExperimentadorPelosCampos() {
			var experimentador = new Experimentador {
				Nome = tbNomeExperimentador.Text,
				Telefone = tbTelefoneExperimentador.Text,
				Email = tbEmailExperimentador.Text
			};

			ExperimentadorService.Salvar(experimentador);
			if (experimentador.Id != 0) {
				idExperimentador = experimentador.Id;
			}
			else {
				experimentador.Id = idExperimentador;
			}

			return experimentador;
		}

		private ExperimentoUm CriaExperimentoUmPelosCampos() {
			var experimentoUm = new ExperimentoUm {
				InstrucaoInicial = tbInstrucao1.Text,
				InstrucaoLinhaDeBase = tbInstrucaoLinhaDeBase.Text,
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

			ExperimentoUmService.Salvar(experimentoUm);
			if (experimentoUm.Id != 0) {
				idExperimentoUm = experimentoUm.Id;
			}
			else {
				experimentoUm.Id = idExperimentoUm;
			}

			return experimentoUm;
		}

		private ExperimentoDois CriarExperimentoDoisPelosCampos() {
			var experimentoDois = new ExperimentoDois {
				InstrucaoInicial = tbInstrucao2.Text,
				QuantidadeBlocosLinhaDeBase = Convert.ToInt32(nudBlocosLinhaDeBase.Value),
				QuantidadeBlocosCondicao1 = Convert.ToInt32(nudBlocosCondicao1.Value),
				PontosTalvezErro1 = Convert.ToInt32(nudTalvezErro1.Value),
				PontosTalvezAcerto1 = Convert.ToInt32(nudTalvezAcerto1.Value),
				PontosCertezaErro1 = Convert.ToInt32(nudCertezaErro1.Value),
				PontosCertezaAcerto1 = Convert.ToInt32(nudCertezaAcerto1.Value),
				QuantidadeBlocosCondicao2 = Convert.ToInt32(nudBlocosCondicao2.Value),
				PontosTalvezErro2 = Convert.ToInt32(nudTalvezErro2.Value),
				PontosTalvezAcerto2 = Convert.ToInt32(nudTalvezAcerto2.Value),
				PontosCertezaErro2 = Convert.ToInt32(nudCertezaErro2.Value),
				PontosCertezaAcerto2 = Convert.ToInt32(nudCertezaAcerto2.Value),
				PontosPorGrau = Convert.ToInt32(nudPontosPorGrau.Value)
			};

			ExperimentoDoisService.Salvar(experimentoDois);
			if (experimentoDois.Id != 0) {
				idExperimentoDois = experimentoDois.Id;
			}
			else {
				experimentoDois.Id = idExperimentoDois;
			}

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
			var experimentoDois = CriarExperimentoDoisPelosCampos();

			ExperimentoUmService.Salvar(experimentoUm);
			ExperimentoDoisService.Salvar(experimentoDois);

			MessageBox.Show("Experimento cadastrado com sucesso!", "Sucesso");
		}

		private void btnIniciarExperimento_Click(object sender, EventArgs e) {
			var participante = CriaParticipantePelosCampos();
			var experimentador = CriaExperimentadorPelosCampos();
			var experimentoUm = CriaExperimentoUmPelosCampos();
			var experimentoDois = CriarExperimentoDoisPelosCampos();

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
				ExperimentoDoisRealizado = experimentoDoisRealizado,
				DateTimeInicio = DateTime.Now
			};

			var telaBackgroud = new TelaMensagem("");
			telaBackgroud.BackColor = Color.Black;
			telaBackgroud.Show();

			new TelaMensagem(experimentoUm.InstrucaoInicial, true).ShowDialog();
			new ExperimentoView(experimentoRealizado).ShowDialog();

			var grauFinal = experimentoDoisRealizado.GrauAtual();

			new TelaMensagem($"Fim do Experimento! O seu grau final foi {grauFinal}!\nPor favor, chamar o experimentador.").ShowDialog();
			telaBackgroud.Close();
		}

		private void btnVerParticipantes_Click(object sender, EventArgs e) {
			new GridCrud(
				"Participante",
				ParticipanteService.GetAllAsObj,
				Participante.odemColunasGrid,
				AbstractService.FilterDataTable,
				null,
				ParticipanteService.DeletarPorId,
				CarregaInfoParticipante
			);
		}

		private void CarregaInfoParticipante(long idParticipante) {
			var participante = ParticipanteService.GetById(idParticipante);

			if (participante != null) {
				this.idParticipante = idParticipante;

				tbNomeParticipante.Text = participante.Nome;
				tbTelefoneParticipante.Text = participante.Telefone;
				tbEmailParticipante.Text = participante.Email;
				tbEnderecoParticipante.Text = participante.Endereco;
				nudIdadeParticipante.Value = participante.Idade;
				cbEscolaridadeParticipante.SelectedItem = participante.Escolaridade;
				tbCursoParticipante.Text = participante.Curso;
			}
		}

		private void btnVerExperimentadores_Click(object sender, EventArgs e) {
			new GridCrud(
				"Experimentador",
				ExperimentadorService.GetAllAsObj,
				Experimentador.odemColunasGrid,
				AbstractService.FilterDataTable,
				null,
				ExperimentadorService.DeletarPorId,
				CarregaInfoExperimentador
			);
		}

		private void CarregaInfoExperimentador(long idExperimentador) {
			var experimentador = ExperimentadorService.GetById(idExperimentador);

			if (experimentador != null) {
				this.idExperimentador = idExperimentador;

				tbNomeExperimentador.Text = experimentador.Nome;
				tbTelefoneExperimentador.Text = experimentador.Telefone;
				tbEmailExperimentador.Text = experimentador.Email;
			}
		}

		private void btnVerExperimentos1_Click(object sender, EventArgs e) {
			new GridCrud(
				"Experimento Um",
				ExperimentoUmService.GetAllAsObj,
				ExperimentoUm.ordemColunas,
				AbstractService.FilterDataTable,
				null,
				ExperimentoUmService.DeletarPorId,
				CarregaInfoExperimentoUm
			);
		}

		private void CarregaInfoExperimentoUm(long idExperimento) {
			var experimentoUm = ExperimentoUmService.GetById(idExperimento);

			if (experimentoUm != null) {
				idExperimentoUm = idExperimento;

				tbInstrucao1.Text = experimentoUm.InstrucaoInicial;
				tbInstrucaoLinhaDeBase.Text = experimentoUm.InstrucaoLinhaDeBase;
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
		}

		private void btnVerExperimentos2_Click(object sender, EventArgs e) {
			new GridCrud(
				"Experimento Dois",
				ExperimentoDoisService.GetAllAsObj,
				ExperimentoDois.ordemColunas,
				AbstractService.FilterDataTable,
				null,
				ExperimentoDoisService.DeletarPorId,
				CarregaInfoExperimentoDois
			);
		}

		private void CarregaInfoExperimentoDois(long idExperimento) {
			var experimentoDois = ExperimentoDoisService.GetById(idExperimento);

			if (experimentoDois != null) {
				idExperimentoDois = idExperimento;

				tbInstrucao2.Text = experimentoDois.InstrucaoInicial;
				nudBlocosLinhaDeBase.Value = experimentoDois.QuantidadeBlocosLinhaDeBase;
				nudBlocosCondicao1.Value = experimentoDois.QuantidadeBlocosCondicao1;
				nudTalvezErro1.Value = experimentoDois.PontosTalvezErro1;
				nudTalvezAcerto1.Value = experimentoDois.PontosTalvezAcerto1;
				nudCertezaErro1.Value = experimentoDois.PontosCertezaErro1;
				nudCertezaAcerto1.Value = experimentoDois.PontosCertezaAcerto1;
				nudBlocosCondicao2.Value = experimentoDois.QuantidadeBlocosCondicao2;
				nudTalvezErro2.Value = experimentoDois.PontosTalvezErro2;
				nudTalvezAcerto2.Value = experimentoDois.PontosTalvezAcerto2;
				nudCertezaErro2.Value = experimentoDois.PontosCertezaErro2;
				nudCertezaAcerto2.Value = experimentoDois.PontosCertezaAcerto2;
				nudPontosPorGrau.Value = experimentoDois.PontosPorGrau;
			}
		}

		private void btnAbrirPastaRelatorios_Click(object sender, EventArgs e) {
			var pastaRelatorios = Ambiente.CriaDiretorioAmbiente(GeradorRelatorios.nomePasta);
			Process.Start(pastaRelatorios.FullName);
		}

		private void GeraRelatorioNovamente(long idExperimentoRealizado) {
			var experimentoRealizado = ExperimentoRealizadoService.GetById(idExperimentoRealizado);
			var geradorRelatorio = new GeradorRelatorios(experimentoRealizado);

			if (geradorRelatorio.ExperimentoJaTemRelatorioNaPasta()) {
				MessageBox.Show("Esse relatório já está na pasta de relatórios!", "Aviso");
				return;
			}

			geradorRelatorio.GerarRelatorio();

			MessageBox.Show("Relatório gerado novamente com sucesso!", "Sucesso");
		}

		private void btnGerarRelatoriosAntigos_Click(object sender, EventArgs e) {
			new GridCrud(
				"Experimentos Realizados",
				ExperimentoRealizadoService.GetAllAsObj,
				ExperimentoRealizado.ordemColunas,
				AbstractService.FilterDataTable,
				null,
				ExperimentoRealizadoService.DeletarPorId,
				GeraRelatorioNovamente
			);
		}

		private void btnMaisSobreOSoftware_Click(object sender, EventArgs e) {
			MessageBox.Show("Desenvolvido por: Rafael Nunes Santna\nCódigo fonte disponível em: https://github.com/RafRunner/MemorizacaoNumeros", "Informações");
		}
	}
}
