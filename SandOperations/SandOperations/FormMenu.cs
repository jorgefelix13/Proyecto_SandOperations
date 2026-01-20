using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SandOperations
{
    public partial class FormMenu : Form
    {
        string rol;
        public FormMenu()  
        {
            InitializeComponent();
            
        }

        public FormMenu(string rol)
        {
            InitializeComponent();
            this.rol = rol;

            if (rol == "administrador")
            {
               // textBox1.Visible = false; // Ocultamos el control
            }
            else
            {
                //textBox1.Visible = true;
                uSUARIOSToolStripMenuItem.Enabled = false;
            }
        }

        private void sALIDASToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rEGISTRARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsuario formUsuario = new FormUsuario();
            
            formUsuario.ShowDialog();
        }

        private void rEGISTRARPRODUCTOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProducto formProducto = new FormProducto();

            formProducto.ShowDialog();
        }

        private void rEGISTRARENTRADAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEntradas formEntradas = new FormEntradas();

            formEntradas.ShowDialog();
        }

        private void rEGISTRARSALIDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSalidas formSalidas = new FormSalidas();

            formSalidas.ShowDialog();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
