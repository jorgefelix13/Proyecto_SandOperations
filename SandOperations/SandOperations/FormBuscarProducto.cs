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
    public partial class FormBuscarProducto : Form
    {
        // 1. VARIABLES PÚBLICAS para guardar lo que seleccione el usuario
        public int IdSeleccionado { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public string Stock { get; set; }
        public string Descripcion { get; set; }
        public FormBuscarProducto()
        {
            InitializeComponent();
        }

        private void FormBuscarProducto_Load(object sender, EventArgs e)
        {
            GestorDB db = new GestorDB();
            // Asumiendo que tienes un método que busca por Nombre O Codigo
            dgvProductos.DataSource = db.FiltrarProductos(txtBuscarProducto.Text);
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            GestorDB db = new GestorDB();
            // Asumiendo que tienes un método que busca por Nombre O Codigo
            dgvProductos.DataSource = db.FiltrarProductos(txtBuscarProducto.Text);
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProductos.Rows[e.RowIndex];

                // Guardamos los datos de la fila en las variables públicas
                IdSeleccionado = Convert.ToInt32(fila.Cells["pro_id"].Value);
                Codigo = fila.Cells["pro_codigo"].Value.ToString();
                Nombre = fila.Cells["pro_nombre"].Value.ToString();
                Precio = fila.Cells["pro_pCompra"].Value.ToString();
                Stock = fila.Cells["pro_stock"].Value.ToString();
                Descripcion = fila.Cells["pro_descripcion"].Value.ToString();

                // IMPORTANTE: Esto le dice al formulario principal "Todo salió bien"
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
