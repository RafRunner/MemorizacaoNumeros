using MemorizacaoNumeros.src.util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pactolo {
    public partial class GridCrud : Form {

        private List<object> tabelaCompleta;

        private readonly string nomeRegistros;
        private readonly Func<List<object>> funcaoCarregaDados;
        private readonly Func<List<object>, string, List<object>> funcaoFiltro;
        private readonly Action<long> funcaoEditar;
        private readonly Action<long> funcaoDeletar;
        private readonly Action<long> funcaoSelecionar;

        private static readonly string colunaOculta = "Id";

		public GridCrud(
            string nomeRegistros,
            Func<List<object>> funcaoCarregaDados, 
            List<string> ordemColunas,
            Func<List<object>, string, List<object>> funcaoFiltro, 
            Action<long> funcaoEditar, 
            Action<long> funcaoDeletar,
            Action<long> funcaoSelecionar) {

            InitializeComponent();

            this.nomeRegistros = nomeRegistros;
            this.tabelaCompleta = funcaoCarregaDados();

            if (tabelaCompleta.Count == 0) {
                MessageBox.Show($"Não existe nenhum registro de {nomeRegistros} no momento!", "Advertência");
                Close();
                return;
            }

            this.funcaoCarregaDados = funcaoCarregaDados;
            this.funcaoFiltro = funcaoFiltro;
            this.funcaoEditar = funcaoEditar;
            this.funcaoDeletar = funcaoDeletar;
            this.funcaoSelecionar = funcaoSelecionar;

            Text = "Gerenciador de " + nomeRegistros;

            if (funcaoEditar == null) {
                buttonEditar.Visible = false;
            }

            dataGrid.DataSource = tabelaCompleta;
            dataGrid.Columns[colunaOculta].Visible = false;
            for (int i = 0; i < ordemColunas.Count; i++) {
                dataGrid.Columns[ordemColunas[i]].DisplayIndex = i;
                dataGrid.Columns[ordemColunas[i]].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            ShowDialog();
        }

        private bool VerifiqueQuantidadeColunasSelecionadasEAvise() {
            if (dataGrid.SelectedRows.Count == 0) {
                MessageBox.Show($"Por favor, selecione pelo menos um(a) {nomeRegistros} (selecione toda a linha clicanco na primeira coluna (a vazia)) para editar!", "Atenção");
                return false;
            }
            else {
                return true;
            }
        }

        private void ButtonEditar_Click(object sender, EventArgs e) {
            if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
                return;
            }

            funcaoEditar.Invoke(ViewUtils.GetIdColunaSelecionada(dataGrid););
            tabelaCompleta = funcaoCarregaDados();
            dataGrid.DataSource = tabelaCompleta;
        }

        private void ButtonDeletar_Click(object sender, EventArgs e) {
            if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
                return;
            }

            funcaoDeletar.Invoke(ViewUtils.GetIdColunaSelecionada(dataGrid););
            tabelaCompleta = funcaoCarregaDados();
            dataGrid.DataSource = tabelaCompleta;
            MessageBox.Show($"{nomeRegistros} deletado com sucesso!", "Sucesso");
        }

        private void ButtonSelecionar_Click(object sender, EventArgs e) {
            if (!VerifiqueQuantidadeColunasSelecionadasEAvise()) {
                return;
            }
            funcaoSelecionar.Invoke(ViewUtils.GetIdColunaSelecionada(dataGrid););
            Close();
        }

        private void TextBoxFiltro_TextChanged(object sender, EventArgs e) {
            string textoDeBusca = textBoxFiltro.Text;
            if (string.IsNullOrWhiteSpace(textoDeBusca)) {
                dataGrid.DataSource = tabelaCompleta;
                return;
            }

            if (textoDeBusca.Length < 3) {
                return;
            }

            dataGrid.DataSource = funcaoFiltro.Invoke(tabelaCompleta, textoDeBusca);
        }

		private void textBoxFiltro_KeyDown(object sender, KeyEventArgs e) {
            var textoDeBusca = textBoxFiltro.Text;
            if (string.IsNullOrWhiteSpace(textoDeBusca)) {
                dataGrid.DataSource = tabelaCompleta;
                return;
            }

            if (textoDeBusca.Length < 3 && e.KeyCode != Keys.Enter) {
                return;
            }

            dataGrid.DataSource = funcaoFiltro.Invoke(tabelaCompleta, textoDeBusca);
		}
	}
}
