using biblioteca.Controllers;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using biblioteca.Models;
using biblioteca.Views;
using MaterialSkin;
using System.IO;

namespace biblioteca.Controllers
{
	internal class PrestamoController
	{
		public List<object> Obtener()
		{
			List<object> prestamos = new List<object>();

			using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
			{
				connection.Open();

				using (SqlCommand cmd = new SqlCommand(""+
					"SELECT prestamos.idprestamo, imagen, isbn, titulo AS libro, autores.nombre AS autor, prestamos.estado AS estado, comentarios, tipo, " +
					"usuarios.nombre AS nombre, usuarios.apellido AS apellido, " +
					"CONVERT(VARCHAR, fechaprestamo, 105) + ' ' + CONVERT(VARCHAR, fechaprestamo, 108) AS fecha_prestamo, " +
					"CONVERT(VARCHAR, fechaentrega, 105) + ' ' + CONVERT(VARCHAR, fechaentrega, 108) AS fecha_entrega " +
					"FROM prestamos " +
					"INNER JOIN prestamoslibros ON prestamos.idprestamo = prestamoslibros.idprestamo " +
					"INNER JOIN usuarios ON prestamos.idusuario = usuarios.idusuario " +
					"INNER JOIN libros ON prestamoslibros.idlibro = libros.idlibro " +
					"INNER JOIN autores ON libros.idautor = autores.idautor", connection))
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string imagenNombre = (string)reader["imagen"];
						string rutaImagen = Path.Combine(Application.StartupPath, "Libros", imagenNombre);

						Image imagen = Image.FromFile(rutaImagen);

						var prestamo = new
						{
							//Imagen = imagen,
							IdPrestamo = (int)reader["idprestamo"],
							Cliente = (string)reader["nombre"] + " " + (string)reader["apellido"],
							ISBN = (string)reader["isbn"],
							Libro = (string)reader["libro"],
							Autor = (string)reader["autor"],
							Estado = (string)reader["estado"],
							Comentarios = (string)reader["comentarios"],
							Tipo = (string)reader["tipo"],
							FechaPrestamo = (string)reader["fecha_prestamo"],
							FechaEntrega = (string)reader["fecha_entrega"]
						};
						prestamos.Add(prestamo);
					}
				}
			}

