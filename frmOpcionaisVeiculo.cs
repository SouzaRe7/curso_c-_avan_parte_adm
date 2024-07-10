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
    public partial class frmOpcionaisVeiculo : Form
    {
        private OpicionaisVeiculoDAO opicionaisVeiculoDAO;
        public frmOpcionaisVeiculo()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
            opicionaisVeiculoDAO = new OpicionaisVeiculoDAO();
        }
        #region Eventos
        private void frmOpcionaisVeiculo_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdOpcionais);
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            CarregarOpcionais();
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposOpconais())
            {
                try
                {
                    CadastrarOpcionais();
                    Util.MsgSucessoUtil();
                    CarregarOpcionais();
                    LimparCamposOpconais();
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
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposOpconais();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposOpconais())
            {
                try
                {
                    AlterarOpcionais();
                    Util.MsgSucessoUtil();
                    CarregarOpcionais();
                    LimparCamposOpconais();
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
            if (Util.MsgExcluirItemUtil(txtNome.Text))
            {
                try
                {
                    ExcluirOpcionais();
                    Util.MsgSucessoUtil();
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                    CarregarOpcionais();
                    LimparCamposOpconais();
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposOpconais();
                }
            }
        }

        private void grdOpcionais_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdOpcionais.RowCount > 0)
            {
                GrdLinhaClicada();
                Util.ControleTela(Util.AcaoBotao.Editar, btnCadastar, btnAlterar, btnExcluir);
            }
        }
        #endregion

        #region Funções

        private void CadastrarOpcionais()
        {
            CadastrarOpcionaisDB();
            //CadastrarOpcionaisXML();
        }

        private void CadastrarOpcionaisDB()
        {
            opicionaisVeiculoDAO.InsertOpiconaisVeiculoDAO(new tb_opcionais
            {
                nome_opcionais = txtNome.Text.Trim(),
                id_empresa_opcionais = Util.CodigoEmpresaLogado,
                id_usuario_opcionais = Util.CodigoUsuarioLogado
            });
        }

        private void CadastrarOpcionaisXML() => XmlUtil.CadastrarUtilXML("id_opcionais", "tb_opcionais", "tb_opcionais_xml", DicionarioCamposOpcionaisXML());

        private void CarregarOpcionais()
        {
            CarregarOpcionaisDB();
            //CarregarOpcionaisXML();
        }

        private void CarregarOpcionaisDB()
        {
            Util.CarregarListaUtil(grdOpcionais, new List<tb_opcionais>(opicionaisVeiculoDAO.SelectOpicionaisVeiculoDAO(Util.CodigoEmpresaLogado)));
            Util.CarregarRemoverCabecalhoLista(grdOpcionais, ListaRemoveColunaGrdOpcionais());
            Util.CarregarAlterarCabecalhoLista(grdOpcionais, DicionarioAlteraNomeColunaGrdOpcionais());
        }

        private void CarregarOpcionaisXML()
        {
            XmlUtil.CarregarGrdUtilXML(grdOpcionais, XmlUtil.PathFileDbXml("tb_opcionais_xml"), "id_empresa_opcionais", Util.CodigoEmpresaLogado.ToString());
            XmlUtil.CarregarRemoverCabecalhoListaUtilXML(grdOpcionais, ListaRemoveColunaGrdOpcionaisXML(), XmlUtil.PathFileDbXml("tb_opcionais_xml"));
            XmlUtil.CarregarAlterarCabecalhoListaUtilXML(grdOpcionais, DicionarioAlteraNomeColunaGrdOpcionaisXML(), XmlUtil.PathFileDbXml("tb_opcionais_xml"));
        }

        private void GrdLinhaClicada()
        {
            GrdLinhaClicadaDB();
            //GrdLinhaClicadaXML();
        }

        private void GrdLinhaClicadaDB()
        {
            tb_opcionais objLinhaClicada = (tb_opcionais)grdOpcionais.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.id_opcionais.ToString();
            txtNome.Text = objLinhaClicada.nome_opcionais;
        }

        private void GrdLinhaClicadaXML()
        {
            var objLinhaClicada = (DataRowView)grdOpcionais.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.Row["id_opcionais"].ToString();
            txtNome.Text = objLinhaClicada.Row["nome_opcionais"].ToString();
        }

        private void AlterarOpcionais()
        {
            AlterarOpcionaisDB();
            //AlterarOpcionaisXML();
        }

        private void AlterarOpcionaisDB()
        {
            opicionaisVeiculoDAO.UpdateOpcionaisVeiculoDAO(new tb_opcionais
            {
                nome_opcionais = txtNome.Text.Trim(),
                id_opcionais = Convert.ToInt32(txtCodigo.Text)
            });
        }

        private void AlterarOpcionaisXML() => XmlUtil.AlterarUtilXML(txtCodigo.Text, "id_opcionais", "tb_opcionais_xml", DicionarioCamposOpcionaisXML());

        private void ExcluirOpcionais()
        {
            ExcluirOpcionaisDB();
            //ExcluirOpcionaisXML();
        }

        private void ExcluirOpcionaisDB() => opicionaisVeiculoDAO.DeleteOpcionaisDAO(Convert.ToInt32(txtCodigo.Text));

        private void ExcluirOpcionaisXML() => XmlUtil.ExcluirItemXmlUtl(txtCodigo.Text, "id_opcionais", "tb_opcionais_xml");

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdOpcionaisXML()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("id_opcionais", "Código");
            dicionario.Add("nome_opcionais", "Nome");

            return dicionario;
        }

        private Dictionary<string, string> DicionarioCamposOpcionaisXML()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("nome_opcionais", txtNome.Text.Trim());
            dicionario.Add("id_empresa_opcionais", Util.CodigoEmpresaLogado.ToString());
            dicionario.Add("id_usuario_opcionais", Util.CodigoUsuarioLogado.ToString());

            return dicionario;
        }

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

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdOpcionais()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("id_opcionais", "Código");
            dicionario.Add("nome_opcionais", "Nome");

            return dicionario;
        }

        private List<string> ListaRemoveColunaGrdOpcionaisXML() => new List<string> { "id_empresa_opcionais", "id_usuario_opcionais" };

        private List<string> ListaRemoveColunaGrdOpcionais() => new List<string> { "id_empresa_opcionais", "id_usuario_opcionais", "tb_empresa", "tb_usuario" };

        private bool VerificarCamposOpconais() => Util.VerificarCamposUtil(DicionarioTextBoxOpconais(Util.CamposObg.NaoObrigatorio));

        private void LimparCamposOpconais() => Util.LimparCamposUtil(DicionarioTextBoxOpconais(Util.CamposObg.Obrigatorio));

        #endregion
    }
}
