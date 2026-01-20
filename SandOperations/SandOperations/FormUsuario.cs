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
        // Variable global para recordar a quién le diste clic
        int idSeleccionado = 0;
        public FormUsuario()
        {
            InitializeComponent();

        }
        // 1. Método para refrescar el Grid
        private void CargarTabla()
        {
            GestorDB db = new GestorDB();
            // Le pasamos lo que haya en la caja de buscar (txtBuscar)
            dgvUsuarios.DataSource = db.ObtenerUsuarios(txtBuscar.Text);

            // Opcional: Ocultar columnas que no quieras ver (como el password o ID)
            // dgvUsuarios.Columns["usu_password"].Visible = false;
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

            db.Registrar(usuario, password, rol);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Por favor selecciona un usuario de la tabla primero.");
                return;
            }

            GestorDB db = new GestorDB();

            //int id = Convert.ToInt32(txtId.Text);
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

            db.Actualizar(idSeleccionado, usuario, password, rol, status);

            // Limpiamos y recargamos la tabla para ver los cambios
            CargarTabla();
            //LimpiarFormulario();(Si tienes un método para limpiar cajas)
            txtUsuario.Clear();
            txtPassword.Clear();
            txtBuscar.Clear();
            idSeleccionado = 0; // Reseteamos el ID
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Validamos que no se haya dado clic en el encabezado (-1)
                if (e.RowIndex >= 0)
                {
                    // Obtenemos la fila actual donde se hizo clic
                    DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                    // 1. Guardamos el ID en nuestra variable global (IMPORTANTE para actualizar)
                    idSeleccionado = Convert.ToInt32(fila.Cells["usu_id"].Value);

                    // 2. Llenamos las cajas de texto
                    txtUsuario.Text = fila.Cells["usu_usuario"].Value.ToString();
                    txtPassword.Text = fila.Cells["usu_password"].Value.ToString();

                    // 3. Seleccionar el ROL correcto (RadioButtons)
                    string rol = fila.Cells["usu_rol"].Value.ToString();
                    if (rol == "administrador")
                    {
                        rdbAdministrador.Checked = true;
                    }
                    else
                    {
                        rdbEmpleado.Checked = true;
                    }

                    // 4. Seleccionar el ESTADO correcto (Activo/Inactivo)
                    // Convertimos el valor de la celda (que viene como bool o bit)
                    bool activo = Convert.ToBoolean(fila.Cells["usu_activo"].Value);
                    if (activo)
                    {
                        rdbActivo.Checked = true;
                    }
                    else
                    {
                        rdbInactivo.Checked = true;
                    }

                    // Opcional: Cambiar el texto del botón de Guardar a "Modificar" visualmente
                    // btnGuardar.Enabled = false;
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error no hay usuario seleccionado");
            }
        }
    }
}
