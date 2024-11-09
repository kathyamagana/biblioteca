using biblioteca.Controllers;
using biblioteca.Models;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Se ha finalizado la interfaz gráfica de Ver Libros.

namespace biblioteca.Views
{
    public partial class VerLibro : Form
    {
        string connectionString = Conexion.connectionString;
        private LibroController libroController;
        private AutorController autorController;
        private GeneroController generoController;
        private EditorialController editorialController;
        private EtiquetaController etiquetaController;
        private ResenaController resenaController;

        private string rutaImagen = "";
        public string nombreArchivo = "default.jpeg";
        string nImagen;

        public VerLibro(int idLibro)
        {
            InitializeComponent();
            IdLibro.Text = idLibro.ToString();
        }

        private void VerLibro_Load(object sender, EventArgs e)
        {
            libroController = new LibroController();

            int idLibro = int.Parse(IdLibro.Text);
            List<DetalleLibro> dLibros = libroController.ObtenerLibro(idLibro);

            if (dLibros.Count > 0)
            {
                DetalleLibro libro = (DetalleLibro)dLibros[0];
                this.Text = libro.Titulo;

                string rutaImagenActual = Path.Combine(Application.StartupPath, "Libros", libro.Imagen);
                nImagen = libro.Imagen;

                pictureBox1.Image = Image.FromFile(rutaImagenActual);
                materialLabel1.Text = libro.Titulo;
                materialLabel22.Text = libro.Descripcion;
                materialLabel3.Text = libro.Autor;
                materialLabel7.Text = libro.Disponibles;
                materialLabel5.Text = libro.Editorial;
                materialLabel4.Text = libro.Ubicacion;

                materialLabel2.Text = "Genero: " + libro.Genero;
                materialLabel6.Text = "ISBN: " + libro.ISBN;
                materialLabel14.Text = "Contenido Visual: " + libro.Contenido_Visual;
                materialLabel8.Text = "Año: " + libro.Ano.ToString();
                materialLabel9.Text = "Idioma: " + libro.Idioma;
                materialLabel12.Text = "Paginas: " + libro.Paginas;
                materialLabel15.Text = "Tomo: " + libro.Tomo;
                materialLabel10.Text = "Tamaño: " + libro.Tamano;
                materialLabel11.Text = "Edicion: " + libro.Edicion;
                materialLabel16.Text = "Serie: " + libro.Serie;




                /*
                numericUpDown3.Text = 
                materialTextBox23.Text = libro.;
                materialTextBox24.Text = libro.Tamano;
                materialTextBox25.Text = libro.;
                materialTextBox26.Text = libro.;
                materialTextBox21.Text = libro.ISBN;
                materialTextBox28.Text = libro.Contenido_Visual;
                materialTextBox29.Text = libro.;
                materialTextBox210.Text = libro.;
                numericUpDown1.Text = libro.Ejemplares;
                materialTextBox211.Text = libro.Ubicacion;
                materialTextBox27.Text = libro.Clasificacion;
                materialTextBox215.Text = libro.Cutter;
                materialTextBox214.Text = libro.Material;
                materialTextBox216.Text = libro.Procedencia;
                */
            }
        }
    }
}
