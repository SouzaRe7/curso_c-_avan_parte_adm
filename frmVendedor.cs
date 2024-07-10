
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
    public partial class frmVendedor : Form
    {
        public frmVendedor()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
        }
        #region Eventos
        private void frmVendedor_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdVendedores);
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            CarregarVendedores();
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposVendedor())
            {
                try
                {
                    CadastrarVendedor();
                    Util.MsgSucessoUtil();
                    CarregarVendedores();
                    LimparCamposVendedor();
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposVendedor();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposVendedor();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (Util.VerificarCamposUtil(DicionarioTextBoxVendedor(Util.CamposObg.NaoObrigatorio), DicionarioMaskedTextBoxVendedor(), null, null))
            {
                try
                {
                    AlterarVendedor();
                    Util.MsgSucessoUtil();
                    CarregarVendedores();
                    LimparCamposVendedor();
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposVendedor();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Util.MsgExcluirItemUtil(txtNome.Text))
            {
                try
                {
                    ExcluirVendedor();
                    Util.MsgSucessoUtil();
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                    CarregarVendedores();
                    LimparCamposVendedor();
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposVendedor();
                }
            }
        }

        private void grdVendedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdVendedores.RowCount > 0)
            {
                GrdLinhaClicada();
                Util.ControleTela(Util.AcaoBotao.Editar, btnCadastar, btnAlterar, btnExcluir);
            }
        }

        private void txtNomeFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarVendedor();
        }

        private void chkStatusFiltro_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarVendedor();
        }

        #endregion

        #region Funções

        private void CadastrarVendedor()
        {
            CadastrarVendedorDB();
            //CadastrarVendedorXML();
        }

        private void CadastrarVendedorDB()
        {
            VendedorDAO vendedorDAO = new VendedorDAO();
            tb_usuario objUsuario = new tb_usuario();

            objUsuario.nome_usuario = txtNome.Text.Trim();
            objUsuario.email_usuario = txtEmail.Text.Trim();
            objUsuario.telefone_usuario = mkTelefone.Text.Trim();
            objUsuario.cpf_usuario = mkCpf.Text.Trim();
            objUsuario.tipo_usuario = (int)Util.TipoUsuario.Vendedor;
            objUsuario.senha_usuario = mkCpf.Text.Trim();
            objUsuario.status_usuario = chkStatus.Checked;
            objUsuario.id_empresa_usuario = Util.CodigoEmpresaLogado;

            vendedorDAO.InsertUsuarioDAO(objUsuario);
        }

        private void CadastrarVendedorXML() => XmlUtil.CadastrarUtilXML("id_usuario", "tb_usuario", "tb_usuario_xml", DicionarioCamposVendedorXML());


        private void CarregarVendedores()
        {
            
            CarregarVendedorDB();
            //CarregarVendedorXML();    
        }

        private void CarregarVendedorDB()
        {
            VendedorDAO vendedorDAO = new VendedorDAO();
            Util.CarregarListaUtil(grdVendedores, new List<tb_usuario>(vendedorDAO.SelectUsuarioDAO(Util.CodigoEmpresaLogado, (int)Util.TipoUsuario.Vendedor)));
            Util.CarregarRemoverCabecalhoLista(grdVendedores, ListaRemoveColunaGrdVendedores());
            Util.CarregarAlterarCabecalhoLista(grdVendedores, DicionarioAlteraNomeColunaGrdVendedores());
            Util.AplicarMascaraCpfUtil(grdVendedores, "cpf_usuario");
            Util.AplicarMascaraTelefoneCelularUtil(grdVendedores, "telefone_usuario");
        }

        private void CarregarVendedorXML()
        {
            XmlUtil.CarregarGrdUtilXML(grdVendedores, XmlUtil.PathFileDbXml("tb_usuario_xml"), "id_empresa_usuario", Util.CodigoEmpresaLogado.ToString());
            XmlUtil.CarregarRemoverCabecalhoListaUtilXML(grdVendedores, ListaRemoveColunaGrdVendedorXML(), XmlUtil.PathFileDbXml("tb_usuario_xml"));
            XmlUtil.CarregarAlterarCabecalhoListaUtilXML(grdVendedores, DicionarioAlteraNomeColunaGrdVendedores(), XmlUtil.PathFileDbXml("tb_usuario_xml"));
            Util.AplicarMascaraCpfUtil(grdVendedores, "cpf_usuario");
            Util.AplicarMascaraTelefoneCelularUtil(grdVendedores, "telefone_usuario");
        }

        private void GrdLinhaClicada()
        {
            GrdLinhaClicadaDB();
            //GrdLinhaClicadaXML();
        }

        private void GrdLinhaClicadaDB()
        {
            tb_usuario objLinhaClicada = (tb_usuario)grdVendedores.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.id_usuario.ToString();
            txtNome.Text = objLinhaClicada.nome_usuario;
            txtEmail.Text = objLinhaClicada.email_usuario;
            mkCpf.Text = objLinhaClicada.cpf_usuario;
            mkTelefone.Text = objLinhaClicada.telefone_usuario;
            chkStatus.Checked = objLinhaClicada.status_usuario;
        }

        private void GrdLinhaClicadaXML()
        {
            var objLinhaClicada = (DataRowView)grdVendedores.CurrentRow.DataBoundItem;

            txtCodigo.Text = objLinhaClicada.Row["id_usuario"].ToString();
            txtNome.Text = objLinhaClicada.Row["nome_usuario"].ToString();
            txtEmail.Text = objLinhaClicada.Row["email_usuario"].ToString();
            mkCpf.Text = objLinhaClicada.Row["cpf_usuario"].ToString();
            mkTelefone.Text = objLinhaClicada.Row["telefone_usuario"].ToString();
            chkStatus.Checked = (bool)objLinhaClicada.Row["status_usuario"];
        }

        private void AlterarVendedor()
        {
            AlterarVendedorDB();
            //AlterarVendedorXML();
        }

        private void AlterarVendedorDB()
        {
            VendedorDAO vendedorDAO = new VendedorDAO();
            tb_usuario objUsuario = new tb_usuario();

            objUsuario.nome_usuario = txtNome.Text.Trim();
            objUsuario.email_usuario = txtEmail.Text.Trim();
            objUsuario.telefone_usuario = mkTelefone.Text.Trim();
            objUsuario.cpf_usuario = mkCpf.Text.Trim();
            objUsuario.tipo_usuario = (int)Util.TipoUsuario.Vendedor;
            objUsuario.status_usuario = chkStatus.Checked;
            objUsuario.id_usuario = Convert.ToInt32(txtCodigo.Text);

            vendedorDAO.UpdateUsuarioDAO(objUsuario);
        }

        private void AlterarVendedorXML() => XmlUtil.AlterarUtilXML(txtCodigo.Text, "id_usuario", "tb_usuario_xml", DicionarioCamposVendedorXML());

        private void ExcluirVendedor()
        {
            ExcluirVendedorDB();
            //ExcluirVendedorXML();
        }

        private void ExcluirVendedorDB() => new VendedorDAO().DeleteUsuarioDAO(Convert.ToInt32(txtCodigo.Text));

        private void ExcluirVendedorXML() => XmlUtil.ExcluirItemXmlUtl(txtCodigo.Text, "id_usuario", "tb_usuario_xml");

        private void FiltrarVendedor()
        {
            FiltrarVendedorDB();
        }

        private void FiltrarVendedorDB()
        {
            string nomeBusca = txtNome.Text.Trim();
            bool status = chkStatusFiltro.Checked;

            Util.CarregarListaUtil(grdVendedores, new List<tb_usuario>(new VendedorDAO().FilterUsuarioDAO(nomeBusca, status, Util.CodigoEmpresaLogado)));
            Util.CarregarRemoverCabecalhoLista(grdVendedores, ListaRemoveColunaGrdVendedores());
            Util.CarregarAlterarCabecalhoLista(grdVendedores, DicionarioAlteraNomeColunaGrdVendedores());
            Util.AplicarMascaraCpfUtil(grdVendedores, "cpf_usuario");
            Util.AplicarMascaraTelefoneCelularUtil(grdVendedores, "telefone_usuario");

        }

        private Dictionary<string, string> DicionarioCamposVendedorXML()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("nome_usuario", txtNome.Text.Trim());
            dicionario.Add("email_usuario", txtEmail.Text.Trim());
            dicionario.Add("cpf_usuario", mkCpf.Text.Trim());
            dicionario.Add("telefone_usuario", mkTelefone.Text.Trim());
            dicionario.Add("senha_usuario", mkCpf.Text.Trim());
            dicionario.Add("status_usuario", chkStatus.Checked.ToString());
            dicionario.Add("id_empresa_usuario", Util.CodigoEmpresaLogado.ToString());
            dicionario.Add("tipo_usuario", Util.TipoUsuario.Vendedor.ToString());

            return dicionario;
        }

        private Dictionary<TextBox, string> DicionarioTextBoxVendedor(Util.CamposObg obg)
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");
            dicionario.Add(txtEmail, "E-mail");

            if (obg == Util.CamposObg.Obrigatorio)
            {
                dicionario.Add(txtCodigo, "Codigo");
            }
            return dicionario;
        }

        private Dictionary<MaskedTextBox, string> DicionarioMaskedTextBoxVendedor()
        {
            Dictionary<MaskedTextBox, string> dicionario = new Dictionary<MaskedTextBox, string>();
            dicionario.Add(mkCpf, "CPF");
            dicionario.Add(mkTelefone, "Telefone");
            return dicionario;
        }

        private Dictionary<CheckBox, string> DicionarioCheckBoxVendedor()
        {
            Dictionary<CheckBox, string> dicionario = new Dictionary<CheckBox, string>();
            dicionario.Add(chkStatus, "Status");
            return dicionario;
        }

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdVendedores()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("id_usuario", "Código");
            dicionario.Add("nome_usuario", "Nome");
            dicionario.Add("email_usuario", "E-mail");
            dicionario.Add("cpf_usuario", "CPF");
            dicionario.Add("telefone_usuario", "Telefone");
            dicionario.Add("status_usuario", "Status");

            return dicionario;
        }

        private List<string> ListaRemoveColunaGrdVendedorXML() => new List<string> { "id_empresa_usuario", "tipo_usuario", "senha_usuario" };

        private List<string> ListaRemoveColunaGrdVendedores() => new List<string> { "id_empresa_usuario", "tipo_usuario", "senha_usuario", "tb_empresa", "tb_veiculo", "tb_cliente", "tb_cor", "tb_marca", "tb_modelo", "tb_opcionais", "tb_tipo_veiculo", "tb_venda" };

        private bool VerificarCamposVendedor() => Util.VerificarCamposUtil(DicionarioTextBoxVendedor(Util.CamposObg.NaoObrigatorio), DicionarioMaskedTextBoxVendedor(), null, DicionarioCheckBoxVendedor());

        private void LimparCamposVendedor() => Util.LimparCamposUtil(DicionarioTextBoxVendedor(Util.CamposObg.Obrigatorio), DicionarioMaskedTextBoxVendedor(), null, DicionarioCheckBoxVendedor());

        #endregion
    }
}
