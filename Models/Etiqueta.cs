using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca.Models
{
    public class Etiqueta
    {
        public int IdEtiqueta { get; set; }
        public string Nombre { get; set; }
    }

    public class EtiquetaLibro
    {
        public int IdEtiquetaLibro { get; set; }
        public int IdEtiqueta { get; set; }
        public int IdLibro { get; set; }
    }
}
