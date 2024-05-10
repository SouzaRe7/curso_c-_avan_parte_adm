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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void coresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmCor().ShowDialog();
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmMarca().ShowDialog();
        }

        private void modelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmModelo().ShowDialog();
        }

        private void opcionaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmOpcionaisVeiculo().ShowDialog();
        }

        private void gerenciarVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmVendedor().ShowDialog();
        }

        private void gerenciarVeículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmVeiculo().ShowDialog();
        }

        private void vendasVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAcompanhaVendaVendedor().ShowDialog();
        }

        private void vendasVeículoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAcompanhaVendaVeiculo().ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
