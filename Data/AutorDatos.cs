#nullable disable
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BibliotecaApp.Models;

namespace BibliotecaApp.Data
{
    public class AutorDatos
    {
        public List<Autor> Listar()
        {
            List<Autor> lista = new List<Autor>();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT IdAutor, Nombre, Apellido FROM Autores", oConexion);
                cmd.CommandType = CommandType.Text;
                oConexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Autor()
                        {
                            IdAutor = Convert.ToInt32(dr["IdAutor"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public bool Insertar(Autor obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Autores (Nombre, Apellido) VALUES (@Nombre, @Apellido)", oConexion);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Autor obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Autores SET Nombre = @Nombre, Apellido = @Apellido WHERE IdAutor = @IdAutor", oConexion);
                cmd.Parameters.AddWithValue("@IdAutor", obj.IdAutor);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int idAutor)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Autores WHERE IdAutor = @IdAutor", oConexion);
                cmd.Parameters.AddWithValue("@IdAutor", idAutor);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}