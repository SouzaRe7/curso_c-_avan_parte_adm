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
    public partial class frmModelo : Form
    {
        public frmModelo()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
        }
        #region Eventos
        private void frmModelo_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdModelos);
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposModelo())
            {
                try
                {

                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposModelo();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCamposModelo();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposModelo())
            {
                try
                {
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposModelo();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposModelo();
        }

        private void grdModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        #region Funções

        private Dictionary<TextBox, string> DicionarioTextBoxModelo(Util.CamposObg obg)
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");

            if (obg == Util.CamposObg.Obrigatorio)
            {
                dicionario.Add(txtCodigo, "Codigo");
            }
            return dicionario;
        }

        private Dictionary<ComboBox, string> DicionarioComboBoxModelo()
        {
            Dictionary<ComboBox, string> dicionario = new Dictionary<ComboBox, string>();
            dicionario.Add(cbMarca, "Marca");
            return dicionario;
        }

        private bool VerificarCamposModelo() => Util.VerificarCamposUtil(DicionarioTextBoxModelo(Util.CamposObg.NaoObrigatorio), null, DicionarioComboBoxModelo());

        private void LimparCamposModelo() => Util.LimparCamposUtil(DicionarioTextBoxModelo(Util.CamposObg.Obrigatorio), null, DicionarioComboBoxModelo());

        #endregion
    }
}
