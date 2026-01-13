using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace SandOperations
{
    class Conexion
    {

        // Ahora leemos la cadena desde el archivo App.config
        private readonly string cadenaConexion =
            ConfigurationManager.ConnectionStrings["CadenaSandOperations"].ConnectionString;

        public SqlConnection Conectar()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
