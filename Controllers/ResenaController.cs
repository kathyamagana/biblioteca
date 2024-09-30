using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using biblioteca.Models;

namespace biblioteca.Controllers
{
    internal class ResenaController
    {
        public List<ResenaLibro> Obtener(int id)
        {
            List<ResenaLibro> resenas = new List<ResenaLibro>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT usuarios.nombre, usuarios.apellido, resenas.* " +
                "FROM resenas " +
                "INNER JOIN usuarios ON resenas.idusuario = usuarios.idusuario " +
                "WHERE idlibro = " + id, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var resena = new ResenaLibro
                        {
                            IdResena = (int)reader["idresena"],
                            Nombre = (string)reader["nombre"],
                            Apellido = (string)reader["apellido"],
                            Resena = (string)reader["resena"],
                            Puntuacion = (string)reader["puntuacion"]
                        };
                        resenas.Add(resena);
                    }
                }
            }

            return resenas;
        }

        public void Crear(Resena resena)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO resenas (IdLibro, IdUsuario, Resena, Puntuacion) VALUES (@IdLibro, @IdUsuario, @Resena, @Puntuacion)", connection))
                {
                    cmd.Parameters.AddWithValue("@IdLibro", resena.IdLibro);
                    cmd.Parameters.AddWithValue("@IdUsuario", resena.IdUsuario);
                    cmd.Parameters.AddWithValue("@Resena", resena.NResena);
                    cmd.Parameters.AddWithValue("@Puntuacion", resena.Puntuacion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int idresena)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM resenas WHERE idresena = @IdResena", connection))
                {
                    cmd.Parameters.AddWithValue("@IdResena", idresena);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
