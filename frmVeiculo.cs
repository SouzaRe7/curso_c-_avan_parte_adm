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
    public partial class frmVeiculo : Form
    {
        public frmVeiculo()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
        }
        #region Eventos
        private void frmVeiculo_Load(object sender, EventArgs e)
        {
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposVeiculo())
            {
                try
                {
                    
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposVeiculo();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparCamposVeiculo();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposVeiculo())
            {
                try
                {
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposVeiculo();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposVeiculo();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Funções

        private Dictionary<TextBox, string> DicionarioTextBoxVeiculo(Util.CamposObg obg)
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            
            dicionario.Add(txtValorCompra, "Valor compra");
            dicionario.Add(txtValorVenda, "Valor venda");
            dicionario.Add(txtMotor, "Motor");
            dicionario.Add(txtAnoModelo, "Ano modelo");
            dicionario.Add(txtAnoFabricacao, "Ano fabricação");
            dicionario.Add(txtKm, "Km");

            if (obg == Util.CamposObg.Obrigatorio)
            {
                dicionario.Add(txtPlaca, "Placa");
                dicionario.Add(txtCodigo, "Codigo");
            }

            return dicionario;
        }

        private Dictionary<ComboBox, string> DicionarioComboBoxVeiculo(Util.CamposObg obg)
        {
            Dictionary<ComboBox, string> dicionario = new Dictionary<ComboBox, string>();

            dicionario.Add(cbMarca, "Marca");
            dicionario.Add(cbModelo, "Modelo");
            dicionario.Add(cbCor, "Cor");
            dicionario.Add(cbCombustivel, "Combustível");
            dicionario.Add(cbDirecao, "Direção");
            dicionario.Add(cbQtdPortas, "Qtd portas");
            dicionario.Add(cbSituacao, "Situação");

            if (obg == Util.CamposObg.Obrigatorio)
            {
                dicionario.Add(cbOpcionais, "Opcionais");
            }

            return dicionario;
        }

        private Dictionary<CheckBox, string> DicionarioCheckBoxVeiculo()
        {
            Dictionary<CheckBox, string> dicionario = new Dictionary<CheckBox, string>();
            dicionario.Add(chkGravarOffline, "Gravar offline");

            return dicionario;
        }

        private bool VerificarCamposVeiculo() => Util.VerificarCamposUtil(DicionarioTextBoxVeiculo(Util.CamposObg.NaoObrigatorio), null, DicionarioComboBoxVeiculo(Util.CamposObg.NaoObrigatorio));

        private void LimparCamposVeiculo() => Util.LimparCamposUtil(DicionarioTextBoxVeiculo(Util.CamposObg.Obrigatorio), null, DicionarioComboBoxVeiculo(Util.CamposObg.Obrigatorio), DicionarioCheckBoxVeiculo());

        #endregion
    }
}
