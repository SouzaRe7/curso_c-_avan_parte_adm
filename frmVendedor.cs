using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VendasAdm
{
    public partial class frmVendedor : Form
    {
        public frmVendedor()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
        }
        #region Eventos
        private void frmVendedor_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdVendedores);
            Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposVendedor())
            {
                try
                {

                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposVendedor();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCamposVendedor();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposVendedor())
            {
                try
                {
                    Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposVendedor();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
            LimparCamposVendedor();
        }
        #endregion

        #region Funções

        private Dictionary<TextBox, string> DicionarioTextBoxVendedor()
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");
            dicionario.Add(txtEmail, "E-mail");
            return dicionario;
        }

        private Dictionary<MaskedTextBox, string> DicionarioMaskedTextBoxVendedor()
        {
            Dictionary<MaskedTextBox, string> dicionario = new Dictionary<MaskedTextBox, string>();
            dicionario.Add(mkCpf, "CPF");
            dicionario.Add(mkTelefone, "Telefone");
            return dicionario;
        }

        private Dictionary<CheckBox, string> DicionarioCheckBoxVendedor()
        {
            Dictionary<CheckBox, string> dicionario = new Dictionary<CheckBox, string>();
            dicionario.Add(chkStatus, "Status");
            return dicionario;
        }

        private bool VerificarCamposVendedor() => Util.VerificarCamposUtil(DicionarioTextBoxVendedor(), DicionarioMaskedTextBoxVendedor(), new Dictionary<ComboBox, string>(), DicionarioCheckBoxVendedor());

        private void LimparCamposVendedor() => Util.LimparCamposUtil(DicionarioTextBoxVendedor(), DicionarioMaskedTextBoxVendedor(), new Dictionary<ComboBox, string>(), DicionarioCheckBoxVendedor());

        #endregion
    }
}
