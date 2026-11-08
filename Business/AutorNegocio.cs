#nullable disable
using System;
using System.Collections.Generic;
using BibliotecaApp.Data;
using BibliotecaApp.Models;

namespace BibliotecaApp.Business
{
    public class AutorNegocio
    {
        private AutorDatos autorDatos = new AutorDatos();

        public List<Autor> Listar()
        {
            return autorDatos.Listar();
        }

        public bool Guardar(Autor obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                throw new Exception("El nombre y el apellido son obligatorios.");
            }

            if (obj.IdAutor == 0)
            {
                return autorDatos.Insertar(obj);
            }
            else
            {
                return autorDatos.Actualizar(obj);
            }
        }

        public bool Eliminar(int idAutor)
        {
            return autorDatos.Eliminar(idAutor);
        }
    }
}