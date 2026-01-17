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
    public partial class FormProducto : Form
    {
        // Variable para guardar el ID del producto que estamos editando
        // Si es 0, significa que es nuevo. Si es > 0, es edición.
        int idProductoEnEdicion = 0;
        public FormProducto()
        {
            InitializeComponent();
        }
        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtPrecioCompra.Clear();
            txtStock.Clear();
            txtDescripcion.Clear();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            // 1. Creamos la instancia del formulario de búsqueda
            FormBuscarProducto formBusqueda = new FormBuscarProducto();

            // 2. Lo abrimos como DIÁLOGO (Pausa este formulario hasta que cierres el otro)
            DialogResult respuesta = formBusqueda.ShowDialog();

            // 3. Si el usuario seleccionó algo (dio doble click)
            if (respuesta == DialogResult.OK)
            {
                // Recuperamos los datos de las variables públicas
                idProductoEnEdicion = formBusqueda.IdSeleccionado; // GUARDAMOS EL ID

                txtCodigo.Text = formBusqueda.Codigo;
                txtNombre.Text = formBusqueda.Nombre;
                txtPrecioCompra.Text = formBusqueda.Precio;
                txtStock.Text = formBusqueda.Stock;
                txtDescripcion.Text = formBusqueda.Descripcion;

                // Opcional: Cambiar botón a "Modificar"
                // btnRegistrar.Text = "Modificar";
            }
        }

       
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // 1. Validar campos vacíos
            if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El código y el nombre son obligatorios.");
                txtCodigo.Focus();
                return;
            }

            // 2. Validar números de forma segura
            decimal precioCompra;
            if (!decimal.TryParse(txtPrecioCompra.Text, out precioCompra))
            {
                MessageBox.Show("El precio debe ser un número válido.");
                txtCodigo.Focus();
                return;
            }

            int stock;
            if (!int.TryParse(txtStock.Text, out stock))
            {
                // Si no pone stock, asumimos 0 o le avisamos
                stock = 0;
                
                // O mandas error: MessageBox.Show("Stock inválido"); return;
            }

            // 3. Si todo está bien, guardamos
            GestorDB bd = new GestorDB();
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;

            bd.RegistrarProducto(codigo, nombre, precioCompra, stock, descripcion);

            LimpiarCampos();

            // TRUCO: Regresar el cursor al código para registrar el siguiente producto rápido
            txtCodigo.Focus();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {

            // El caracter 13 es la tecla ENTER (lo que manda la pistola al final)
            if (e.KeyChar == (char)13)
            {
                e.Handled = true; // Evita el sonido "ding" de Windows

                GestorDB db = new GestorDB();

                // 1. Buscamos el producto en la base de datos
                DataTable datos = db.FiltrarProductos(txtCodigo.Text);

                // 2. Verificamos si encontró algo (Si tiene filas, es que YA EXISTE)
                if (datos.Rows.Count > 0)
                {
                    // -- CASO: EL PRODUCTO YA EXISTE --
                    // Llenamos las cajas con la info de la base de datos
                    idProductoEnEdicion =Convert.ToInt32(datos.Rows[0]["pro_id"].ToString());
                    txtNombre.Text = datos.Rows[0]["pro_nombre"].ToString();
                    txtPrecioCompra.Text = datos.Rows[0]["pro_pCompra"].ToString();
                    txtStock.Text = datos.Rows[0]["pro_stock"].ToString();
                    txtDescripcion.Text = datos.Rows[0]["pro_descripcion"].ToString();

                    // Opcional: Avisar o bloquear el botón de registrar para no duplicar
                    //MessageBox.Show("Este producto ya está registrado.");
                }
                else
                {
                    // -- CASO: EL PRODUCTO ES NUEVO --
                    // Limpiamos las cajas (menos el código) para escribir los datos nuevos
                    txtNombre.Clear();
                    txtPrecioCompra.Clear();
                    txtStock.Clear(); // O puedes poner "0"
                    txtDescripcion.Clear();

                    // Ponemos el foco en Nombre para empezar a escribir rápido
                    txtNombre.Focus();
                }
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // 1. Validar campos vacíos
            if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El código y el nombre son obligatorios.");
                txtCodigo.Focus();
                return;
            }

            // 2. Validar números de forma segura
            decimal precioCompra;
            if (!decimal.TryParse(txtPrecioCompra.Text, out precioCompra))
            {
                MessageBox.Show("El precio debe ser un número válido.");
                txtCodigo.Focus();
                return;
            }

            GestorDB bd = new GestorDB();

            //int id = Convert.ToInt32(textBox1.Text);
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            int stock = Convert.ToInt32(txtStock.Text);
            string descripcion = txtDescripcion.Text;

            bd.ActualizarProducto(idProductoEnEdicion,codigo,nombre,precioCompra,stock,descripcion);

            LimpiarCampos();
        }
    }
}
