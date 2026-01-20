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
    public partial class FormSalidas : Form
    {
        int idProductoEnEdicion = 0;
        public FormSalidas()
        {
            InitializeComponent();
            //txtFecha.Text = DateTime.Now.ToString();
            txtNombre.Enabled = false;  
            txtStock.Enabled = false;
        }

        private void FormSalidas_Load(object sender, EventArgs e)
        {
            // Limpiamos por si acaso
            dataGridView1.Columns.Clear();

            // Agregamos las columnas (NombreInterno, TextoTitulo)
            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns.Add("codigo", "Código");
            dataGridView1.Columns.Add("nombre", "Nombre");
            dataGridView1.Columns.Add("cantidad", "Cantidad");
            dataGridView1.Columns.Add("destino", "Destino"); // Para Mojada/Seca
            dataGridView1.Columns.Add("trabajador", "Trabajador");

            // Ocultamos la columna ID para que no estorbe
            dataGridView1.Columns["id"].Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Verificamos que haya cosas que guardar
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos en la lista.");
                return;
            }

            GestorDB db = new GestorDB();
            int contados = 0;

            try
            {
                // --- EL CICLO (FOREACH) ---
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    // Validamos que no sea la fila vacía del final (si existe)
                    if (fila.Cells["id"].Value != null)
                    {
                        // 1. Sacamos los datos de la celda
                        int idProd = Convert.ToInt32(fila.Cells["id"].Value);
                        int cant = Convert.ToInt32(fila.Cells["cantidad"].Value);
                        string dest = fila.Cells["destino"].Value.ToString();
                        string trabajador = fila.Cells["trabajador"].Value.ToString();

                        // 2. Mandamos a guardar a la BD
                        db.RegistrarSalida(idProd, cant, dest, trabajador);

                        contados++;
                    }
                }

                // Si llegó hasta aquí, todo salió bien
                MessageBox.Show($"Se registraron {contados} salidad correctamente.\nEl inventario se actualizó automáticamente.");

                // Limpiamos el grid para empezar de nuevo
                dataGridView1.Rows.Clear();
                // 5. Limpiar casillas para el siguiente (pero dejamos el destino)
                txtCantidad.Clear();
                txtCodigo.Clear();
                txtNombre.Clear();
                txtStock.Clear();
                txtTrabajador.Clear();
                idProductoEnEdicion = 0; // Opcional: si quieres obligar a escanear de nuevo
                txtCodigo.Focus(); // Listo para el siguiente

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar: " + ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // 1. Validaciones básicas
            if (idProductoEnEdicion == 0)
            {
                MessageBox.Show("Primero busca o escanea un producto.");
                return;
            }

            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Falta cantidad");
                return;
            }

            // 2. Obtener valores numéricos
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            string trabajador = txtTrabajador.Text;

            // 3. Obtener el destino de los RadioButtons
            string destino = "";
            if (rdbPlantaMojada.Checked) destino = "Planta Mojada";
            else if (rdbPlantaSeca.Checked) destino = "Planta Seca";
            else destino = "General"; // Por si no seleccionó ninguno

            // 4. AGREGAR AL GRID
            // El orden debe ser igual al de las columnas que creamos arriba
            dataGridView1.Rows.Add(idProductoEnEdicion, txtCodigo.Text, txtNombre.Text, cantidad, destino, trabajador);

            // 5. Limpiar casillas para el siguiente (pero dejamos el destino)
            txtCantidad.Clear();
            txtCodigo.Clear();
            txtNombre.Clear();
            txtStock.Clear();
            txtTrabajador.Clear();
            // idProductoSeleccionado = 0; // Opcional: si quieres obligar a escanear de nuevo
            txtCodigo.Focus(); // Listo para el siguiente
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
                    idProductoEnEdicion = Convert.ToInt32(datos.Rows[0]["pro_id"].ToString());
                    txtNombre.Text = datos.Rows[0]["pro_nombre"].ToString();
                    txtStock.Text = datos.Rows[0]["pro_stock"].ToString();

                    // Opcional: Avisar o bloquear el botón de registrar para no duplicar
                    //MessageBox.Show("Este producto ya está registrado.");
                }
                else
                {
                    // -- CASO: EL PRODUCTO ES NUEVO --
                    // Limpiamos las cajas (menos el código) para escribir los datos nuevos
                    txtNombre.Clear();
                    txtStock.Clear(); // O puedes poner "0"

                    // Ponemos el foco en Nombre para empezar a escribir rápido
                    txtNombre.Focus();
                }
            }
        }
    }
}
