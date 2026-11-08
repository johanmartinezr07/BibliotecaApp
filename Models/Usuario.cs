#nullable disable
namespace BibliotecaApp.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

        public Usuario() { }

        public Usuario(int idUsuario, string nombre, string apellido, string documento, string telefono, string correo)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Apellido = apellido;
            Documento = documento;
            Telefono = telefono;
            Correo = correo;
        }
    }
}