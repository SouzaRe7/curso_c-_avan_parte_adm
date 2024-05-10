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
    public partial class frmCor : Form
    {
        public frmCor()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
        }

        #region Eventos
        private void frmCor_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdCores);
            Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposCor())
            {
                try
                {
                    
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposCor();
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposCor())
            {
                try
                {
                    Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposCor();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCamposCor();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
            LimparCamposCor();
        }
        #endregion

        #region Funções

        private Dictionary<TextBox, string> DicionarioTextBoxCor()
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");
            return dicionario;
        }

        private bool VerificarCamposCor() => Util.VerificarCamposUtil(DicionarioTextBoxCor(), new Dictionary<MaskedTextBox, string>(), new Dictionary<ComboBox, string>(), new Dictionary<CheckBox, string>());

        private void LimparCamposCor()=> Util.LimparCamposUtil(DicionarioTextBoxCor(), new Dictionary<MaskedTextBox, string>(), new Dictionary<ComboBox, string>(), new Dictionary<CheckBox, string>());
        
        #endregion
    }
}
