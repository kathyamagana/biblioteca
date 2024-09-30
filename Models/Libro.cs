using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca.Models
{
    public class Libro
    {
        public int IdLibro { get; set; }
        public int IdAutor { get; set; }
        public int IdGenero { get; set; }
        public int IdEditorial { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string Idioma { get; set; }
        public string Tamano { get; set; }
        public string Edicion { get; set; }
        public string Paginas { get; set; }
        public string ISBN { get; set; }
        public string Contenido_Visual { get; set; }
        public string Tomo { get; set; }
        public string Serie { get; set; }
        public string Ejemplares { get; set; }
        public string Disponibles { get; set; }
        public string Estado { get; set; }
        public string Ubicacion { get; set; }
        public string Clasificacion { get; set; }
        public string Cutter { get; set; }
        public string Material { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Procedencia { get; set; }
    }

    public class DetalleLibro
    {
        public int IdLibro { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public string Editorial { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string Idioma { get; set; }
        public string Tamano { get; set; }
        public string Edicion { get; set; }
        public string Paginas { get; set; }
        public string ISBN { get; set; }
        public string Contenido_Visual { get; set; }
        public string Tomo { get; set; }
        public string Serie { get; set; }
        public string Ejemplares { get; set; }
        public string Disponibles { get; set; }
        public string Estado { get; set; }
        public string Ubicacion { get; set; }
        public string Clasificacion { get; set; }
        public string Cutter { get; set; }
        public string Material { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Procedencia { get; set; }
    }
}
