#nullable disable
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BibliotecaApp.Models;

namespace BibliotecaApp.Data
{
    public class EditorialDatos
    {
        public List<Editorial> Listar()
        {
            List<Editorial> lista = new List<Editorial>();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT IdEditorial, Nombre, Pais FROM Editoriales", oConexion);
                cmd.CommandType = CommandType.Text;
                oConexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Editorial()
                        {
                            IdEditorial = Convert.ToInt32(dr["IdEditorial"]),
                            Nombre = dr["Nombre"].ToString(),
                            Pais = dr["Pais"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public bool Insertar(Editorial obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Editoriales (Nombre, Pais) VALUES (@Nombre, @Pais)", oConexion);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Pais", obj.Pais ?? (object)DBNull.Value);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Editorial obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Editoriales SET Nombre = @Nombre, Pais = @Pais WHERE IdEditorial = @IdEditorial", oConexion);
                cmd.Parameters.AddWithValue("@IdEditorial", obj.IdEditorial);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Pais", obj.Pais ?? (object)DBNull.Value);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int idEditorial)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Editoriales WHERE IdEditorial = @IdEditorial", oConexion);
                cmd.Parameters.AddWithValue("@IdEditorial", idEditorial);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}