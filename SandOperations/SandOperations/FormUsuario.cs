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
    public partial class FormUsuario : Form
    {
        public FormUsuario()
        {
            InitializeComponent();
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GestorDB db = new GestorDB();

            string usuario = txtUsuario.Text;
            string password = txtPassword.Text;
            
            string rol = null;
            

            if (rdbAdministrador.Checked)
            {
                rol = "administrador";
            }
            else
            {
                rol = "empleado";
            }

            db.Registrar(usuario,password,rol);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            GestorDB db = new GestorDB();

            int id = Convert.ToInt32(txtId.Text);
            string usuario = txtUsuario.Text;
            string password = txtPassword.Text;

            string rol = null;
            bool status;

            if (rdbAdministrador.Checked)
            {
                rol = "administrador";
            }
            else
            {
                rol = "empleado";
            }

            if (rdbActivo.Checked)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            db.Actualizar(id,usuario,password,rol,status);
        }
    }
}
