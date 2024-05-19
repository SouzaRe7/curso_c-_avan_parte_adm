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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            Util.ConfigureFormBorderStyle(this);
        }
        #region Eventos
        private void btnAcesso_Click(object sender, EventArgs e)
        {
            if (VerificaCamposLogin())
            {
                try
                {

                }
                catch
                {
                    Util.MsgErroUtil();
                }
            }
        }
        #endregion

        #region Funções
        /// <summary>
        /// Função dicionario do TextBox da tela Login
        /// </summary>
        /// <returns>Dictionary</returns>
        private Dictionary<TextBox, string> DicionarioTextBoxLogin()
        {
            Dictionary<TextBox, string> dicionario = new Dictionary<TextBox, string>();

            dicionario.Add(txtLogin, "Login");
            dicionario.Add(txtSenha, "Senha");

            return dicionario;
        }

        private bool VerificaCamposLogin()=> Util.VerificarCamposUtil(DicionarioTextBoxLogin());
      
        #endregion
    }
}
