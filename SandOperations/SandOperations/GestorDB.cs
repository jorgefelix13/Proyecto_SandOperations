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

        //metodo para el login 
        public string Login(string usuario, string password)
        {
            string rol = null;
            try
            {
                using (SqlConnection conexion = con.Conectar())
                {
                    conexion.Open();

                    string query = "select top 1 usu_rol from usuarios where usu_usuario = @usuario and usu_password = @password ";

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
    }
}
