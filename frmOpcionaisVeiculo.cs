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
    public partial class frmOpcionaisVeiculo : Form
    {
        public frmOpcionaisVeiculo()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
        }
        #region Eventos
        private void frmOpcionaisVeiculo_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdOpcionais);
            Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposOpconais())
            {
                try
                {
                    Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposOpconais();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCamposOpconais();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposOpconais())
            {
                try
                {
                    Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposOpconais();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
            LimparCamposOpconais();
        }
        #endregion

        #region Funções

        private Dictionary<TextBox, string> DicionarioTextBoxOpconais()
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");
            return dicionario;
        }

        private bool VerificarCamposOpconais() => Util.VerificarCamposUtil(DicionarioTextBoxOpconais(), new Dictionary<MaskedTextBox, string>(), new Dictionary<ComboBox, string>(), new Dictionary<CheckBox, string>());

        private void LimparCamposOpconais() => Util.LimparCamposUtil(DicionarioTextBoxOpconais(), new Dictionary<MaskedTextBox, string>(), new Dictionary<ComboBox, string>(), new Dictionary<CheckBox, string>());

        #endregion
    }
}
