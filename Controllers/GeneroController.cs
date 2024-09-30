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
    internal class GeneroController
    {
        public List<object> Obtener()
        {
            List<object> generos = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT *, (SELECT COUNT(*) FROM libros WHERE idgenero = a.idgenero) AS libros FROM generos AS a", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var genero = new
                        {
                            IdGenero = (int)reader["idgenero"],
                            Nombre = (string)reader["nombre"],
                            Descripcion = (string)reader["descripcion"],
                            Libros = (int)reader["libros"]
                        };
                        generos.Add(genero);
                    }
                }
            }

            return generos;
        }

        public List<object> Filtrar(string busqueda)
        {
            List<object> generos = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("" +
                    "SELECT *, (SELECT COUNT(*) FROM libros WHERE idgenero = a.idgenero) AS libros " +
                    "FROM generos AS a " +
                    "WHERE nombre LIKE '%"+busqueda+"%' OR descripcion LIKE '%"+busqueda+"%'", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var genero = new
                        {
                            IdGenero = (int)reader["idgenero"],
                            Nombre = (string)reader["nombre"],
                            Descripcion = (string)reader["descripcion"],
                            Libros = (int)reader["libros"]
                        };
                        generos.Add(genero);
                    }
                }
            }

            return generos;
        }

        public void Crear(Genero genero)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO generos (nombre, descripcion) VALUES (@Nombre, @Descripcion)", connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", genero.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", genero.Descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Genero genero)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE generos SET nombre = @Nombre, descripcion = @Descripcion WHERE idgenero = @IdGenero", connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", genero.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", genero.Descripcion);
                    cmd.Parameters.AddWithValue("@IdGenero", genero.IdGenero);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int idGenero)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM generos WHERE idgenero = @IdGenero", connection))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", idGenero);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
