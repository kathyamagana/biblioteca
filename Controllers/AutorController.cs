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
    public class AutorController
    {
        public List<object> Obtener()
        {
            List<object> autores = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT *, (SELECT COUNT(*) FROM libros WHERE idautor = a.idautor) AS libros FROM autores AS a", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var autor = new
                        {
                            IdAutor = (int)reader["idautor"],
                            Nombre = (string)reader["nombre"],
                            Nacionalidad = (string)reader["nacionalidad"],
                            Libros = (int)reader["libros"]
                        };
                        autores.Add(autor);
                    }
                }
            }

            return autores;
        }

        public List<object> Filtrar(string busqueda)
        {
            List<object> autores = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("" +
                    "SELECT *, (SELECT COUNT(*) FROM libros WHERE idautor = a.idautor) AS libros " +
                    "FROM autores AS a " +
                    "WHERE nombre LIKE '%"+busqueda+"%' OR nacionalidad LIKE '%"+busqueda+"%'", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var autor = new
                        {
                            IdAutor = (int)reader["idautor"],
                            Nombre = (string)reader["nombre"],
                            Nacionalidad = (string)reader["nacionalidad"],
                            Libros = (int)reader["libros"]
                        };
                        autores.Add(autor);
                    }
                }
            }

            return autores;
        }

        public void Crear(Autor autor)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO autores (nombre, nacionalidad) VALUES (@Nombre, @Nacionalidad)", connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", autor.Nombre);
                    cmd.Parameters.AddWithValue("@Nacionalidad", autor.Nacionalidad);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Autor autor)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE autores SET nombre = @Nombre, nacionalidad = @Nacionalidad WHERE idautor = @IdAutor", connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", autor.Nombre);
                    cmd.Parameters.AddWithValue("@Nacionalidad", autor.Nacionalidad);
                    cmd.Parameters.AddWithValue("@IdAutor", autor.IdAutor);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int idAutor)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM autores WHERE idautor = @IdAutor", connection))
                {
                    cmd.Parameters.AddWithValue("@IdAutor", idAutor);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
