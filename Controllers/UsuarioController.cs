using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using biblioteca.Models;

using System.Security.Cryptography;

namespace biblioteca.Controllers
{
    internal class UsuarioController
    {
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

        public List<Usuario> Obtener(string tipo)
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                string tipoU = "";
                if(tipo == "todos"){
                    tipoU = "WHERE rol != 'admin' ";
                }else if(tipo == "usuarios"){
                    tipoU = "WHERE rol = 'usuario' ";
                }

                string query = "SELECT IdUsuario, Rol, Nombre, Apellido, DUI, NIE, Genero, Telefono, Email, Direccion, Institucion, Estado FROM usuarios " + tipoU;

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

        public List<Usuario> ObtenerUsuario(int id)
        {
            List<Usuario> usuario = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                string query = "SELECT IdUsuario, Rol, Nombre, Apellido, DUI, NIE, Genero, Telefono, Email, Direccion, Institucion, Estado, Contrasena FROM usuarios WHERE IdUsuario = " + id;

                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var _usuario = new Usuario
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
                            Institucion = reader["Institucion"].ToString(),
                            Contrasena = reader["Contrasena"].ToString()
                        };

                        string estado = reader["Estado"].ToString();
                        _usuario.Estado = ObtenerEstadoTexto(estado);

                        usuario.Add(_usuario);
                    }
                }
            }

            return usuario;
        }

        public List<Usuario> Filtrar(string busqueda, string usuarios)
        {
            List<Usuario> usuariosFiltrados = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM usuarios " +
                            "WHERE rol != 'admin' AND (rol LIKE @Busqueda OR " +
                            "nombre LIKE @Busqueda OR " +
                            "apellido LIKE @Busqueda OR " +
                            "dui LIKE @Busqueda OR " +
                            "nie LIKE @Busqueda OR " +
                            "institucion LIKE @Busqueda)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Busqueda", "%" + busqueda + "%");

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

                            usuariosFiltrados.Add(usuario);
                        }
                    }
                }
            }

            return usuariosFiltrados;
        }

        private string CrearMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public void Crear(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                string sqlQuery = "INSERT INTO usuarios (Rol, Nombre, Apellido, DUI, NIE, Genero, Telefono, Email, Contrasena, Direccion, Institucion, Estado) " +
                                "VALUES (@Rol, @Nombre, @Apellido, @DUI, @NIE, @Genero, @Telefono, @Email, @Contrasena, @Direccion, @Institucion, @Estado)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@DUI", usuario.DUI);
                    cmd.Parameters.AddWithValue("@NIE", usuario.NIE);
                    cmd.Parameters.AddWithValue("@Genero", usuario.Genero);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    
                    string contrasenaEncriptada = CrearMD5(usuario.Contrasena);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasenaEncriptada);

                    cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("@Institucion", usuario.Institucion);
                    cmd.Parameters.AddWithValue("@Estado", usuario.Estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                string sqlQuery = "UPDATE usuarios SET " +
                    "Rol = @Rol, " +
                    "Nombre = @Nombre, " +
                    "Apellido = @Apellido, " +
                    "DUI = @DUI, " +
                    "NIE = @NIE, " +
                    "Genero = @Genero, " +
                    "Telefono = @Telefono, " +
                    "Email = @Email, " +
                    "Contrasena = @Contrasena, " +
                    "Direccion = @Direccion, " +
                    "Institucion = @Institucion, " +
                    "Estado = @Estado " +
                    "WHERE IdUsuario = @IdUsuario";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@DUI", usuario.DUI);
                    cmd.Parameters.AddWithValue("@NIE", usuario.NIE);
                    cmd.Parameters.AddWithValue("@Genero", usuario.Genero);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                    cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    cmd.Parameters.AddWithValue("@Institucion", usuario.Institucion);
                    cmd.Parameters.AddWithValue("@Estado", usuario.Estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("DELETE FROM usuarios WHERE idusuario = @IdUsuario", connection))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CambiarEstado(int IdUsuario, int Estado)
        {
            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                string sqlQuery = "UPDATE usuarios SET " +
                    "Estado = @Estado " +
                    "WHERE IdUsuario = @IdUsuario";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Estado", Estado);
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<MetricaPrestamos> MetricaPrestamos(int id)
        {
            List<MetricaPrestamos> metricas = new List<MetricaPrestamos>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT idusuario, " +
                "(SELECT COUNT(*) FROM prestamos WHERE prestamos.idusuario = us.idusuario) AS Prestamos, " +
                "(SELECT COUNT(*) FROM prestamos WHERE estado = 'Activo' AND prestamos.idusuario = us.idusuario) AS Activos, " +
                "(SELECT COUNT(*) FROM prestamos WHERE estado = 'Vencido' AND prestamos.idusuario = us.idusuario) AS Vencidos " +
                "FROM usuarios AS us WHERE us.idusuario = " + id, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var metrica = new MetricaPrestamos
                        {
                            IdUsuario = (int)reader["idusuario"],
                            Prestamos = (int)reader["prestamos"],
                            Activos = (int)reader["activos"],
                            Vencidos = (int)reader["vencidos"]
                        };

                        metricas.Add(metrica);
                    }
                }
            }

            return metricas;
        }
    }
}

//llegar hasta aca
