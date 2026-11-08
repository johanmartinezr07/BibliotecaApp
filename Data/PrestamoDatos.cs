#nullable disable
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BibliotecaApp.Models;

namespace BibliotecaApp.Data
{
    public class PrestamoDatos
    {
        public List<Prestamo> Listar()
        {
            List<Prestamo> lista = new List<Prestamo>();
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                string query = @"SELECT p.IdPrestamo, p.IdLibro, p.IdUsuario, p.FechaPrestamo, p.FechaDevolucion, p.Estado,
                                        l.Titulo AS TituloLibro, (u.Nombre + ' ' + u.Apellido) AS NombreUsuario
                                 FROM Prestamos p
                                 INNER JOIN Libros l ON p.IdLibro = l.IdLibro
                                 INNER JOIN Usuarios u ON p.IdUsuario = u.IdUsuario";

                SqlCommand cmd = new SqlCommand(query, oConexion);
                cmd.CommandType = CommandType.Text;
                oConexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Prestamo()
                        {
                            IdPrestamo = Convert.ToInt32(dr["IdPrestamo"]),
                            IdLibro = Convert.ToInt32(dr["IdLibro"]),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            FechaPrestamo = Convert.ToDateTime(dr["FechaPrestamo"]),
                            FechaDevolucion = dr["FechaDevolucion"] != DBNull.Value ? Convert.ToDateTime(dr["FechaDevolucion"]) : (DateTime?)null,
                            Estado = dr["Estado"].ToString(),
                            TituloLibro = dr["TituloLibro"].ToString(),
                            NombreUsuario = dr["NombreUsuario"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public bool RegistrarPrestamo(Prestamo obj)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                SqlTransaction trans = oConexion.BeginTransaction();

                try
                {
                    // 1. Insertar el préstamo
                    string queryInsert = "INSERT INTO Prestamos (IdLibro, IdUsuario, FechaPrestamo, Estado) VALUES (@IdLibro, @IdUsuario, @FechaPrestamo, 'Prestado')";
                    SqlCommand cmdInsert = new SqlCommand(queryInsert, oConexion, trans);
                    cmdInsert.Parameters.AddWithValue("@IdLibro", obj.IdLibro);
                    cmdInsert.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmdInsert.Parameters.AddWithValue("@FechaPrestamo", obj.FechaPrestamo);
                    cmdInsert.ExecuteNonQuery();

                    // 2. Reducir stock del libro
                    string queryUpdate = "UPDATE Libros SET Stock = Stock - 1 WHERE IdLibro = @IdLibro AND Stock > 0";
                    SqlCommand cmdUpdate = new SqlCommand(queryUpdate, oConexion, trans);
                    cmdUpdate.Parameters.AddWithValue("@IdLibro", obj.IdLibro);
                    int filasAfectadas = cmdUpdate.ExecuteNonQuery();

                    if (filasAfectadas == 0)
                    {
                        trans.Rollback();
                        throw new Exception("El libro no tiene stock disponible.");
                    }

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public bool RegistrarDevolucion(int idPrestamo, int idLibro)
        {
            using (SqlConnection oConexion = Conexion.ObtenerConexion())
            {
                oConexion.Open();
                SqlTransaction trans = oConexion.BeginTransaction();

                try
                {
                    // 1. Marcar préstamo como Devuelto y registrar la fecha de devolución
                    string queryUpdatePrestamo = "UPDATE Prestamos SET FechaDevolucion = GETDATE(), Estado = 'Devuelto' WHERE IdPrestamo = @IdPrestamo AND Estado = 'Prestado'";
                    SqlCommand cmdPrestamo = new SqlCommand(queryUpdatePrestamo, oConexion, trans);
                    cmdPrestamo.Parameters.AddWithValue("@IdPrestamo", idPrestamo);
                    int filasAfectadas = cmdPrestamo.ExecuteNonQuery();

                    if (filasAfectadas == 0)
                    {
                        trans.Rollback();
                        return false;
                    }

                    // 2. Incrementar stock del libro devuelto
                    string queryUpdateStock = "UPDATE Libros SET Stock = Stock + 1 WHERE IdLibro = @IdLibro";
                    SqlCommand cmdStock = new SqlCommand(queryUpdateStock, oConexion, trans);
                    cmdStock.Parameters.AddWithValue("@IdLibro", idLibro);
                    cmdStock.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
    }
}