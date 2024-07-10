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
    public partial class frmModelo : Form
    {
        private ModeloDAO modeloDAO;
        private MarcaDAO marcaDAO;

        public frmModelo()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
            modeloDAO = new ModeloDAO();
            marcaDAO = new MarcaDAO();
        }
        #region Eventos
        private void frmModelo_Load(object sender, EventArgs e)
        {
            Util.ConfiguraDataGridViewUtil(grdModelos);
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            CarregarModelos();
            CarrecarMarcas();
        }

        private void btnCadastar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposModelo())
            {
                try
                {
                    CadastrarModelo();
                    Util.MsgSucessoUtil();
                    CarregarModelos();
                    LimparCamposModelo();
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposModelo();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
            LimparCamposModelo();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (VerificarCamposModelo())
            {
                try
                {
                    AlterarModelo();
                    Util.MsgSucessoUtil();
                    CarregarModelos();
                    LimparCamposModelo();
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposModelo();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Util.MsgExcluirItemUtil(txtNome.Text+"/"+cbMarca.Text))
            {
                try
                {
                    ExcluirModelo();
                    Util.MsgSucessoUtil();
                    CarregarModelos();
                    LimparCamposModelo();
                    Util.ControleTela(Util.AcaoBotao.Novo, btnCadastar, btnAlterar, btnExcluir);
                }
                catch
                {
                    Util.MsgErroUtil();
                    LimparCamposModelo();
                }
            }
        }

        private void grdModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdModelos.RowCount > 0)
            {
                CarregaLinhaClicada();
                Util.ControleTela(Util.AcaoBotao.Editar, btnCadastar, btnAlterar, btnExcluir);
            }
        }
        #endregion

        #region Funções

        private void CadastrarModelo()
        {
            CadastrarModeloDB();
        }

        private void CadastrarModeloDB()
        {
            modeloDAO.InsertModeloDAO(new tb_modelo
            {
                nome_modelo = txtNome.Text.Trim(),
                id_marca_modelo = Convert.ToInt32(cbMarca.SelectedValue),
                id_empresa_modelo = Util.CodigoEmpresaLogado,
                id_usuario_modelo = Util.CodigoUsuarioLogado,
            });
        }

        private void CarregarModelos()
        {
            CarregarModelosDB();
        }

        private void CarregarModelosDB()
        {
            Util.CarregarListaUtil(grdModelos, new List<ModeloMarcaVO>(modeloDAO.SelectModeloDAO(Util.CodigoEmpresaLogado)));
            Util.CarregarRemoverCabecalhoLista(grdModelos, ListaRemoveColunaGrdModelos());
            Util.CarregarAlterarCabecalhoLista(grdModelos, DicionarioAlteraNomeColunaGrdModelos());
        }

        private void CarregaLinhaClicada()
        {
            CarregaLinhaClicadaDB();
        }

        private void CarregaLinhaClicadaDB()
        {
            ModeloMarcaVO vo = (ModeloMarcaVO)grdModelos.CurrentRow.DataBoundItem;

            tb_modelo objLinhaClicada = vo.ObjModeloVO;

            txtCodigo.Text = objLinhaClicada.id_modelo.ToString();
            txtNome.Text = objLinhaClicada.nome_modelo;
            cbMarca.SelectedIndex = -1;
            cbMarca.SelectedValue = objLinhaClicada.id_marca_modelo;
        }

        private void AlterarModelo()
        {
            AlterarModeloDB();
        }

        private void AlterarModeloDB()
        {
            modeloDAO.UpdateModeloDAO(new tb_modelo
            {
                nome_modelo = txtNome.Text.Trim(),
                id_marca_modelo = Convert.ToInt32(cbMarca.SelectedValue),
                id_modelo = Convert.ToInt32(txtCodigo.Text)
            });
        }

        private void ExcluirModelo()
        {
            ExcluirModeloDB();
        }

        private void ExcluirModeloDB() => modeloDAO.DeleteModeloDAO(Convert.ToInt32(txtCodigo.Text));

        private Dictionary<TextBox, string> DicionarioTextBoxModelo(Util.CamposObg obg)
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();
            dicionario.Add(txtNome, "Nome");

            if (obg == Util.CamposObg.Obrigatorio)
            {
                dicionario.Add(txtCodigo, "Codigo");
            }
            return dicionario;
        }

        private Dictionary<ComboBox, string> DicionarioComboBoxModelo()
        {
            Dictionary<ComboBox, string> dicionario = new Dictionary<ComboBox, string>();
            dicionario.Add(cbMarca, "Marca");
            return dicionario;
        }

        private Dictionary<string, string> DicionarioAlteraNomeColunaGrdModelos()
        {
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("Codigo", "Código");

            return dicionario;
        }

        private void CarrecarMarcas()
        {
            List<tb_marca> lstMarcas = marcaDAO.SelectMarcaDAO(Util.CodigoEmpresaLogado);
            cbMarca.DisplayMember = "nome_marca";
            cbMarca.ValueMember = "id_marca";
            cbMarca.DataSource = lstMarcas;
            cbMarca.SelectedIndex = -1;
        }
        private List<string> ListaRemoveColunaGrdModelos() => new List<string> { "IdMarca", "ObjModeloVO" };

        private bool VerificarCamposModelo() => Util.VerificarCamposUtil(DicionarioTextBoxModelo(Util.CamposObg.NaoObrigatorio), null, DicionarioComboBoxModelo());

        private void LimparCamposModelo() => Util.LimparCamposUtil(DicionarioTextBoxModelo(Util.CamposObg.Obrigatorio), null, DicionarioComboBoxModelo());

        #endregion
    }
}
