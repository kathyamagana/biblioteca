using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca.Models
{
    public class Dashboards
    {
    }

    public class MesesMetrica
    {
        public int Mes { get; set; }
        public string NombreMes { get; set; }
        public int TotalPrestamos { get; set; }
    }

    public class Metrica
    {
        public int Libros { get; set; }
        public int Usuarios { get; set; }
        public int Activos { get; set; }
        public int Vencidos { get; set; }
    }
}
