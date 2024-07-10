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
    public partial class frmTipoVeiculo : Form
    {
        private TipoVeiculoDAO tipoVeiculoDAO;
        public frmTipoVeiculo()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
            tipoVeiculoDAO = new TipoVeiculoDAO();
        }

        #region Eventos
        private void frmTipoVeiculo_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdTiposVeiculos);
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            CarregarTipoVeiculo();
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposTipoVeiculo())
            {
                try
                {
                    CadastrarTipoVeiculo();
                    Util.MsgSucessoUtil();
                    CarregarTipoVeiculo();
                    LimparCamposTipoVeiculo();
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposTipoVeiculo();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposTipoVeiculo();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposTipoVeiculo())
            {
                try
                {
                    AlterarTipoVeiculo();
                    Util.MsgSucessoUtil();
                    CarregarTipoVeiculo();
                    LimparCamposTipoVeiculo();
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposTipoVeiculo();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Util.MsgExcluirItemUtil(txtNome.Text))
            {
                try
                {
                    ExcluirTipoVeiculo();
                    Util.MsgSucessoUtil();
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                    CarregarTipoVeiculo();
                    LimparCamposTipoVeiculo();
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposTipoVeiculo();
                }
            }
        }

        private void grdTiposVeiculos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdTiposVeiculos.RowCount > 0)
            {
                GrdLinhaClicada();
                Util.ControleTela(Util.AcaoBotao.Editar, btnCadastar, btnAlterar, btnExcluir);
            }
        }

        #endregion

        #region Funções

        private void CadastrarTipoVeiculo()
        {
            CadastrarTipoVeiculoDB();
            //CadastrarTipoVeiculoXML();
        }

        private void CadastrarTipoVeiculoDB()
        {
            tipoVeiculoDAO.InsertTipoVeiculoDAO(new tb_tipo_veiculo
            {
                nome_tipo_veiculo = txtNome.Text.Trim(),
                id_empresa_tipo_veiculo = Util.CodigoEmpresaLogado,
                id_usuario_tipo_veiculo = Util.CodigoUsuarioLogado
            });
        }

        private void CadastrarTipoVeiculoXML() => XmlUtil.CadastrarUtilXML("id_tipo_veiculo", "tb_tipo_veiculo", "tb_tipo_veiculo_xml", DicionarioCamposTipoVeiculoXML());

        private void CarregarTipoVeiculo()
        {
            CarregarTipoVeiculoDB();
            //CarregarTipoVeiculoXML();
        }

        private void CarregarTipoVeiculoDB()
        {
            Util.CarregarListaUtil(grdTiposVeiculos, new List<tb_tipo_veiculo>(tipoVeiculoDAO.SelectTipoVeiculoDAO(Util.CodigoEmpresaLogado)));
            Util.CarregarRemoverCabecalhoLista(grdTiposVeiculos, ListaRemoveColunaGrdTipoVeiculo());
            Util.CarregarAlterarCabecalhoLista(grdTiposVeiculos, DicionarioAlteraNomeColunaGrdTipoVeiculo());
        }

        private void CarregarTipoVeiculoXML()
        {
            XmlUtil.CarregarGrdUtilXML(grdTiposVeiculos, XmlUtil.PathFileDbXml("tb_tipo_veiculo_xml"), "id_empresa_tipo_veiculo", Util.CodigoEmpresaLogado.ToString());
            XmlUtil.CarregarRemoverCabecalhoListaUtilXML(grdTiposVeiculos, ListaRemoveColunaGrdTipoVeiculoXML(), XmlUtil.PathFileDbXml("tb_tipo_veiculo_xml"));
            XmlUtil.CarregarAlterarCabecalhoListaUtilXML(grdTiposVeiculos, DicionarioAlteraNomeColunaGrdTipoVeiculoXML(), XmlUtil.PathFileDbXml("tb_tipo_veiculo_xml"));
        }

        private void GrdLinhaClicada()
        {
            GrdLinhaClicadaDB();
            //GrdLinhaClicadaXML();
        }

        private void GrdLinhaClicadaDB()
        {
            tb_tipo_veiculo objLinhaClicada = (tb_tipo_veiculo)grdTiposVeiculos.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.id_tipo_veiculo.ToString();
            txtNome.Text = objLinhaClicada.nome_tipo_veiculo;
        }

        private void GrdLinhaClicadaXML()
        {
            var objLinhaClicada = (DataRowView)grdTiposVeiculos.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.Row["id_tipo_veiculo"].ToString();
            txtNome.Text = objLinhaClicada.Row["nome_tipo_veiculo"].ToString();
        }

        private void AlterarTipoVeiculo()
        {
            AlterarTipoVeiculoDB();
            //AlterarTipoVeiculoXML();
        }

        private void AlterarTipoVeiculoDB()
        {
            tipoVeiculoDAO.UpdateTipoVeiculoDAO(new tb_tipo_veiculo
            {
                nome_tipo_veiculo = txtNome.Text,
                id_tipo_veiculo = Convert.ToInt32(txtCodigo.Text)
            });
        }

        private void AlterarTipoVeiculoXML() => XmlUtil.AlterarUtilXML(txtCodigo.Text, "id_tipo_veiculo", "tb_tipo_veiculo_xml", DicionarioCamposTipoVeiculoXML());

        private void ExcluirTipoVeiculo()
        {
            ExcluirTipoVeiculoDB();
            //ExcluirTipoVeiculoXML();
        }

        private void ExcluirTipoVeiculoDB() => tipoVeiculoDAO.DeleteTipoVeiculoDAO(Convert.ToInt32(txtCodigo.Text));

        private void ExcluirTipoVeiculoXML() => XmlUtil.ExcluirItemXmlUtl(txtCodigo.Text, "id_tipo_veiculo", "tb_tipo_veiculo_xml");

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdTipoVeiculoXML()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("id_tipo_veiculo", "Código");
            dicionario.Add("nome_tipo_veiculo", "Nome");

            return dicionario;
        }

        private Dictionary<string, string> DicionarioCamposTipoVeiculoXML()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("nome_tipo_veiculo", txtNome.Text.Trim());
            dicionario.Add("id_empresa_tipo_veiculo", Util.CodigoEmpresaLogado.ToString());
            dicionario.Add("id_usuario_tipo_veiculo", Util.CodigoUsuarioLogado.ToString());

            return dicionario;
        }

        private Dictionary<TextBox, string> DicionarioTextBoxTipoVeiculo(Util.CamposObg obg)
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");

            if (obg == Util.CamposObg.Obrigatorio)
            {
                dicionario.Add(txtCodigo, "Codigo");
            }
            return dicionario;
        }

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdTipoVeiculo()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("id_tipo_veiculo", "Código");
            dicionario.Add("nome_tipo_veiculo", "Nome");

            return dicionario;
        }

        private List<string> ListaRemoveColunaGrdTipoVeiculoXML() => new List<string> { "id_usuario_tipo_veiculo", "id_empresa_tipo_veiculo" };

        private List<string> ListaRemoveColunaGrdTipoVeiculo() => new List<string> { "id_empresa_tipo_veiculo", "id_usuario_tipo_veiculo", "tb_empresa", "tb_usuario", "tb_veiculo" };

        private bool VerificarCamposTipoVeiculo() => Util.VerificarCamposUtil(DicionarioTextBoxTipoVeiculo(Util.CamposObg.NaoObrigatorio));

        private void LimparCamposTipoVeiculo() => Util.LimparCamposUtil(DicionarioTextBoxTipoVeiculo(Util.CamposObg.Obrigatorio));

        #endregion
    }
}
