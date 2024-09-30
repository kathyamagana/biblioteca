using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using biblioteca.Models;
using System.Globalization;

namespace biblioteca.Controllers
{
    internal class DashboardController
    {
        public List<Metrica> Metricas()
        {
            List<Metrica> metricas = new List<Metrica>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT " +
                "(SELECT COUNT(*) FROM libros) AS libros," +
                "(SELECT COUNT(*) FROM usuarios WHERE estado = 1 AND rol = 'usuario') AS usuarios," +
                "(SELECT COUNT(*) FROM prestamos WHERE estado = 'Activo') AS activos," +
                "(SELECT COUNT(*) FROM prestamos WHERE estado = 'Vencido') AS vencidos", connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var metrica = new Metrica
                        {
                            Libros = (int)reader["libros"],
                            Usuarios = (int)reader["usuarios"],
                            Activos = (int)reader["activos"],
                            Vencidos = (int)reader["vencidos"]
                        };

                        metricas.Add(metrica);
                    }
                }
            }

            return metricas;
        }

        public List<MesesMetrica> Graficos()
        {
            List<MesesMetrica> metricas = new List<MesesMetrica>();

            using (SqlConnection connection = new SqlConnection(Conexion.connectionString))
            {
                connection.Open();

                int anoActual = DateTime.Now.Year;
                CultureInfo culture = new CultureInfo("es-ES");

                using (SqlCommand cmd = new SqlCommand(
                    "WITH Months AS ( " +
                    "    SELECT 1 AS MonthNumber UNION ALL " +
                    "    SELECT 2 UNION ALL " +
                    "    SELECT 3 UNION ALL " +
                    "    SELECT 4 UNION ALL " +
                    "    SELECT 5 UNION ALL " +
                    "    SELECT 6 UNION ALL " +
                    "    SELECT 7 UNION ALL " +
                    "    SELECT 8 UNION ALL " +
                    "    SELECT 9 UNION ALL " +
                    "    SELECT 10 UNION ALL " +
                    "    SELECT 11 UNION ALL " +
                    "    SELECT 12 " +
                    ") " +
                    "SELECT " +
                    "    m.MonthNumber AS Mes, " +
                    "    DATENAME(MONTH, DATEFROMPARTS(@ano, m.MonthNumber, 1)) AS NombreMes, " +
                    "    COUNT(p.fechaprestamo) AS TotalPrestamos " +
                    "FROM Months m " +
                    "LEFT JOIN prestamos p ON YEAR(p.fechaprestamo) = @ano AND DATEPART(MONTH, p.fechaprestamo) = m.MonthNumber " +
                    "GROUP BY m.MonthNumber, DATENAME(MONTH, DATEFROMPARTS(@ano, m.MonthNumber, 1)) " +
                    "ORDER BY m.MonthNumber;", connection))
                {
                    cmd.Parameters.AddWithValue("@ano", anoActual);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var mesMetrica = new MesesMetrica
                            {
                                Mes = (int)reader["Mes"],
                                NombreMes = (string)reader["NombreMes"],
                                TotalPrestamos = (int)reader["TotalPrestamos"]
                            };

                            metricas.Add(mesMetrica);
                        }
                    }
                }
            }

            return metricas;
        }
    }
}
