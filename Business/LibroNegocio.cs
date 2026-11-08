#nullable disable
using System;
using System.Collections.Generic;
using BibliotecaApp.Data;
using BibliotecaApp.Models;

namespace BibliotecaApp.Business
{
    public class LibroNegocio
    {
        private LibroDatos libroDatos = new LibroDatos();

        public List<Libro> Listar()
        {
            return libroDatos.Listar();
        }

        public bool Guardar(Libro obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Titulo))
            {
                throw new Exception("El título del libro es obligatorio.");
            }
            if (obj.IdAutor <= 0)
            {
                throw new Exception("Debe seleccionar un autor válido.");
            }
            if (obj.IdEditorial <= 0)
            {
                throw new Exception("Debe seleccionar una editorial válida.");
            }
            if (obj.Stock < 0)
            {
                throw new Exception("El stock no puede ser negativo.");
            }

            if (obj.IdLibro == 0)
            {
                return libroDatos.Insertar(obj);
            }
            else
            {
                return libroDatos.Actualizar(obj);
            }
        }

        public bool Eliminar(int idLibro)
        {
            return libroDatos.Eliminar(idLibro);
        }
    }
}