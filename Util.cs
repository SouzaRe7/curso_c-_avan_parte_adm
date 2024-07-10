using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VendasAdm
{
    public static class Util
    {
        public static int CodigoUsuarioLogado = 1;

        public static int CodigoEmpresaLogado = 1;

        public enum TipoUsuario
        {
            Admin,
            Vendedor
        }

        public static DateTime DataAtualUtil()
        {
            return DateTime.Now;
        }

        public static void ConfigureFormBorderStyle(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public enum CamposObg
        {
            Obrigatorio,
            NaoObrigatorio
        }

        public enum AcaoBotao
        {
            Novo,
            Editar
        }

        public static void ControleTela(Enum tipo, Button btnCadastrar, Button btnAlterar, Button btnExcluir)
        {
            switch (tipo)
            {
                case AcaoBotao.Novo:
                    btnCadastrar.Enabled = true;
                    btnAlterar.Enabled = false;
                    btnExcluir.Enabled = false;
                    break;
                case AcaoBotao.Editar:
                    btnCadastrar.Enabled = false;
                    btnAlterar.Enabled = true;
                    btnExcluir.Enabled = true;
                    break;
            }
        }

        public static void ConfiguraDataGridViewUtil(DataGridView dataGridView)
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true;
            dataGridView.MultiSelect = false;
        }

        public static void CarregarListaUtil<T>(DataGridView dataGridView, List<T> lista)
        {
            //dataGridView.DataSource = null;
            dataGridView.DataSource = lista;
        }

        public static void AdicionarListaUtil<T>(List<T> lista, T novoItem)
        {
            lista.Add(novoItem);
        }

        public static void CarregarRemoverCabecalhoLista(DataGridView dataGridView, List<string> lista)
        {

            foreach (var item in lista)
            {
                dataGridView.Columns[item].Visible = false;
            }
        }

        public static void CarregarAlterarCabecalhoLista(DataGridView dataGridView, Dictionary<string, string> dicionario)
        {
            foreach (KeyValuePair<string, string> par in dicionario)
            {
                dataGridView.Columns[par.Key].HeaderText = par.Value;
            }
        }

        public static void MsgErroUtil()
        {
            MessageBox.Show("Ocorreu um erro, tente mais tarde!");
        }

        public static void MsgSucessoUtil()
        {
            MessageBox.Show("Ação realizada com sucesso!", "Sucesso", MessageBoxButtons.OK);
        }

        public static bool MsgExcluirItemUtil(string item)
        {
            bool ret = false;

            if (MessageBox.Show("Deseja excluir o item: '" + item + "'?", "Confirmação da exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ret = true;
            }

            return ret;
        }

        public static void AplicarMascaraCpfUtil(DataGridView dataGridView, string nomeCampo)
        {
            // Percorre as linhas do grid para aplicar a máscara ao CPF
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[nomeCampo].Value != null)
                {
                    string cpf = row.Cells[nomeCampo].Value.ToString();
                    string cpfFormatado = FormatarCpfUtil(cpf);
                    row.Cells[nomeCampo].Value = cpfFormatado;
                }
            }
        }

        public static string FormatarCpfUtil(string cpf)
        {
            // Verifica se o CPF tem a quantidade de caracteres esperada
            if (cpf.Length == 11)
            {
                // Formata o CPF: XXX.XXX.XXX-XX
                return string.Format("{0}.{1}.{2}-{3}",
                    cpf.Substring(0, 3),
                    cpf.Substring(3, 3),
                    cpf.Substring(6, 3),
                    cpf.Substring(9, 2));
            }
            else
            {
                // Se o CPF não tiver o formato esperado, retorna o próprio CPF
                return cpf;
            }
        }

        public static void AplicarMascaraTelefoneCelularUtil(DataGridView dataGridView, string nomeCampo)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[nomeCampo].Value != null)
                {
                    string celular = row.Cells[nomeCampo].Value.ToString();
                    string celularFormatado = FormatarTelefoneCelularUtil(celular);
                    row.Cells[nomeCampo].Value = celularFormatado;
                }
            }
        }

        public static string FormatarTelefoneCelularUtil(string celular)
        {
            if (celular.Length == 11) // Verifica se o número tem a quantidade de dígitos esperada para celular
            {
                // Formata o celular: (XX) XXXXX-XXXX
                return string.Format("({0}) {1}-{2}",
                    celular.Substring(0, 2),
                    celular.Substring(2, 5),
                    celular.Substring(7, 4));
            }
            else
            {
                // Se o formato não for esperado, retorna o próprio número
                return celular;
            }
        }

        public static void AplicarMascaraTelefoneFixoUtil(DataGridView dataGridView, string nomeCampo)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[nomeCampo].Value != null)
                {
                    string telefoneFixo = row.Cells[nomeCampo].Value.ToString();
                    string telefoneFixoFormatado = FormatarTelefoneFixoUtil(telefoneFixo);
                    row.Cells[nomeCampo].Value = telefoneFixoFormatado;
                }
            }
        }

        public static string FormatarTelefoneFixoUtil(string telefone)
        {
            if (telefone.Length == 10) // Verifica se o número tem a quantidade de dígitos esperada para telefone fixo
            {
                // Formata o telefone fixo: (XX) XXXX-XXXX
                return string.Format("({0}) {1}-{2}",
                    telefone.Substring(0, 2),
                    telefone.Substring(2, 4),
                    telefone.Substring(6, 4));
            }
            else
            {
                // Se o formato não for esperado, retorna o próprio número
                return telefone;
            }
        }

        public static bool VerificarCamposUtil(Dictionary<TextBox, string> dicionarioTexBox = null, Dictionary<MaskedTextBox, string> dicionarioMaskedTextBox = null, Dictionary<ComboBox, string> dicionarioComboBox = null, Dictionary<CheckBox, string> dicionarioCheckBox = null)
        {
            bool ret = true;
            string campos = "";

            // Verificar TextBox vazias
            if (dicionarioTexBox != null)
            {
                if (dicionarioTexBox.Count > 0)
                {
                    foreach (KeyValuePair<TextBox, string> par in dicionarioTexBox)
                    {
                        if (par.Key.Text.Trim() == String.Empty)
                        {
                            campos += "\n- " + par.Value;
                            ret = false;
                        }
                    }
                }
            }

            // Verificar MaskedTextBox vazias
            if (dicionarioMaskedTextBox != null)
            {
                if (dicionarioMaskedTextBox.Count > 0)
                {
                    foreach (KeyValuePair<MaskedTextBox, string> par in dicionarioMaskedTextBox)
                    {
                        if (par.Key.Text.Trim() == String.Empty)
                        {
                            campos += "\n- " + par.Value;
                            ret = false;
                        }
                    }
                }
            }

            // Verificar ComboBox vazias
            if (dicionarioComboBox != null)
            {
                if (dicionarioComboBox.Count > 0)
                {
                    foreach (KeyValuePair<ComboBox, string> par in dicionarioComboBox)
                    {
                        if (par.Key.SelectedItem == null || par.Key.SelectedItem.ToString().Trim() == String.Empty)
                        {
                            campos += "\n- " + par.Value;
                            ret = false;
                        }
                    }
                }
            }

            // Verificar CheckBox não marcados
            if (dicionarioCheckBox != null)
            {
                if (dicionarioCheckBox.Count > 0)
                {
                    foreach (KeyValuePair<CheckBox, string> par in dicionarioCheckBox)
                    {
                        if (!par.Key.Checked)
                        {
                            campos += "\n- " + par.Value;
                            ret = false;
                        }
                    }
                }
            }

            if (!ret)
            {
                MessageBox.Show("Preencher o(s) campo(s):" + campos, "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        public static void LimparCamposUtil(Dictionary<TextBox, string> dicionarioTexBox = null, Dictionary<MaskedTextBox, string> dicionarioMaskedTextBox = null, Dictionary<ComboBox, string> dicionarioComboBox = null, Dictionary<CheckBox, string> dicionarioCheckBox = null)
        {
            // Limpa o(s) TextBox
            if (dicionarioTexBox != null)
            {
                if (dicionarioTexBox.Count > 0)
                {
                    foreach (KeyValuePair<TextBox, string> par in dicionarioTexBox)
                    {
                        par.Key.Clear();
                    }
                }
            }

            // Limpa o(s) MaskedTextBox
            if (dicionarioMaskedTextBox != null)
            {
                if (dicionarioMaskedTextBox.Count > 0)
                {
                    foreach (KeyValuePair<MaskedTextBox, string> par in dicionarioMaskedTextBox)
                    {
                        par.Key.Clear();
                    }
                }
            }

            // Limpa o(s) ComboBox
            if (dicionarioComboBox != null)
            {
                if (dicionarioComboBox.Count > 0)
                {
                    foreach (KeyValuePair<ComboBox, string> par in dicionarioComboBox)
                    {
                        par.Key.SelectedIndex = -1;
                    }
                }
            }

            // Limpa o(s) CheckBox
            if (dicionarioCheckBox != null)
            {
                if (dicionarioCheckBox.Count > 0)
                {
                    foreach (KeyValuePair<CheckBox, string> par in dicionarioCheckBox)
                    {
                        par.Key.Checked = false;
                    }
                }
            }
        }
    }
}
