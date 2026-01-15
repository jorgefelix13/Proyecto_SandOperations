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
          
            // El caracter 13 es la tecla ENTER
            if (e.KeyChar == (char)13)
            {
                e.Handled = true; // Evita el sonido de "ding" de Windows
                txtNombre.Focus(); // Salta a la siguiente caja automáticamente
            }
        
        }
    }
}
