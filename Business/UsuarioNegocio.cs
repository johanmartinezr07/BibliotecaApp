#nullable disable
using System;
using System.Collections.Generic;
using BibliotecaApp.Data;
using BibliotecaApp.Models;

namespace BibliotecaApp.Business
{
    public class UsuarioNegocio
    {
        private UsuarioDatos usuarioDatos = new UsuarioDatos();

        public List<Usuario> Listar()
        {
            return usuarioDatos.Listar();
        }

        public bool Guardar(Usuario obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Documento))
            {
                throw new Exception("El nombre, apellido y documento son obligatorios.");
            }

            if (obj.IdUsuario == 0)
            {
                return usuarioDatos.Insertar(obj);
            }
            else
            {
                return usuarioDatos.Actualizar(obj);
            }
        }

        public bool Eliminar(int idUsuario)
        {
            return usuarioDatos.Eliminar(idUsuario);
        }
    }
}