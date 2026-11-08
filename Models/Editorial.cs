#nullable disable
namespace BibliotecaApp.Models
{
    public class Editorial
    {
        public int IdEditorial { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;

        public Editorial() { }

        public Editorial(int idEditorial, string nombre, string pais)
        {
            IdEditorial = idEditorial;
            Nombre = nombre;
            Pais = pais;
        }
    }
}