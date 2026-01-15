using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SandOperations
{
    public class GestorDB
    {
        
        Conexion con = new Conexion();

        //esta parte son los metodos para usuarios
        //metodo para el login 
        public string Login(string usuario, string password)
        {
            string rol = null;
            try
            {
                using (SqlConnection conexion = con.Conectar())
                {
                    conexion.Open();

                    string query = "select top 1 usu_rol from usuarios where usu_usuario = @usuario and usu_password = @password and usu_activo = 1";

                    SqlCommand command = new SqlCommand(query, conexion);

                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@password", password);


                    // 1. Ejecutamos y guardamos el objeto (puede ser null si no encuentra al usuario)
                    object resultado = command.ExecuteScalar();

                    // 2. Verificamos si trajo algo
                    if (resultado != null)
                    {
                        rol = resultado.ToString(); // Asignamos el valor a tu variable   
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }

            return rol;
        }

        //Metodo para registrar un usuario
        public void Registrar(string usuario, string password, string rol)
        {
            try
            {
                using (SqlConnection conexion = con.Conectar())
                {
                    conexion.Open();

                    string query = "insert into usuarios(usu_usuario,usu_password,usu_rol) values(@usuario,@password,@rol)";

                    SqlCommand command = new SqlCommand(query, conexion);

                    command.Parameters.AddWithValue("@usuario",usuario);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@rol", rol);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Usuario Registrado Correctamente");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:"+ex.Message);
            }
        }

        //Metodo para actualizar usuario
        public void Actualizar(int id, string usuario, string password, string rol, bool status)
        {
            try
            {
                using (SqlConnection conexion = con.Conectar())
                {
                    conexion.Open();

                    string query = "update usuarios set usu_usuario = @usuario, usu_password = @password, usu_rol = @rol, usu_activo = @status where usu_id = @id";

                    SqlCommand command = new SqlCommand(query, conexion);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@rol", rol);
                    command.Parameters.AddWithValue("@status", status);

                    command.ExecuteNonQuery();

                    MessageBox.Show("El usuario se actualizo correctamente");
                }
            }
            catch (SqlException ex)
            {
                // El error 2627 es "Violation of UNIQUE KEY" (Nombre repetido)
                if (ex.Number == 2627)
                {
                    MessageBox.Show("Ese nombre de usuario ya existe, elige otro.");
                }
                else
                {
                    MessageBox.Show("Error SQL: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        //Metodo para mostrar datos al datagritview
        public DataTable ObtenerUsuarios(string busqueda = "")
        {
            DataTable tabla = new DataTable();
            try
            {
                using (SqlConnection conexion = con.Conectar())
                {
                    conexion.Open();
                    // Usamos LIKE para buscar coincidencias parciales
                    // El '%' sirve para decir "lo que sea que siga"
                    string query = "SELECT * FROM usuarios WHERE usu_usuario LIKE @busqueda + '%'";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.Parameters.AddWithValue("@busqueda", busqueda);

                    // El SqlDataAdapter es como un camión que llena la tabla de golpe
                    SqlDataAdapter adaptador = new SqlDataAdapter(command);
                    adaptador.Fill(tabla);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
            return tabla;
        }

        //esta parte son los metodos para los productos
        //Metodo para registrar producto
        public void RegistrarProducto(string codigo, string nombre, decimal precioCompra, int stock, string descripcion)
        {
            try
            {
                using (SqlConnection conexion = con.Conectar())
                {
                    conexion.Open();

                    string query = "insert into productos(pro_codigo, pro_nombre, pro_pCompra, pro_stock, pro_descripcion) values(@codigo, @nombre, @precioCompra, @stock, @descripcion)";

                    SqlCommand command = new SqlCommand(@query, conexion);

                    command.Parameters.AddWithValue("@codigo", codigo);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@precioCompra", precioCompra);
                    command.Parameters.AddWithValue("@stock", stock);
                    command.Parameters.AddWithValue("@descripcion", descripcion);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Producto registrado correctamente");
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error:"+ex.Message);
            }
        }

        //Metodo para actualizar producto
        public void ActualizarProducto(int id, string codigo, string nombre, decimal precioCompra, int stock, string descripcion)
        {
            try
            {
                using (SqlConnection conexion = con.Conectar())
                {
                    conexion.Open();

                    string query = "update productos set pro_codigo = @codigo, pro_nombre = @nombre, pro_pCompra = @precioCompra, pro_stock = @stock, pro_descripcion = @descripcion where pro_id = @id";

                    SqlCommand command = new SqlCommand(query, conexion);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@codigo", codigo);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@precioCompra", precioCompra);
                    command.Parameters.AddWithValue("@stock", stock);
                    command.Parameters.AddWithValue("@descripcion", descripcion);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Producto registrado correctamente");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("El producto no se registro correctamente");
                MessageBox.Show("Error:"+ex.Message);
            }
        }
        //metodo para traer los datos del producto por el codigo
        public DataTable TraerProductoPorCodigo(string codigo)
        {
            DataTable tabla = new DataTable();
            try
            {
                using (SqlConnection conexion = con.Conectar())
                {
                    conexion.Open();
                    // Buscamos todos los datos donde el código coincida
                    string query = "SELECT * FROM productos WHERE pro_codigo = @codigo";

                    SqlCommand command = new SqlCommand(query, conexion);
                    command.Parameters.AddWithValue("@codigo", codigo);

                    SqlDataAdapter adaptador = new SqlDataAdapter(command);
                    adaptador.Fill(tabla);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar producto: " + ex.Message);
            }
            return tabla;
        }
    }
}
