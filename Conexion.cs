using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biblioteca
{
    internal class Conexion
    {
        public static string connectionString
        {
            get
            {
                return "Data Source=\\SQLEXPRESS01;Initial Catalog=biblioteca; Integrated Security=True;";
            }
        }
    }
}
