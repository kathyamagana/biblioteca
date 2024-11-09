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
using biblioteca.Controllers;
using biblioteca.Models;
using biblioteca.Views;
using biblioteca.Views.Libros;
//El diseño para el panel de Admin ha finalizado.
namespace biblioteca
{
    public partial class EscritorioAdmin : MaterialForm
    {

        public EscritorioAdmin()
        {
            InitializeComponent();
        }
        private void EscritorioAdmin_Load(object sender, EventArgs e)
        {
            Dashboard Dashboard = new Dashboard();
            Dashboard.TopLevel = false;
            Dashboard.FormBorderStyle = FormBorderStyle.None;
            Dashboard.Dock = DockStyle.Fill;

            tabPage8.Controls.Add(Dashboard);
            Dashboard.Show();

            Usuarios Usuarios = new Usuarios();
            Usuarios.TopLevel = false;
            Usuarios.FormBorderStyle = FormBorderStyle.None;
            Usuarios.Dock = DockStyle.Fill;

            tabPage5.Controls.Add(Usuarios);
            Usuarios.Show();

            Prestamos Prestamos = new Prestamos();
            Prestamos.TopLevel = false;
            Prestamos.FormBorderStyle = FormBorderStyle.None;
            Prestamos.Dock = DockStyle.Fill;

            tabPage6.Controls.Add(Prestamos);
            Prestamos.Show();

            Libros Libros = new Libros();
            Libros.TopLevel = false;
            Libros.FormBorderStyle = FormBorderStyle.None;
            Libros.Dock = DockStyle.Fill;

            tabPage4.Controls.Add(Libros);
            Libros.Show(); 

            Editoriales Editoriales = new Editoriales();
            Editoriales.TopLevel = false;
            Editoriales.FormBorderStyle = FormBorderStyle.None;
            Editoriales.Dock = DockStyle.Fill;

            tabPage1.Controls.Add(Editoriales);
            Editoriales.Show();

            Autores Autores = new Autores();
            Autores.TopLevel = false;
            Autores.FormBorderStyle = FormBorderStyle.None;
            Autores.Dock = DockStyle.Fill;

            tabPage2.Controls.Add(Autores);
            Autores.Show();

            Etiquetas Etiquetas = new Etiquetas();
            Etiquetas.TopLevel = false;
            Etiquetas.FormBorderStyle = FormBorderStyle.None;
            Etiquetas.Dock = DockStyle.Fill;

            tabPage3.Controls.Add(Etiquetas);
            Etiquetas.Show();

            Generos Generos = new Generos();
            Generos.TopLevel = false;
            Generos.FormBorderStyle = FormBorderStyle.None;
            Generos.Dock = DockStyle.Fill;

            tabPage7.Controls.Add(Generos);
            Generos.Show();
        }
    }
}
