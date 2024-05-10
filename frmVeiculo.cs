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
        #region Globais
        private enum CamposObg
        {
            Obrigatorio,
            NaoObrigatorio
        }
        #endregion
        #region Eventos
        private void frmVeiculo_Load(object sender, EventArgs e)
        {
            Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
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
                    Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
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
            Util.ControleTela("Inicial", btnCadastar, btnAlterar, btnExcluir);
            LimparCamposVeiculo();
        }
        #endregion

        #region Funções

        private Dictionary<TextBox, string> DicionarioTextBoxVeiculo(CamposObg tipo)
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            
            dicionario.Add(txtValorCompra, "Valor compra");
            dicionario.Add(txtValorVenda, "Valor venda");
            dicionario.Add(txtMotor, "Motor");
            dicionario.Add(txtAnoModelo, "Ano modelo");
            dicionario.Add(txtAnoFabricacao, "Ano fabricação");
            dicionario.Add(txtKm, "Km");

            if (tipo == CamposObg.NaoObrigatorio)
            {
                dicionario.Add(txtPlaca, "Placa");
            }

            return dicionario;
        }

        private Dictionary<ComboBox, string> DicionarioComboBoxVeiculo(CamposObg tipo)
        {
            Dictionary<ComboBox, string> dicionario = new Dictionary<ComboBox, string>();

            dicionario.Add(cbMarca, "Marca");
            dicionario.Add(cbModelo, "Modelo");
            dicionario.Add(cbCor, "Cor");
            dicionario.Add(cbCombustivel, "Combustível");
            dicionario.Add(cbDirecao, "Direção");
            dicionario.Add(cbQtdPortas, "Qtd portas");
            dicionario.Add(cbSituacao, "Situação");

            if (tipo == CamposObg.NaoObrigatorio)
            {
                dicionario.Add(cbOpcionais, "Opcionais");
            }

            return dicionario;
        }

        private bool VerificarCamposVeiculo() => Util.VerificarCamposUtil(DicionarioTextBoxVeiculo(CamposObg.Obrigatorio), new Dictionary<MaskedTextBox, string>(), DicionarioComboBoxVeiculo(CamposObg.Obrigatorio), new Dictionary<CheckBox, string>());

        private void LimparCamposVeiculo() => Util.LimparCamposUtil(DicionarioTextBoxVeiculo(CamposObg.NaoObrigatorio), new Dictionary<MaskedTextBox, string>(), DicionarioComboBoxVeiculo(CamposObg.NaoObrigatorio), new Dictionary<CheckBox, string>());

        #endregion
    }
}
