#nullable disable
using System;

namespace BibliotecaApp.Models
{
    public class Prestamo
    {
        public int IdPrestamo { get; set; }
        public int IdLibro { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public string Estado { get; set; } = "Prestado";

        // Propiedades de lectura para mostrar en tablas
        public string TituloLibro { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;

        public Prestamo() { }

        public Prestamo(int idPrestamo, int idLibro, int idUsuario, DateTime fechaPrestamo, DateTime? fechaDevolucion, string estado)
        {
            IdPrestamo = idPrestamo;
            IdLibro = idLibro;
            IdUsuario = idUsuario;
            FechaPrestamo = fechaPrestamo;
            FechaDevolucion = fechaDevolucion;
            Estado = estado;
        }
    }
}