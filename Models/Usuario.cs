using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DUI { get; set; }
        public string NIE { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string Direccion { get; set; }
        public string Institucion { get; set; }
        public string Estado { get; set; }
    }

    public class MetricaPrestamos
    {
        public int IdUsuario { get; set; }
        public int Prestamos { get; set; }
        public int Activos { get; set; }
        public int Vencidos { get; set; }
    }
}
