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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            GestorDB db = new GestorDB();

            

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {   
                MessageBox.Show("El campo no puede estar vacío");
                return;
            }

         
            string usuario = txtUser.Text;
            string password = txtPassword.Text;

            string rol = db.Login(usuario,password);

            if(rol != null)
            {
                MessageBox.Show("Bienvenido Rol:"+ rol);
                FormMenu formMenu = new FormMenu(rol);
                this.Hide(); // Ocultamos el login
                formMenu.ShowDialog();
                this.Close(); // Cerramos el login al volver
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }
    }
}
