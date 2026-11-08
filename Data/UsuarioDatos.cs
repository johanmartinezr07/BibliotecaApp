#nullable disable
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BibliotecaApp.Models;

namespace BibliotecaApp.Data
{
    public class UsuarioDatos
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT IdUsuario, Nombre, Apellido, Documento, Telefono, Correo FROM Usuarios", oConexion);
                cmd.CommandType = CommandType.Text;
                oConexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            Documento = dr["Documento"].ToString(),
                            Telefono = dr["Telefono"]?.ToString() ?? "",
                            Correo = dr["Correo"]?.ToString() ?? ""
                        });
                    }
                }
            }
            return lista;
        }

        public bool Insertar(Usuario obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (Nombre, Apellido, Documento, Telefono, Correo) VALUES (@Nombre, @Apellido, @Documento, @Telefono, @Correo)", oConexion);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                cmd.Parameters.AddWithValue("@Documento", obj.Documento);
                cmd.Parameters.AddWithValue("@Telefono", obj.Telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Correo", obj.Correo ?? (object)DBNull.Value);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Actualizar(Usuario obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, Documento = @Documento, Telefono = @Telefono, Correo = @Correo WHERE IdUsuario = @IdUsuario", oConexion);
                cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                cmd.Parameters.AddWithValue("@Documento", obj.Documento);
                cmd.Parameters.AddWithValue("@Telefono", obj.Telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Correo", obj.Correo ?? (object)DBNull.Value);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int idUsuario)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario", oConexion);
                cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                cmd.CommandType = CommandType.Text;

                oConexion.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}