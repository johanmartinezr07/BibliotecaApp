#nullable disable
using System;
using System.Collections.Generic;
using BibliotecaApp.Data;
using BibliotecaApp.Models;

namespace BibliotecaApp.Business
{
    public class PrestamoNegocio
    {
        private PrestamoDatos prestamoDatos = new PrestamoDatos();

        public List<Prestamo> Listar()
        {
            return prestamoDatos.Listar();
        }

        public bool RegistrarPrestamo(Prestamo obj)
        {
            if (obj.IdLibro <= 0)
            {
                throw new Exception("Debe seleccionar un libro válido.");
            }
            if (obj.IdUsuario <= 0)
            {
                throw new Exception("Debe seleccionar un usuario válido.");
            }

            return prestamoDatos.RegistrarPrestamo(obj);
        }

        public bool RegistrarDevolucion(int idPrestamo, int idLibro)
        {
            if (idPrestamo <= 0 || idLibro <= 0)
            {
                throw new Exception("Seleccione un préstamo válido para devolver.");
            }

            return prestamoDatos.RegistrarDevolucion(idPrestamo, idLibro);
        }
    }
}