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
using System.Windows.Forms.DataVisualization.Charting;

namespace biblioteca.Views
{
    public partial class Dashboard : MaterialForm
    {
        string connectionString = Conexion.connectionString;
        private DashboardController dashboardController;

        public Dashboard()
        {
            InitializeComponent();
            MostrarGraficoDeBarras();
            MostrarMetricas();
        }

        private void MostrarGraficoDeBarras()
        {
            dashboardController = new DashboardController();

            List<MesesMetrica> metricas = dashboardController.Graficos();

            ChartArea chartArea = chart1.ChartAreas[0];
            chart1.Series.Clear();

            Series series = new Series("TotalPrestamos");
            series.ChartType = SeriesChartType.Bar;

            foreach (var metrica in metricas)
            {
                DataPoint dataPoint = new DataPoint();
                dataPoint.AxisLabel = metrica.NombreMes;
                dataPoint.YValues = new double[] { metrica.TotalPrestamos };
                series.Points.Add(dataPoint);
            }

            chart1.Series.Add(series);
            chartArea.AxisX.Title = "Mes";
            chartArea.AxisY.Title = "Total de Préstamos";
            chartArea.AxisX.Interval = 1;

            chart1.Invalidate();
        }

        private void MostrarMetricas()
        {
            dashboardController = new DashboardController();

            List<Metrica> metricas = dashboardController.Metricas();

            if (metricas.Count > 0)
            {
                var primerResultado = metricas[0];

                materialLabel6.Text = primerResultado.Libros.ToString();
                materialLabel7.Text = primerResultado.Usuarios.ToString();
                materialLabel8.Text = primerResultado.Activos.ToString();
                materialLabel9.Text = primerResultado.Vencidos.ToString();
            }

            chart2.Series.Clear();
            Series series = new Series("Prestamos");
            series.ChartType = SeriesChartType.Pie;
            series.Points.AddXY("Activos", Convert.ToDouble(materialLabel8.Text));
            series.Points.AddXY("Vencidos", Convert.ToDouble(materialLabel9.Text));
            chart2.Series.Add(series);

            ChartArea chartArea = chart2.ChartAreas[0];
            chartArea.AxisX.Title = "Estado de Prestamos";
            chartArea.AxisY.Title = "Cantidad";
            chartArea.AxisX.Interval = 1;
        }
    }
}
