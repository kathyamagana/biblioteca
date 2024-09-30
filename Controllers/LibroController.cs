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
    internal class LibroController
    {
        public List<object> Obtener()
        {
            List<object> libros = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT " +
                    "autores.nombre AS autor, generos.nombre AS genero, editoriales.nombre AS editorial, libros.*" +
                    "FROM libros " +
                    "INNER JOIN autores ON libros.idautor = autores.idautor " +
                    "INNER JOIN generos ON libros.idgenero = generos.idgenero " +
                    "INNER JOIN editoriales ON libros.ideditorial = editoriales.ideditorial", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var libro = new
                        {
                            IdLibro = (int)reader["idlibro"],
                            Autor = reader["autor"] is DBNull ? string.Empty : (string)reader["autor"],
                            Genero = reader["genero"] is DBNull ? string.Empty : (string)reader["genero"],
                            Editorial = reader["editorial"] is DBNull ? string.Empty : (string)reader["editorial"],
                            Titulo = reader["titulo"] is DBNull ? string.Empty : (string)reader["titulo"],
                            Ano = reader["ano"] is DBNull ? 0 : (int)reader["ano"],
                            ISBN = reader["isbn"] is DBNull ? string.Empty : (string)reader["isbn"],
                            Ejemplares = reader["ejemplares"] is DBNull ? string.Empty : (string)reader["ejemplares"],
                            Disponibles = reader["disponibles"] is DBNull ? string.Empty : (string)reader["disponibles"],
                            Estado = reader["estado"] is DBNull ? string.Empty : (string)reader["estado"],
                            Ubicacion = reader["ubicacion"] is DBNull ? string.Empty : (string)reader["ubicacion"],
                            Descripcion = reader["descripcion"] is DBNull ? string.Empty : (string)reader["descripcion"]
                        };

                        libros.Add(libro);
                    }
                }
            }

            return libros;
        }

        public List<Libro> ObtenerLibros()
        {
            List<Libro> libros = new List<Libro>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM libros ", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var libro = new Libro
                        {
                            IdLibro = (int)reader["idlibro"],
                            IdAutor = (int)reader["idautor"],
                            IdGenero = (int)reader["idgenero"],
                            IdEditorial = (int)reader["ideditorial"],
                            Titulo = reader["titulo"] is DBNull ? string.Empty : (string)reader["titulo"],
                            Ano = (int)reader["ano"],
                            Idioma = reader["idioma"] is DBNull ? string.Empty : (string)reader["idioma"],
                            Tamano = reader["tamano"] is DBNull ? string.Empty : (string)reader["tamano"],
                            Edicion = reader["edicion"] is DBNull ? string.Empty : (string)reader["edicion"],
                            Paginas = reader["paginas"] is DBNull ? string.Empty : (string)reader["paginas"],
                            ISBN = reader["isbn"] is DBNull ? string.Empty : (string)reader["isbn"],
                            Contenido_Visual = reader["contenido_visual"] is DBNull ? string.Empty : (string)reader["contenido_visual"],
                            Tomo = reader["tomo"] is DBNull ? string.Empty : (string)reader["tomo"],
                            Serie = reader["serie"] is DBNull ? string.Empty : (string)reader["serie"],
                            Ejemplares = reader["ejemplares"] is DBNull ? string.Empty : (string)reader["ejemplares"],
                            Disponibles = reader["disponibles"] is DBNull ? string.Empty : (string)reader["disponibles"],
                            Estado = reader["estado"] is DBNull ? string.Empty : (string)reader["estado"],
                            Ubicacion = reader["ubicacion"] is DBNull ? string.Empty : (string)reader["ubicacion"],
                            Clasificacion = reader["clasificacion"] is DBNull ? string.Empty : (string)reader["clasificacion"],
                            Cutter = reader["cutter"] is DBNull ? string.Empty : (string)reader["cutter"],
                            Material = reader["material"] is DBNull ? string.Empty : (string)reader["material"],
                            Descripcion = reader["descripcion"] is DBNull ? string.Empty : (string)reader["descripcion"],
                            Imagen = reader["imagen"] is DBNull ? string.Empty : (string)reader["imagen"],
                            Procedencia = reader["procedencia"] is DBNull ? string.Empty : (string)reader["procedencia"],
                        };

                        libros.Add(libro);
                    }
                }
            }

            return libros;
        }

        public List<DetalleLibro> ObtenerLibro(int id)
        {
            List<DetalleLibro> libros = new List<DetalleLibro>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT " +
                    "autores.nombre AS autor, generos.nombre AS genero, editoriales.nombre AS editorial, libros.* " +
                    "FROM libros " +
                    "INNER JOIN autores ON libros.idautor = autores.idautor " +
                    "INNER JOIN generos ON libros.idgenero = generos.idgenero " +
                    "INNER JOIN editoriales ON libros.ideditorial = editoriales.ideditorial " +
                    "WHERE idlibro = " + id, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var libro = new DetalleLibro
                        {
                            IdLibro = (int)reader["idlibro"],
                            Autor = reader["autor"] is DBNull ? string.Empty : (string)reader["autor"],
                            Genero = reader["genero"] is DBNull ? string.Empty : (string)reader["genero"],
                            Editorial = reader["editorial"] is DBNull ? string.Empty : (string)reader["editorial"],
                            Titulo = reader["titulo"] is DBNull ? string.Empty : (string)reader["titulo"],
                            Ano = (int)reader["ano"],
                            Idioma = reader["idioma"] is DBNull ? string.Empty : (string)reader["idioma"],
                            Tamano = reader["tamano"] is DBNull ? string.Empty : (string)reader["tamano"],
                            Edicion = reader["edicion"] is DBNull ? string.Empty : (string)reader["edicion"],
                            Paginas = reader["paginas"] is DBNull ? string.Empty : (string)reader["paginas"],
                            ISBN = reader["isbn"] is DBNull ? string.Empty : (string)reader["isbn"],
                            Contenido_Visual = reader["contenido_visual"] is DBNull ? string.Empty : (string)reader["contenido_visual"],
                            Tomo = reader["tomo"] is DBNull ? string.Empty : (string)reader["tomo"],
                            Serie = reader["serie"] is DBNull ? string.Empty : (string)reader["serie"],
                            Ejemplares = reader["ejemplares"] is DBNull ? string.Empty : (string)reader["ejemplares"],
                            Disponibles = reader["disponibles"] is DBNull ? string.Empty : (string)reader["disponibles"],
                            Estado = reader["estado"] is DBNull ? string.Empty : (string)reader["estado"],
                            Ubicacion = reader["ubicacion"] is DBNull ? string.Empty : (string)reader["ubicacion"],
                            Clasificacion = reader["clasificacion"] is DBNull ? string.Empty : (string)reader["clasificacion"],
                            Cutter = reader["cutter"] is DBNull ? string.Empty : (string)reader["cutter"],
                            Material = reader["material"] is DBNull ? string.Empty : (string)reader["material"],
                            Descripcion = reader["descripcion"] is DBNull ? string.Empty : (string)reader["descripcion"],
                            Imagen = reader["imagen"] is DBNull ? string.Empty : (string)reader["imagen"],
                            Procedencia = reader["procedencia"] is DBNull ? string.Empty : (string)reader["procedencia"],
                        };
                        libros.Add(libro);
                    }
                }
            }

            return libros;
        }

        public List<object> Filtrar(string busqueda)
        {
            List<object> librosFiltrados = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                string query = "SELECT autores.nombre AS autor, generos.nombre AS genero, editoriales.nombre AS editorial, libros.* " +
                            "FROM libros " +
                            "INNER JOIN autores ON libros.IdAutor = autores.IdAutor " +
                            "INNER JOIN generos ON libros.IdGenero = generos.IdGenero " +
                            "INNER JOIN editoriales ON libros.IdEditorial = editoriales.IdEditorial " +
                            "WHERE autores.nombre LIKE @Busqueda OR generos.nombre LIKE @Busqueda OR " +
                            "libros.Titulo LIKE @Busqueda OR libros.Descripcion LIKE @Busqueda";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Busqueda", "%" + busqueda + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var libro = new
                            {
                                IdLibro = (int)reader["idlibro"],
                                Autor = reader["autor"] is DBNull ? string.Empty : (string)reader["autor"],
                                Genero = reader["genero"] is DBNull ? string.Empty : (string)reader["genero"],
                                Editorial = reader["editorial"] is DBNull ? string.Empty : (string)reader["editorial"],
                                Titulo = reader["titulo"] is DBNull ? string.Empty : (string)reader["titulo"],
                                Ano = reader["ano"] is DBNull ? 0 : (int)reader["ano"],
                                ISBN = reader["isbn"] is DBNull ? string.Empty : (string)reader["isbn"],
                                Ejemplares = reader["ejemplares"] is DBNull ? string.Empty : (string)reader["ejemplares"],
                                Disponibles = reader["disponibles"] is DBNull ? string.Empty : (string)reader["disponibles"],
                                Estado = reader["estado"] is DBNull ? string.Empty : (string)reader["estado"],
                                Ubicacion = reader["ubicacion"] is DBNull ? string.Empty : (string)reader["ubicacion"],
                                Descripcion = reader["descripcion"] is DBNull ? string.Empty : (string)reader["descripcion"]
                            };
                            librosFiltrados.Add(libro);
                        }
                    }
                }
            }

            return librosFiltrados;
        }

        public void Crear(Libro libro)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO libros (idautor, idgenero, ideditorial, titulo, ano, idioma, tamano, edicion, paginas, isbn, contenido_visual, tomo, serie, ejemplares, disponibles, estado, ubicacion, clasificacion, cutter, material, descripcion, imagen, procedencia) VALUES (@IdAutor, @IdGenero, @IdEditorial, @Titulo, @Ano, @Idioma, @Tamano, @Edicion, @Paginas, @ISBN, @Contenido_Visual, @Tomo, @Serie, @Ejemplares, @Disponibles, @Estado, @Ubicacion, @Clasificacion, @Cutter, @Material, @Descripcion, @Imagen, @Procedencia)", connection))
                {
                    cmd.Parameters.AddWithValue("@IdAutor", libro.IdAutor);
                    cmd.Parameters.AddWithValue("@IdGenero", libro.IdGenero);
                    cmd.Parameters.AddWithValue("@IdEditorial", libro.IdEditorial);
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Ano", libro.Ano);
                    cmd.Parameters.AddWithValue("@Idioma", libro.Idioma);
                    cmd.Parameters.AddWithValue("@Tamano", libro.Tamano);
                    cmd.Parameters.AddWithValue("@Edicion", libro.Edicion);
                    cmd.Parameters.AddWithValue("@Paginas", libro.Paginas);
                    cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);
                    cmd.Parameters.AddWithValue("@Contenido_Visual", libro.Contenido_Visual);
                    cmd.Parameters.AddWithValue("@Tomo", libro.Tomo);
                    cmd.Parameters.AddWithValue("@Serie", libro.Serie);
                    cmd.Parameters.AddWithValue("@Ejemplares", libro.Ejemplares);
                    cmd.Parameters.AddWithValue("@Disponibles", libro.Disponibles);
                    cmd.Parameters.AddWithValue("@Estado", libro.Estado);
                    cmd.Parameters.AddWithValue("@Ubicacion", libro.Ubicacion);
                    cmd.Parameters.AddWithValue("@Clasificacion", libro.Clasificacion);
                    cmd.Parameters.AddWithValue("@Cutter", libro.Cutter);
                    cmd.Parameters.AddWithValue("@Material", libro.Material);
                    cmd.Parameters.AddWithValue("@Descripcion", libro.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", libro.Imagen);
                    cmd.Parameters.AddWithValue("@Procedencia", libro.Procedencia);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Libro libro)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE libros SET idautor = @IdAutor, idgenero = @IdGenero, ideditorial = @IdEditorial, titulo = @Titulo, ano = @Ano, idioma = @Idioma, tamano = @Tamano, edicion = @Edicion, paginas = @Paginas, isbn = @ISBN, contenido_visual = @Contenido_Visual, tomo = @Tomo, serie = @Serie, ejemplares = @Ejemplares, disponibles = @Disponibles, estado = @Estado, ubicacion = @Ubicacion, clasificacion = @Clasificacion, cutter = @Cutter, material = @Material, descripcion = @Descripcion, imagen = @Imagen, procedencia = @Procedencia WHERE idlibro = @IdLibro", connection))
                {
                    cmd.Parameters.AddWithValue("@IdLibro", libro.IdLibro);
                    cmd.Parameters.AddWithValue("@IdAutor", libro.IdAutor);
                    cmd.Parameters.AddWithValue("@IdGenero", libro.IdGenero);
                    cmd.Parameters.AddWithValue("@IdEditorial", libro.IdEditorial);
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Ano", libro.Ano);
                    cmd.Parameters.AddWithValue("@Idioma", libro.Idioma);
                    cmd.Parameters.AddWithValue("@Tamano", libro.Tamano);
                    cmd.Parameters.AddWithValue("@Edicion", libro.Edicion);
                    cmd.Parameters.AddWithValue("@Paginas", libro.Paginas);
                    cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);
                    cmd.Parameters.AddWithValue("@Contenido_Visual", libro.Contenido_Visual);
                    cmd.Parameters.AddWithValue("@Tomo", libro.Tomo);
                    cmd.Parameters.AddWithValue("@Serie", libro.Serie);
                    cmd.Parameters.AddWithValue("@Ejemplares", libro.Ejemplares);
                    cmd.Parameters.AddWithValue("@Disponibles", libro.Disponibles);
                    cmd.Parameters.AddWithValue("@Estado", libro.Estado);
                    cmd.Parameters.AddWithValue("@Ubicacion", libro.Ubicacion);
                    cmd.Parameters.AddWithValue("@Clasificacion", libro.Clasificacion);
                    cmd.Parameters.AddWithValue("@Cutter", libro.Cutter);
                    cmd.Parameters.AddWithValue("@Material", libro.Material);
                    cmd.Parameters.AddWithValue("@Descripcion", libro.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", libro.Imagen);
                    cmd.Parameters.AddWithValue("@Procedencia", libro.Procedencia);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int idLibro)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM libros WHERE idlibro = @IdLibro", connection))
                {
                    cmd.Parameters.AddWithValue("@IdLibro", idLibro);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Actualizar Disponibles
        public void ActualizarDisponibles(int id, int disponibles)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE libros SET disponibles = @Disponibles WHERE idlibro = @IdLibro", connection))
                {
                    cmd.Parameters.AddWithValue("@IdLibro", id);
                    cmd.Parameters.AddWithValue("@Disponibles", disponibles);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Etiquetas
        public List<object> ObtenerEtiquetas(int id)
        {
            List<object> etiquetas = new List<object>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT " +
                    "etiquetaslibros.idetiquetalibro AS id, etiquetas.nombre AS etiqueta " +
                    "FROM etiquetaslibros " +
                    "INNER JOIN etiquetas ON etiquetaslibros.idetiqueta = etiquetas.idetiqueta " +
                    "WHERE idlibro = " + id, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var etiqueta = new
                        {
                            Id = (int)reader["id"],
                            Etiqueta = reader["etiqueta"] is DBNull ? string.Empty : (string)reader["etiqueta"]
                        };
                        etiquetas.Add(etiqueta);
                    }
                }
            }

            return etiquetas;
        }

        public void AnadirEtiqueta(int idlibro, int idetiqueta)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO etiquetaslibros (idetiqueta, idlibro) VALUES (@IdEtiqueta, @IdLibro)", connection))
                {
                    cmd.Parameters.AddWithValue("@IdEtiqueta", idetiqueta);
                    cmd.Parameters.AddWithValue("@IdLibro", idlibro);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarEtiqueta(int idetiquetalibro)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM etiquetaslibros WHERE idetiquetalibro = @IdEtiquetaLibro", connection))
                {
                    cmd.Parameters.AddWithValue("@IdEtiquetaLibro", idetiquetalibro);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
