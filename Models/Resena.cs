using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca.Models
{
    public class Resena
    {
        public int IdResena { get; set; }
        public int IdLibro { get; set; }
        public int IdUsuario { get; set; }
        public string NResena { get; set; }
        public string Puntuacion { get; set; }
    }

    public class ResenaLibro
    {
        public int IdResena { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Resena { get; set; }
        public string Puntuacion { get; set; }
    }
}
