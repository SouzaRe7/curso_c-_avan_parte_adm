using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAO;

namespace VendasAdm
{
    public partial class frmMarca : Form
    {
        private MarcaDAO marcaDAO;
        public frmMarca()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
            marcaDAO = new MarcaDAO();
        }

        #region Eventos
        private void frmMarca_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdMarcas);
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            CarregarMarcas();
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposMarca())
            {
                try
                {
                    CadastrarMarca();
                    Util.MsgSucessoUtil();
                    CarregarMarcas();
                    LimparCamposMarca();
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
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposMarca();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposMarca())
            {
                try
                {
                    AlterarMarca();
                    Util.MsgSucessoUtil();
                    CarregarMarcas();
                    LimparCamposMarca();
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
            if (Util.MsgExcluirItemUtil(txtNome.Text))
            {
                try
                {
                    ExcluirMarca();
                    Util.MsgSucessoUtil();
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                    CarregarMarcas();
                    LimparCamposMarca();
                }
                catch
                {
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                    LimparCamposMarca();
                }
            }
        }

        private void grdMarcas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdMarcas.RowCount > 0)
            {
                GrdLinhaClicada();
                Util.ControleTela(Util.AcaoBotao.Editar, btnCadastar, btnAlterar, btnExcluir);
            }
        }
        #endregion

        #region Funções

        private void CadastrarMarca()
        {
            CadastrarMarcaDB();
            //CadastrarMarcaXML();
        }

        private void CadastrarMarcaDB()
        {
            marcaDAO.InsertMarcaDAO(new tb_marca
            {
                nome_marca = txtNome.Text.Trim(),
                id_empresa_marca = Util.CodigoEmpresaLogado,
                id_usuario_marca = Util.CodigoUsuarioLogado,
            });
        }

        private void CadastrarMarcaXML() => XmlUtil.CadastrarUtilXML("id_marca", "tb_marca", "tb_marca_xml", DicionarioCamposMarcaXML());

        private void CarregarMarcas()
        {
            CarregarMarcaDB();
            //CarregarMarcaXML();
        }

        private void CarregarMarcaDB()
        {
            Util.CarregarListaUtil(grdMarcas, new List<tb_marca>(marcaDAO.SelectMarcaDAO(Util.CodigoEmpresaLogado)));
            Util.CarregarRemoverCabecalhoLista(grdMarcas, ListaRemoveColunaGrdMarcas());
            Util.CarregarAlterarCabecalhoLista(grdMarcas, DicionarioAlteraNomeColunaGrdMarcas());
        }

        private void CarregarMarcaXML()
        {
            XmlUtil.CarregarGrdUtilXML(grdMarcas, XmlUtil.PathFileDbXml("tb_marca_xml"), "id_empresa_marca", Util.CodigoEmpresaLogado.ToString());
            XmlUtil.CarregarRemoverCabecalhoListaUtilXML(grdMarcas, ListaRemoveColunaGrdMarcasXML(), XmlUtil.PathFileDbXml("tb_marca_xml"));
            XmlUtil.CarregarAlterarCabecalhoListaUtilXML(grdMarcas, DicionarioAlteraNomeColunaGrdMarcasXML(), XmlUtil.PathFileDbXml("tb_marca_xml"));
        }

        private void GrdLinhaClicada()
        {
            GrdLinhaClicadaDB();
            //GrdLinhaClicadaXML();
        }

        private void GrdLinhaClicadaDB()
        {
            tb_marca objLinhaClicada = (tb_marca)grdMarcas.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.id_marca.ToString();
            txtNome.Text = objLinhaClicada.nome_marca;
        }

        private void GrdLinhaClicadaXML()
        {
            var objLinhaClicada = (DataRowView)grdMarcas.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.Row["id_marca"].ToString();
            txtNome.Text = objLinhaClicada.Row["nome_marca"].ToString();
        }

        private void AlterarMarca()
        {
            AlterarMarcaDB();
            //AlterarMarcaXML();
        }

        private void AlterarMarcaDB()
        {
            marcaDAO.UpdateMarcaDAO(new tb_marca
            {
                nome_marca = txtNome.Text.Trim(),
                id_marca = Convert.ToInt32(txtCodigo.Text)
            });
        }

        private void AlterarMarcaXML() => XmlUtil.AlterarUtilXML(txtCodigo.Text, "id_marca", "tb_marca_xml", DicionarioCamposMarcaXML());

        private void ExcluirMarca()
        {
            ExcluirMarcaDB();
            //ExcluirMarcaXML();
        }

        private void ExcluirMarcaDB() => marcaDAO.DeleteMarcaDAO(Convert.ToInt32(txtCodigo.Text));

        private void ExcluirMarcaXML() => XmlUtil.ExcluirItemXmlUtl(txtCodigo.Text, "id_marca", "tb_marca_xml");

        private Dictionary<string, string> DicionarioCamposMarcaXML()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("nome_marca", txtNome.Text.Trim());
            dicionario.Add("id_empresa_marca", Util.CodigoEmpresaLogado.ToString());
            dicionario.Add("id_usuario_marca", Util.CodigoUsuarioLogado.ToString());

            return dicionario;
        }

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

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdMarcas()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("id_marca", "Código");
            dicionario.Add("nome_marca", "Nome");

            return dicionario;
        }

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdMarcasXML()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("id_marca", "Código");
            dicionario.Add("nome_marca", "Nome");

            return dicionario;
        }

        private List<string> ListaRemoveColunaGrdMarcasXML() => new List<string> { "id_empresa_marca", "id_usuario_marca" };

        private List<string> ListaRemoveColunaGrdMarcas() => new List<string> { "id_empresa_marca", "id_usuario_marca", "tb_empresa", "tb_usuario", "tb_modelo" };

        private bool VerificarCamposMarca() => Util.VerificarCamposUtil(DicionarioTextBoxMarca(Util.CamposObg.NaoObrigatorio));

        private void LimparCamposMarca() => Util.LimparCamposUtil(DicionarioTextBoxMarca(Util.CamposObg.Obrigatorio));

        #endregion
    }
}
