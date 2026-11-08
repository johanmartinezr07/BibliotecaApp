#nullable disable
namespace BibliotecaApp.Models
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int IdAutor { get; set; }
        public int IdEditorial { get; set; }
        public int AnioPublicacion { get; set; }
        public int Stock { get; set; }

        // Propiedades de lectura para mostrar en tablas
        public string NombreAutor { get; set; } = string.Empty;
        public string NombreEditorial { get; set; } = string.Empty;

        public Libro() { }

        public Libro(int idLibro, string titulo, int idAutor, int idEditorial, int anioPublicacion, int stock)
        {
            IdLibro = idLibro;
            Titulo = titulo;
            IdAutor = idAutor;
            IdEditorial = idEditorial;
            AnioPublicacion = anioPublicacion;
            Stock = stock;
        }
    }
}