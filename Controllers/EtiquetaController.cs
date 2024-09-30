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
    internal class EtiquetaController
    {
        public List<object> Obtener()
        {
            List<object> etiquetas = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM etiquetas", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var etiqueta = new
                        {
                            IdEtiqueta = (int)reader["idetiqueta"],
                            Nombre = (string)reader["nombre"]
                        };
                        etiquetas.Add(etiqueta);
                    }
                }
            }

            return etiquetas;
        }

        public List<object> Filtrar(string busqueda)
        {
            List<object> etiquetas = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("" +
                    "SELECT * " +
                    "FROM etiquetas " +
                    "WHERE nombre LIKE '%"+busqueda+"%'", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var etiqueta = new
                        {
                            IdEtiqueta = (int)reader["idetiqueta"],
                            Nombre = (string)reader["nombre"]
                        };
                        etiquetas.Add(etiqueta);
                    }
                }
            }

            return etiquetas;
        }

        public void Crear(Etiqueta etiqueta)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO etiquetas (nombre) VALUES (@Nombre)", connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", etiqueta.Nombre);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Etiqueta etiqueta)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE etiquetas SET nombre = @Nombre WHERE idetiqueta = @IdEtiqueta", connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", etiqueta.Nombre);
                    cmd.Parameters.AddWithValue("@IdEtiqueta", etiqueta.IdEtiqueta);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int idEtiqueta)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM etiquetas WHERE idetiqueta = @IdEtiqueta", connection))
                {
                    cmd.Parameters.AddWithValue("@IdEtiqueta", idEtiqueta);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
