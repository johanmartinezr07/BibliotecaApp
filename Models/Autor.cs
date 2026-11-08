namespace BibliotecaApp.Models
{
    public class Autor
    {
        public int IdAutor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;

        public Autor() { }

        public Autor(int idAutor, string nombre, string apellido)
        {
            IdAutor = idAutor;
            Nombre = nombre;
            Apellido = apellido;
        }
    }
}