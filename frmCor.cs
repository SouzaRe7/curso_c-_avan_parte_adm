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
    public partial class frmCor : Form
    {
        private CorDAO corDAO;
        public frmCor()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
            corDAO = new CorDAO();
        }

        #region Eventos
        private void frmCor_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdCores);
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            CarregarCores();
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposCor())
            {
                try
                {
                    CadastrarCor();
                    Util.MsgSucessoUtil();
                    CarregarCores();
                    LimparCamposCor();
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
                    AlterarCor();
                    Util.MsgSucessoUtil();
                    CarregarCores();
                    LimparCamposCor();
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
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposCor();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Util.MsgExcluirItemUtil(txtNome.Text))
            {
                try
                {
                    ExcluirCor();
                    Util.MsgSucessoUtil();
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                    CarregarCores();
                    LimparCamposCor();
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposCor();
                }
            }
        }

        private void grdCores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdCores.RowCount > 0)
            {
                GrdLinhaClicada();
                Util.ControleTela(Util.AcaoBotao.Editar, btnCadastar, btnAlterar, btnExcluir);
            }
        }
        #endregion

        #region Funções

        private void CadastrarCor()
        {
            CadastrarCorDB();
            //CadastrarCorXML();
        }

        private void CadastrarCorDB()
        {
            corDAO.InsertCorDAO(new tb_cor
            {
                nome_cor = txtNome.Text.Trim(),
                id_usuario_cor = Util.CodigoUsuarioLogado,
                id_empresa_cor = Util.CodigoEmpresaLogado,
            });
        }

        private void CadastrarCorXML() => XmlUtil.CadastrarUtilXML("id_cor", "tb_cor", "tb_cor_xml", DicionarioCamposCorXML());

        private void CarregarCores()
        {
            CarregarCoresDB();
            //CarregarCoresXML();
        }

        private void CarregarCoresDB()
        {
            Util.CarregarListaUtil(grdCores, new List<tb_cor>(corDAO.SelectCorDAO(Util.CodigoEmpresaLogado)));
            Util.CarregarRemoverCabecalhoLista(grdCores, ListaRemoveColunaGrdCoresDB());
            Util.CarregarAlterarCabecalhoLista(grdCores, DicionarioAlteraNomeColunaGrdCoresDB());
        }

        private void CarregarCoresXML()
        {
            XmlUtil.CarregarGrdUtilXML(grdCores, XmlUtil.PathFileDbXml("tb_cor_xml"), "id_empresa_cor", Util.CodigoEmpresaLogado.ToString());
            XmlUtil.CarregarRemoverCabecalhoListaUtilXML(grdCores, ListaRemoveColunaGrdCoresXML(), XmlUtil.PathFileDbXml("tb_cor_xml"));
            XmlUtil.CarregarAlterarCabecalhoListaUtilXML(grdCores, DicionarioAlteraNomeColunaGrdCoresXML(), XmlUtil.PathFileDbXml("tb_cor_xml"));
        }

        private void GrdLinhaClicada()
        {
            GrdLinhaClicadaDB();
            //GrdLinhaClicadaXML();
        }

        private void GrdLinhaClicadaDB()
        {
            tb_cor objLinhaClicada = (tb_cor)grdCores.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.id_cor.ToString();
            txtNome.Text = objLinhaClicada.nome_cor;
        }

        private void GrdLinhaClicadaXML()
        {
            var objLinhaClicada = (DataRowView)grdCores.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.Row["id_cor"].ToString();
            txtNome.Text = objLinhaClicada.Row["nome_cor"].ToString();
        }

        private void AlterarCor()
        {
            AlterarCorDB();
            //AlterarCorXML();
        }

        private void AlterarCorDB()
        {
            corDAO.UpdateCorDAO(new tb_cor
            {
                nome_cor = txtNome.Text.Trim(),
                id_cor = Convert.ToInt32(txtCodigo.Text)
            });
        }

        private void AlterarCorXML() => XmlUtil.AlterarUtilXML(txtCodigo.Text, "id_cor", "tb_cor_xml", DicionarioCamposCorXML());
        
        private void ExcluirCor()
        {
            ExcluirCorDB();
            //ExcluirCorXML();
        }

        private void ExcluirCorDB() => corDAO.DeleteCorDAO(Convert.ToInt32(txtCodigo.Text));

        private void ExcluirCorXML() => XmlUtil.ExcluirItemXmlUtl(txtCodigo.Text, "id_cor", "tb_cor_xml");

        private Dictionary<string, string> DicionarioCamposCorXML()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("nome_cor", txtNome.Text.Trim());
            dicionario.Add("id_empresa_cor", Util.CodigoEmpresaLogado.ToString());
            dicionario.Add("id_usuario_cor", Util.CodigoUsuarioLogado.ToString());

            return dicionario;
        }

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

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdCoresDB()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("id_cor", "Código");
            dicionario.Add("nome_cor", "Nome");

            return dicionario;
        }

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdCoresXML()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("id_cor", "Código");
            dicionario.Add("nome_cor", "Nome");

            return dicionario;
        }

        private List<string> ListaRemoveColunaGrdCoresDB() => new List<string> { "id_empresa_cor", "id_usuario_cor", "tb_empresa", "tb_usuario", "tb_veiculo" };

        private List<string> ListaRemoveColunaGrdCoresXML() => new List<string> { "id_empresa_cor", "id_usuario_cor" };

        private bool VerificarCamposCor() => Util.VerificarCamposUtil(DicionarioTextBoxCor(Util.CamposObg.NaoObrigatorio));

        private void LimparCamposCor()=> Util.LimparCamposUtil(DicionarioTextBoxCor(Util.CamposObg.Obrigatorio));

        #endregion
    }
}
