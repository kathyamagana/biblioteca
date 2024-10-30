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
    internal class EditorialController
    {
         public List<object> Obtener()
        {
            List<object> editoriales = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT *, (SELECT COUNT(*) FROM libros WHERE ideditorial = a.ideditorial) AS libros FROM editoriales AS a", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var editorial = new
                        {
                            IdEditorial = (int)reader["ideditorial"],
                            Nombre = (string)reader["nombre"],
                            Nacionalidad = (string)reader["nacionalidad"],
                            Libros = (int)reader["libros"]
                        };
                        editoriales.Add(editorial);
                    }
                }
            }

            return editoriales;
        }

        public List<object> Filtrar(string busqueda)
        {
            List<object> editoriales = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("" +
                    "SELECT *, (SELECT COUNT(*) FROM libros WHERE ideditorial = a.ideditorial ) AS libros " +
                    "FROM editoriales AS a " +
                    "WHERE nombre LIKE '%"+busqueda+"%' OR nacionalidad LIKE '%"+busqueda+"%'", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var editorial = new
                        {
                            IdEditorial = (int)reader["ideditorial"],
                            Nombre = (string)reader["nombre"],
                            Nacionalidad = (string)reader["nacionalidad"],
                            Libros = (int)reader["libros"]
                        };
                        editoriales.Add(editorial);
                    }
                }
            }

            return editoriales;
        }

        public void Crear(Editorial editorial)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO editoriales (nombre, nacionalidad) VALUES (@Nombre, @Nacionalidad)", connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", editorial.Nombre);
                    cmd.Parameters.AddWithValue("@Nacionalidad", editorial.Nacionalidad);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //aplicacion de crud 

        public void Actualizar(Editorial editorial)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE editoriales SET nombre = @Nombre, nacionalidad = @Nacionalidad WHERE ideditorial = @IdEditorial", connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", editorial.Nombre);
                    cmd.Parameters.AddWithValue("@Nacionalidad", editorial.Nacionalidad);
                    cmd.Parameters.AddWithValue("@IdEditorial", editorial.IdEditorial);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int idEditorial)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM editoriales WHERE ideditorial = @IdEditorial", connection))
                {
                    cmd.Parameters.AddWithValue("@IdEditorial", idEditorial);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