			return prestamos;
		}

		private string ObtenerEstadoTexto(string estado)
        {
            string nEstado = "";

            if (estado == "1")
            {
                nEstado = "Activo";
            }

            if (estado == "0")
            {
                nEstado = "Inactivo";
            }

            if (estado == "3")
            {
                nEstado = "Baneado";
            }

            return nEstado;
        }

		public List<Usuario> ObtenerUsuariosValidos()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                string query = "SELECT IdUsuario, Rol, Nombre, Apellido, DUI, NIE, Genero, Telefono, Email, Direccion, Institucion, Estado FROM usuarios WHERE rol = 'usuario' AND estado = '1' ";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario
                        {
                            IdUsuario = (int)reader["IdUsuario"],
                            Rol = reader["Rol"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            DUI = reader["DUI"].ToString(),
                            NIE = reader["NIE"].ToString(),
                            Genero = reader["Genero"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Email = reader["Email"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Institucion = reader["Institucion"].ToString()
                        };

                        string estado = reader["Estado"].ToString();
                        usuario.Estado = ObtenerEstadoTexto(estado);

                        usuarios.Add(usuario);
                    }
                }
            }

            return usuarios;
        }

		public List<object> Filtrar(string busqueda)
		{
			List<object> prestamos = new List<object>();

			using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
			{
				connection.Open();

				using (SqlCommand cmd = new SqlCommand(""+
					"SELECT prestamos.idprestamo, imagen, isbn, titulo AS libro, autores.nombre AS autor, prestamos.estado AS estado, comentarios, tipo, " +
					"usuarios.nombre AS nombre, usuarios.apellido AS apellido, " +
					"CONVERT(VARCHAR, fechaprestamo, 105) + ' ' + CONVERT(VARCHAR, fechaprestamo, 108) AS fecha_prestamo, " +
					"CONVERT(VARCHAR, fechaentrega, 105) + ' ' + CONVERT(VARCHAR, fechaentrega, 108) AS fecha_entrega " +
					"FROM prestamos " +
					"INNER JOIN prestamoslibros ON prestamos.idprestamo = prestamoslibros.idprestamo " +
					"INNER JOIN usuarios ON prestamos.idusuario = usuarios.idusuario " +
					"INNER JOIN libros ON prestamoslibros.idlibro = libros.idlibro " +
					"INNER JOIN autores ON libros.idautor = autores.idautor " + 
					"WHERE titulo LIKE '%"+busqueda+"%' OR autores.nombre LIKE '%"+busqueda+"%' OR usuarios.nombre LIKE '%"+busqueda+"%' OR usuarios.apellido LIKE '%"+busqueda+"%' OR prestamos.estado LIKE '%"+busqueda+"%' OR tipo LIKE '%"+busqueda+"%'", connection))
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string imagenNombre = (string)reader["imagen"];
						string rutaImagen = Path.Combine(Application.StartupPath, "Libros", imagenNombre);

						Image imagen = Image.FromFile(rutaImagen);

						var prestamo = new
						{
							//Imagen = imagen,
							IdPrestamo = (int)reader["idprestamo"],
							Cliente = (string)reader["nombre"] + " " + (string)reader["apellido"],
							ISBN = (string)reader["isbn"],
							Libro = (string)reader["libro"],
							Autor = (string)reader["autor"],
							Estado = (string)reader["estado"],
							Comentarios = (string)reader["comentarios"],
							Tipo = (string)reader["tipo"],
							FechaPrestamo = (string)reader["fecha_prestamo"],
							FechaEntrega = (string)reader["fecha_entrega"]
						};
						prestamos.Add(prestamo);
					}
				}
			}

			return prestamos;
		}

		public List<object> PorUsuario(int idusuario)
		{
			List<object> prestamos = new List<object>();

			using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
			{
				connection.Open();

				using (SqlCommand cmd = new SqlCommand(""+
					"SELECT imagen, isbn, titulo AS libro, nombre AS autor, prestamos.estado AS estado, comentarios, tipo, " +
					"CONVERT(VARCHAR, fechaprestamo, 105) + ' ' + CONVERT(VARCHAR, fechaprestamo, 108) AS fecha_prestamo, " +
					"CONVERT(VARCHAR, fechaentrega, 105) + ' ' + CONVERT(VARCHAR, fechaentrega, 108) AS fecha_entrega " +
					"FROM prestamos " +
					"INNER JOIN prestamoslibros ON prestamos.idprestamo = prestamoslibros.idlibroprestamo " +
					"INNER JOIN libros ON prestamoslibros.idlibro = libros.idlibro " +
					"INNER JOIN autores ON libros.idautor = autores.idautor " +
					"WHERE idusuario =" + idusuario, connection))
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string imagenNombre = (string)reader["imagen"];
						string rutaImagen = Path.Combine(Application.StartupPath, "Libros", imagenNombre);

						Image imagen = Image.FromFile(rutaImagen);

						var prestamo = new
						{
							//Imagen = imagen,
							ISBN = (string)reader["isbn"],
							Libro = (string)reader["libro"],
							Autor = (string)reader["autor"],
							Estado = (string)reader["estado"],
							Comentarios = (string)reader["comentarios"],
							Tipo = (string)reader["tipo"],
							FechaPrestamo = (string)reader["fecha_prestamo"],
							FechaEntrega = (string)reader["fecha_entrega"]
						};
						prestamos.Add(prestamo);
					}
				}
			}

			return prestamos;
		}

		public void Crear(Prestamo prestamo, int idLibro)
		{
			int idPrestamo;

			using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
			{
				connection.Open();

				using (SqlTransaction transaction = connection.BeginTransaction())
				{
					try
					{
						using (SqlCommand cmd = new SqlCommand("INSERT INTO prestamos (IdUsuario, FechaPrestamo, FechaEntrega, Estado, Comentarios, Tipo) OUTPUT INSERTED.IdPrestamo VALUES (@IdUsuario, @FechaPrestamo, @FechaEntrega, @Estado, @Comentarios, @Tipo)", connection, transaction))
						{
							cmd.Parameters.AddWithValue("@IdUsuario", prestamo.IdUsuario);
							cmd.Parameters.AddWithValue("@FechaPrestamo", prestamo.FechaPrestamo);
							cmd.Parameters.AddWithValue("@FechaEntrega", prestamo.FechaEntrega);
							cmd.Parameters.AddWithValue("@Estado", prestamo.Estado);
							cmd.Parameters.AddWithValue("@Comentarios", prestamo.Comentarios);
							cmd.Parameters.AddWithValue("@Tipo", prestamo.Tipo);

							idPrestamo = (int)cmd.ExecuteScalar();
						}

						using (SqlCommand cmd = new SqlCommand("INSERT INTO prestamoslibros (idprestamo, idlibro) VALUES (@IdPrestamo, @IdLibro)", connection, transaction))
						{
							cmd.Parameters.AddWithValue("@IdPrestamo", idPrestamo);
							cmd.Parameters.AddWithValue("@IdLibro", idLibro);
							cmd.ExecuteNonQuery();
						}

						using (SqlCommand cmd = new SqlCommand("UPDATE libros SET disponibles = disponibles -1 WHERE idlibro = @IdLibro", connection, transaction))
						{
							cmd.Parameters.AddWithValue("@IdLibro", idLibro);
							cmd.ExecuteNonQuery();
						}

						transaction.Commit();
					}
					catch (Exception)
					{
						transaction.Rollback();
						throw;
					}
				}
			}
		}

		public void Actualizar(DateTime FechaEntrega, string Comentarios, string Estado, int IdPrestamo)
		{
			using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
			{
				connection.Open();

				using (SqlTransaction transaction = connection.BeginTransaction())
				{
					using (SqlCommand cmd = new SqlCommand("UPDATE prestamos SET FechaEntrega = @FechaEntrega, Comentarios = @Comentarios, Estado = @Estado WHERE IdPrestamo = @IdPrestamo", connection, transaction))
					{
						cmd.Parameters.AddWithValue("@FechaEntrega", FechaEntrega);
						cmd.Parameters.AddWithValue("@Comentarios", Comentarios);
						cmd.Parameters.AddWithValue("@Estado", Estado);
						cmd.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);

						cmd.ExecuteNonQuery();
					}

					if (Estado == "Finalizado")
					{
						using (SqlCommand selectCmd = new SqlCommand("SELECT TOP 1 idlibro FROM prestamoslibros WHERE idprestamo = @IdPrestamo", connection, transaction))
						{
							selectCmd.Parameters.AddWithValue("@IdPrestamo", IdPrestamo);
							List<int> idLibros = new List<int>();

							using (SqlDataReader reader = selectCmd.ExecuteReader())
							{
								while (reader.Read())
								{
									idLibros.Add(reader.GetInt32(0));
								}
							}

							foreach (int idLibro in idLibros)
							{
								using (SqlCommand updateCmd = new SqlCommand("UPDATE libros SET disponibles = disponibles + 1 WHERE idlibro = @IdLibro", connection, transaction))
								{
									updateCmd.Parameters.AddWithValue("@IdLibro", idLibro);
									updateCmd.ExecuteNonQuery();
								}
							}
						}
					}

					transaction.Commit();
				}
			}
		}

		public void Eliminar(int idPrestamo)
		{
			using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
			{
				connection.Open();

				using (SqlTransaction transaction = connection.BeginTransaction())
				{
					List<int> idLibros = new List<int>();

					using (SqlCommand selectCmd = new SqlCommand("SELECT idlibro FROM prestamoslibros WHERE idprestamo = @IdPrestamo", connection, transaction))
					{
						selectCmd.Parameters.AddWithValue("@IdPrestamo", idPrestamo);

						using (SqlDataReader reader = selectCmd.ExecuteReader())
						{
							while (reader.Read())
							{
								idLibros.Add(reader.GetInt32(0));
							}
						}
					}

					foreach (int idLibro in idLibros)
					{
						using (SqlCommand updateCmd = new SqlCommand("UPDATE libros SET disponibles = disponibles + 1 WHERE idlibro = @IdLibro", connection, transaction))
						{
							updateCmd.Parameters.AddWithValue("@IdLibro", idLibro);
							updateCmd.ExecuteNonQuery();
						}

						using (SqlCommand deleteCmd = new SqlCommand("DELETE FROM prestamoslibros WHERE idprestamo = @IdPrestamo AND idlibro = @IdLibro", connection, transaction))
						{
							deleteCmd.Parameters.AddWithValue("@IdPrestamo", idPrestamo);
							deleteCmd.Parameters.AddWithValue("@IdLibro", idLibro);
							deleteCmd.ExecuteNonQuery();
						}
					}

					using (SqlCommand cmd = new SqlCommand("DELETE FROM prestamos WHERE IdPrestamo = @IdPrestamo", connection, transaction))
					{
						cmd.Parameters.AddWithValue("@IdPrestamo", idPrestamo);
						cmd.ExecuteNonQuery();
					}

					transaction.Commit();
				}
			}
		}

	}
}
