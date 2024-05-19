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
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
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
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
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
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposCor();
        }

        private void grdCores_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        #region Funções

        private Dictionary<TextBox, string> DicionarioTextBoxCor(Util.CamposObg obg)
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");

            if (obg == Util.CamposObg.Obrigatorio)
            {
                dicionario.Add(txtCodigo, "Codigo");
            }

            return dicionario;
        }

        private bool VerificarCamposCor() => Util.VerificarCamposUtil(DicionarioTextBoxCor(Util.CamposObg.NaoObrigatorio));

        private void LimparCamposCor()=> Util.LimparCamposUtil(DicionarioTextBoxCor(Util.CamposObg.Obrigatorio));

        #endregion
    }
}
