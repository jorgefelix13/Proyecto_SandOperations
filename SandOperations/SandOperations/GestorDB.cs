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
    }
}
