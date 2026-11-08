#nullable disable
using System;
using System.Collections.Generic;
using BibliotecaApp.Data;
using BibliotecaApp.Models;

namespace BibliotecaApp.Business
{
    public class EditorialNegocio
    {
        private EditorialDatos editorialDatos = new EditorialDatos();

        public List<Editorial> Listar()
        {
            return editorialDatos.Listar();
        }

        public bool Guardar(Editorial obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                throw new Exception("El nombre de la editorial es obligatorio.");
            }

            if (obj.IdEditorial == 0)
            {
                return editorialDatos.Insertar(obj);
            }
            else
            {
                return editorialDatos.Actualizar(obj);
            }
        }

        public bool Eliminar(int idEditorial)
        {
            return editorialDatos.Eliminar(idEditorial);
        }
    }
}