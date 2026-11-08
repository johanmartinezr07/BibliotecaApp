using Microsoft.Data.SqlClient;

namespace BibliotecaApp.Data
{
    public class Conexion
    {
        private static string cadenaConexion = @"Server=.\SQLEXPRESS;Database=BibliotecaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}