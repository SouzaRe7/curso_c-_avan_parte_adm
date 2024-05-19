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
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposOpconais())
            {
                try
                {
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
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
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
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
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposOpconais();
        }

        private void grdOpcionais_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        #region Funções

        private Dictionary<TextBox, string> DicionarioTextBoxOpconais(Util.CamposObg obg)
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");

            if (obg == Util.CamposObg.Obrigatorio)
            {
                dicionario.Add(txtCodigo, "Codigo");
            }
            return dicionario;
        }

        private bool VerificarCamposOpconais() => Util.VerificarCamposUtil(DicionarioTextBoxOpconais(Util.CamposObg.NaoObrigatorio));

        private void LimparCamposOpconais() => Util.LimparCamposUtil(DicionarioTextBoxOpconais(Util.CamposObg.Obrigatorio));

        #endregion
    }
}
