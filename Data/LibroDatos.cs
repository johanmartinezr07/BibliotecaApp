#nullable disable
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BibliotecaApp.Models;

namespace BibliotecaApp.Data
{
    public class LibroDatos
    {
        public List<Libro> Listar()
        {
            List<Libro> lista = new List<Libro>();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                string query = @"SELECT l.IdLibro, l.Titulo, l.IdAutor, l.IdEditorial, l.AnioPublicacion, l.Stock,
                                        (a.Nombre + ' ' + a.Apellido) AS NombreAutor, e.Nombre AS NombreEditorial
                                 FROM Libros l
                                 INNER JOIN Autores a ON l.IdAutor = a.IdAutor
                                 INNER JOIN Editoriales e ON l.IdEditorial = e.IdEditorial";

                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.CommandType = CommandType.Text;
                oConexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Libro()
                        {
                            IdLibro = Convert.ToInt32(dr["IdLibro"]),
                            Titulo = dr["Titulo"].ToString(),
                            IdAutor = Convert.ToInt32(dr["IdAutor"]),
                            IdEditorial = Convert.ToInt32(dr["IdEditorial"]),
                            AnioPublicacion = Convert.ToInt32(dr["AnioPublicacion"]),
                            Stock = Convert.ToInt32(dr["Stock"]),
                            NombreAutor = dr["NombreAutor"].ToString(),
                            NombreEditorial = dr["NombreEditorial"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public bool Insertar(Libro obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Libros (Titulo, IdAutor, IdEditorial, AnioPublicacion, Stock) VALUES (@Titulo, @IdAutor, @IdEditorial, @AnioPublicacion, @Stock)", oConexion);
                cmd.Parameters.AddWithValue("@Titulo", obj.Titulo);
                cmd.Parameters.AddWithValue("@IdAutor", obj.IdAutor);
                cmd.Parameters.AddWithValue("@IdEditorial", obj.IdEditorial);
                cmd.Parameters.AddWithValue("@AnioPublicacion", obj.AnioPublicacion);
                cmd.Parameters.AddWithValue("@Stock", obj.Stock);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Libro obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Libros SET Titulo = @Titulo, IdAutor = @IdAutor, IdEditorial = @IdEditorial, AnioPublicacion = @AnioPublicacion, Stock = @Stock WHERE IdLibro = @IdLibro", oConexion);
                cmd.Parameters.AddWithValue("@IdLibro", obj.IdLibro);
                cmd.Parameters.AddWithValue("@Titulo", obj.Titulo);
                cmd.Parameters.AddWithValue("@IdAutor", obj.IdAutor);
                cmd.Parameters.AddWithValue("@IdEditorial", obj.IdEditorial);
                cmd.Parameters.AddWithValue("@AnioPublicacion", obj.AnioPublicacion);
                cmd.Parameters.AddWithValue("@Stock", obj.Stock);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int idLibro)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Libros WHERE IdLibro = @IdLibro", oConexion);
                cmd.Parameters.AddWithValue("@IdLibro", idLibro);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}