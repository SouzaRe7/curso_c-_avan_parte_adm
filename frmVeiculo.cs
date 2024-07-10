using DAO;
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
        private CorDAO corDAO;
        private MarcaDAO marcaDAO;
        private ModeloDAO modeloDAO;
        private VeiculoDAO veiculoDAO;
        private TipoVeiculoDAO tipoVeiculoDAO;
        private OpicionaisVeiculoDAO opicionaisVeiculoDAO;
        private List<string> itensSelecionados = new List<string>();

        public frmVeiculo()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
            corDAO = new CorDAO();
            marcaDAO = new MarcaDAO();
            modeloDAO = new ModeloDAO();
            veiculoDAO = new VeiculoDAO();
            tipoVeiculoDAO = new TipoVeiculoDAO();
            opicionaisVeiculoDAO = new OpicionaisVeiculoDAO();
        }
        #region Eventos
        private void frmVeiculo_Load(object sender, EventArgs e)
        {
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            CarrecarCores();
            CarrecarMarcas();
            CarrecarModelos();
            CarrecarTiposVeiculos();
            CarrecarOpicionaisVeiculo();
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposVeiculo())
            {
                try
                {
                    CadastarVeiculo();
                    Util.MsgSucessoUtil();
                    LimparCamposVeiculo();
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
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            new frmConsultarVeiculo().ShowDialog();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            // Verifica se um item está selecionado na ComboBox
            if (cbOpcionais.SelectedItem != null)
            {
                // Obtém o objeto selecionado
                var opcionalSelecionado = (tb_opcionais)cbOpcionais.SelectedItem;

                // Obtém o nome do opcional
                string nomeOpcional = opcionalSelecionado.nome_opcionais;

                // Adiciona o nome do opcional à ListBox
                lstOpcoesSelecionadas.Items.Add(nomeOpcional);

                // Adiciona o item à lista de itens selecionados
                itensSelecionados.Add(nomeOpcional);

                // Remove o item da fonte de dados
                var lstOpcionais = (List<tb_opcionais>)cbOpcionais.DataSource;
                lstOpcionais.Remove(opcionalSelecionado);

                // Atualiza o DataSource da ComboBox
                cbOpcionais.DataSource = null;
                cbOpcionais.DataSource = lstOpcionais;
                cbOpcionais.DisplayMember = "nome_opcionais";
                cbOpcionais.ValueMember = "nome_opcionais";
                cbOpcionais.SelectedIndex = -1;
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            // Verifica se há um item selecionado na ListBox
            if (lstOpcoesSelecionadas.SelectedIndex != -1)
            {
                // Obtém o item selecionado na ListBox
                string itemRemovido = lstOpcoesSelecionadas.SelectedItem.ToString();

                // Remove o item da ListBox
                lstOpcoesSelecionadas.Items.RemoveAt(lstOpcoesSelecionadas.SelectedIndex);

                // Remove o item da lista de itens selecionados
                itensSelecionados.Remove(itemRemovido);

                // Adiciona o item de volta à ComboBox
                // Primeiramente, obtemos a lista original da fonte de dados da ComboBox
                var lstOpcionais = (List<tb_opcionais>)cbOpcionais.DataSource;

                // Criamos um novo objeto tb_opcionais para adicionar de volta à lista
                tb_opcionais opcionalRemovido = new tb_opcionais { nome_opcionais = itemRemovido };

                // Adicionamos o objeto à lista de opcionais
                lstOpcionais.Add(opcionalRemovido);

                // Atualiza o DataSource da ComboBox
                cbOpcionais.DataSource = null;
                cbOpcionais.DataSource = lstOpcionais;
                cbOpcionais.DisplayMember = "nome_opcionais";
                cbOpcionais.ValueMember = "nome_opcionais";
                cbOpcionais.SelectedIndex = -1;
            }
        }

        private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarrecarModelos();
            cbModelo.Enabled = true;
        }
        #endregion

        #region Funções

        private void CadastarVeiculo()
        {
            CadastarVeiculoDB();
        }

        private void CadastarVeiculoDB()
        {
            veiculoDAO.InsertVeiculoDAO(new tb_veiculo
            {
                id_modelo_veiculo = Convert.ToInt32(cbModelo.SelectedValue),
                id_cor_veiculo = Convert.ToInt32(cbCor.SelectedValue),
                combustivel_veiculo = Convert.ToInt16(cbCombustivel.SelectedValue),
                direcao_veiculo = Convert.ToInt16(cbDirecao.SelectedValue),
                qtd_portas_veiculo = Convert.ToInt16(cbQtdPortas.SelectedValue),
                situacao_veiculo = Convert.ToInt16(cbSituacao.SelectedValue),
                id_tipo_veiculo_veiculo = Convert.ToInt16(cbTipoVeiculo.SelectedValue),
                valor_compra_veiculo = Convert.ToDecimal(txtValorCompra.Text),
                valor_venda_veiculo = Convert.ToDecimal(txtValorVenda.Text),
                motor_veiculo = txtMotor.Text.Trim(),
                ano_fabricacao_veiculo = txtAnoFabricacao.Text.Trim(),
                ano_modelo_veiculo = txtAnoModelo.Text.Trim(),
                placa_veiculo = txtPlaca.Text.Trim(),
                km_veiculo = txtKm.Text.Trim(),
                opcionais_veiculo = ConverteListaOpcionaisVeiculoParaString(),
                obs_veiculo = txtObs.Text.Trim(),
                id_empresa_veiculo = Util.CodigoEmpresaLogado,
                id_usuario_veiculo = Util.CodigoUsuarioLogado,
            });
        }

        private void AlterarVeiculo()
        {
            AlterarVeiculoDB();
        }

        private void AlterarVeiculoDB()
        {
            veiculoDAO.UpdateVeiculoDAO(new tb_veiculo
            {
                id_modelo_veiculo = Convert.ToInt32(cbModelo.SelectedValue),
                id_cor_veiculo = Convert.ToInt32(cbCor.SelectedValue),
                combustivel_veiculo = Convert.ToInt16(cbCombustivel.SelectedValue),
                direcao_veiculo = Convert.ToInt16(cbDirecao.SelectedValue),
                qtd_portas_veiculo = Convert.ToInt16(cbQtdPortas.SelectedValue),
                situacao_veiculo = Convert.ToInt16(cbSituacao.SelectedValue),
                id_tipo_veiculo_veiculo = Convert.ToInt16(cbTipoVeiculo.SelectedValue),
                valor_compra_veiculo = Convert.ToDecimal(txtValorCompra.Text),
                valor_venda_veiculo = Convert.ToDecimal(txtValorVenda.Text),
                motor_veiculo = txtMotor.Text.Trim(),
                ano_fabricacao_veiculo = txtAnoFabricacao.Text.Trim(),
                ano_modelo_veiculo = txtAnoModelo.Text.Trim(),
                placa_veiculo = txtPlaca.Text.Trim(),
                km_veiculo = txtKm.Text.Trim(),
                opcionais_veiculo = ConverteListaOpcionaisVeiculoParaString(),
                obs_veiculo = txtObs.Text.Trim(),
                id_veiculo = Convert.ToInt32(txtCodigo.Text),
            });
        }

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
                dicionario.Add(txtObs, "Observação");
            }

            return dicionario;
        }

        private Dictionary<ComboBox, string> DicionarioComboBoxVeiculo(Util.CamposObg obg)
        {
            Dictionary<ComboBox, string> dicionario = new Dictionary<ComboBox, string>();

            dicionario.Add(cbTipoVeiculo, "Tipo veículo");
            dicionario.Add(cbModelo, "Modelo");
            dicionario.Add(cbCor, "Cor");
            dicionario.Add(cbCombustivel, "Combustível");
            dicionario.Add(cbDirecao, "Direção");
            dicionario.Add(cbQtdPortas, "Qtd portas");
            dicionario.Add(cbSituacao, "Situação");
            dicionario.Add(cbMarca, "Marca");

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

        private void CarrecarCores()
        {
            List<tb_cor> lstCores = corDAO.SelectCorDAO(Util.CodigoEmpresaLogado);
            cbCor.DisplayMember = "nome_cor";
            cbCor.ValueMember = "id_cor";
            cbCor.DataSource = lstCores;
            cbCor.SelectedIndex = -1;
        }

        private void CarrecarMarcas()
        {
            List<tb_marca> lstMarcas = marcaDAO.SelectMarcaDAO(Util.CodigoEmpresaLogado);
            cbMarca.DisplayMember = "nome_marca";
            cbMarca.ValueMember = "id_marca";
            cbMarca.DataSource = lstMarcas;
            cbMarca.SelectedIndex = -1;
        }

        private void CarrecarModelos()
        {
                cbModelo.Enabled = false;
                List<tb_modelo> lstModelos = modeloDAO.FilterModeloMarcaDAO(Util.CodigoEmpresaLogado, Convert.ToInt32(cbMarca.SelectedValue));
                if (lstModelos.Count == 0)
                {
                    lstModelos.Add(new tb_modelo { id_modelo = 0, nome_modelo = "Nenhum modelo encontrado" });
                }
                cbModelo.DisplayMember = "nome_modelo";
                cbModelo.ValueMember = "id_modelo";
                cbModelo.DataSource = lstModelos;
                cbModelo.SelectedIndex = -1;
            
        }

        private void CarrecarTiposVeiculos()
        {
            List<tb_tipo_veiculo> lstTipoVeiculos = tipoVeiculoDAO.SelectTipoVeiculoDAO(Util.CodigoEmpresaLogado);
            cbTipoVeiculo.DisplayMember = "nome_tipo_veiculo";
            cbTipoVeiculo.ValueMember = "id_tipo_veiculo";
            cbTipoVeiculo.DataSource = lstTipoVeiculos;
            cbTipoVeiculo.SelectedIndex = -1;
        }

        private void CarrecarOpicionaisVeiculo()
        {
            List<tb_opcionais> lstOpcionais = opicionaisVeiculoDAO.SelectOpicionaisVeiculoDAO(Util.CodigoEmpresaLogado);
            cbOpcionais.DisplayMember = "nome_opcionais";
            cbOpcionais.ValueMember = "nome_opcionais";
            cbOpcionais.DataSource = lstOpcionais;
            cbOpcionais.SelectedIndex = -1;
        }

        private string ConverteListaOpcionaisVeiculoParaString()
        {
            List<string> itens = new List<string>();

            foreach (var item in lstOpcoesSelecionadas.Items)
            {
                itens.Add(item.ToString());
            }

            return string.Join(",", itens);
        }

        private bool VerificarCamposVeiculo() => Util.VerificarCamposUtil(DicionarioTextBoxVeiculo(Util.CamposObg.NaoObrigatorio), null, DicionarioComboBoxVeiculo(Util.CamposObg.NaoObrigatorio));

        private void LimparCamposVeiculo()
        {
            lstOpcoesSelecionadas.Items.Clear();
            CarrecarOpicionaisVeiculo();
            Util.LimparCamposUtil(DicionarioTextBoxVeiculo(Util.CamposObg.Obrigatorio), null, DicionarioComboBoxVeiculo(Util.CamposObg.Obrigatorio), DicionarioCheckBoxVeiculo());
            cbModelo.Enabled = false;
        }

        #endregion
    }
}
