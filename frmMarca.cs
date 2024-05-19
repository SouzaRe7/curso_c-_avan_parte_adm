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
    public partial class frmMarca : Form
    {
        public frmMarca()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
        }

        #region Eventos
        private void frmMarca_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdMarcas);
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposMarca())
            {
                try
                {

                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposMarca();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCamposMarca();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposMarca())
            {
                try
                {
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposMarca();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposMarca();
        }

        private void grdMarcas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        #region Funções
        private Dictionary<TextBox, string> DicionarioTextBoxMarca(Util.CamposObg obg)
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");

            if (obg == Util.CamposObg.Obrigatorio)
            {
                dicionario.Add(txtCodigo, "Codigo");
            }
            return dicionario;
        }

        private bool VerificarCamposMarca() => Util.VerificarCamposUtil(DicionarioTextBoxMarca(Util.CamposObg.NaoObrigatorio));

        private void LimparCamposMarca() => Util.LimparCamposUtil(DicionarioTextBoxMarca(Util.CamposObg.Obrigatorio));

        #endregion
    }
}
