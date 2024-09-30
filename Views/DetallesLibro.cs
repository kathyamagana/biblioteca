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

namespace biblioteca.Views
{
    public partial class DetallesLibro : Form
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

        public DetallesLibro(int idLibro)
        {
            InitializeComponent();
            IdLibroDetalle.Text = idLibro.ToString();
        }

        private void CargarCombos()
        {
            autorController = new AutorController();
            materialComboBox1.DataSource = autorController.Obtener();
            materialComboBox1.ValueMember = "IdAutor";
            materialComboBox1.DisplayMember = "Nombre";

            generoController = new GeneroController();
            materialComboBox2.DataSource = generoController.Obtener();
            materialComboBox2.ValueMember = "IdGenero";
            materialComboBox2.DisplayMember = "Nombre";

            editorialController = new EditorialController();
            materialComboBox3.DataSource = editorialController.Obtener();
            materialComboBox3.ValueMember = "IdEditorial";
            materialComboBox3.DisplayMember = "Nombre";

            etiquetaController = new EtiquetaController();
            materialComboBox4.DataSource = etiquetaController.Obtener();
            materialComboBox4.ValueMember = "IdEtiqueta";
            materialComboBox4.DisplayMember = "Nombre";
            
        }

        private void DetallesLibro_Load(object sender, EventArgs e)
        {
            materialCard3.Visible = false;
            materialCard4.Visible = false;
            materialCard5.Visible = false;
            materialButton5.Visible = false;

            CargarCombos();
            libroController = new LibroController();

            int idLibro = int.Parse(IdLibroDetalle.Text);
            List<DetalleLibro> dLibros = libroController.ObtenerLibro(idLibro);

            if (dLibros.Count > 0)
            {
                DetalleLibro libro = (DetalleLibro)dLibros[0];
                this.Text = libro.Titulo;

                string rutaImagenActual = Path.Combine(Application.StartupPath, "Libros", libro.Imagen);
                nImagen = libro.Imagen;

                pictureBox1.Image = Image.FromFile(rutaImagenActual);
                materialTextBox212.Text = libro.Titulo;
                numericUpDown3.Text = libro.Ano.ToString();
                materialTextBox23.Text = libro.Idioma;
                materialTextBox24.Text = libro.Tamano;
                materialTextBox25.Text = libro.Edicion;
                materialTextBox26.Text = libro.Paginas;
                materialTextBox21.Text = libro.ISBN;
                materialTextBox28.Text = libro.Contenido_Visual;
                materialTextBox29.Text = libro.Tomo;
                materialTextBox210.Text = libro.Serie;
                numericUpDown1.Text = libro.Ejemplares;
                numericUpDown2.Text = libro.Disponibles;
                materialTextBox211.Text = libro.Ubicacion;
                materialTextBox27.Text = libro.Clasificacion;
                materialTextBox215.Text = libro.Cutter;
                materialTextBox214.Text = libro.Material;
                materialTextBox213.Text = libro.Descripcion;
                materialTextBox216.Text = libro.Procedencia;
            }
        }

        // Cambiar Imagen
        private void materialButton3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Archivos de imagen|*.jpg;*.png;*.bmp|Todos los archivos|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    rutaImagen = openFileDialog.FileName;
                    pictureBox1.Image = Image.FromFile(rutaImagen);
                }
            }
        }

        // Actualizar
        private async void materialButton1_Click(object sender, EventArgs e)
        {
            string Titulo = materialTextBox212.Text;
            string ISBN = materialTextBox21.Text;
            string Ejemplares = numericUpDown1.Text;
            string Disponibles = numericUpDown2.Text;
            string Descripcion = materialTextBox213.Text;

            if (Titulo.Trim() != "" && Descripcion.Trim() != "" && ISBN.Trim() != "" && int.Parse(Ejemplares) > 0 && int.Parse(Disponibles) > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas actualizar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(rutaImagen))
                    {
                        nImagen = Path.GetFileName(rutaImagen);
                        string destino = Path.Combine(Application.StartupPath, "Libros", nImagen);

                        if (!File.Exists(destino))
                        {
                            File.Copy(rutaImagen, destino);
                        }
                    }

                    Libro libro = new Libro
                    {
                        IdLibro = int.Parse(IdLibroDetalle.Text),
                        IdAutor = int.Parse(materialComboBox1.SelectedValue.ToString()),
                        IdGenero = int.Parse(materialComboBox2.SelectedValue.ToString()),
                        IdEditorial = int.Parse(materialComboBox3.SelectedValue.ToString()),
                        Titulo = materialTextBox212.Text,
                        Ano = int.Parse(numericUpDown3.Text),
                        Idioma = materialTextBox23.Text,
                        Tamano = materialTextBox24.Text,
                        Edicion = materialTextBox25.Text,
                        Paginas = materialTextBox26.Text,
                        ISBN = materialTextBox21.Text,
                        Contenido_Visual = materialTextBox28.Text,
                        Tomo = materialTextBox29.Text,
                        Serie = materialTextBox210.Text,
                        Ejemplares = numericUpDown1.Text,
                        Disponibles = numericUpDown2.Text,
                        Estado = materialComboBox5.SelectedItem.ToString(),
                        Ubicacion = materialTextBox211.Text,
                        Clasificacion = materialTextBox27.Text,
                        Cutter = materialTextBox215.Text,
                        Material = materialTextBox214.Text,
                        Descripcion = materialTextBox213.Text,
                        Imagen = nImagen,
                        Procedencia = materialTextBox216.Text,
                    };

                    libroController.Actualizar(libro);

                    materialCard6.Visible = true;
                    materialLabel11.Text = "¡Actualizado";

                    await Task.Delay(4000);

                    materialCard6.Visible = false;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor completa el formulario...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // Eliminar
        private async void materialButton2_Click(object sender, EventArgs e)
        {   
            int idLibro = int.Parse(IdLibroDetalle.Text);

            DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas eliminar este registro, ya no podras recuperarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                libroController.Eliminar(idLibro);

                materialCard6.Visible = true;
                materialLabel1.Text = "¡Eliminado";

                await Task.Delay(4000);

                materialCard6.Visible = false;
                this.Close();
            }
        }

        // Reseñas y Etiquetas
        private void materialButton4_Click(object sender, EventArgs e)
        {
            CargarResenas();
            CargarEtiquetas();

            materialCard1.Visible = false;
            materialCard2.Visible = false;
            materialButton1.Visible = false;
            materialButton2.Visible = false;
            materialButton4.Visible = false;
            materialButton5.Visible = true;

            materialCard3.Visible = true;
            materialCard4.Visible = true;
            materialCard5.Visible = true;
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            materialCard1.Visible = true;
            materialCard2.Visible = true;
            materialButton1.Visible = true;
            materialButton2.Visible = true;
            materialButton4.Visible = true;
            materialButton5.Visible = false;

            materialCard3.Visible = false;
            materialCard4.Visible = false;
            materialCard5.Visible = false;
        }

        // Cargar Reseñas
        private void CargarResenas()
        {
            int idLibro = int.Parse(IdLibroDetalle.Text);
            resenaController = new ResenaController();
            dataGridViewResenas.DataSource = resenaController.Obtener(idLibro);
        }

        // Eliminar Resena
        private async void materialButton6_Click(object sender, EventArgs e)
        {
            if (dataGridViewResenas.SelectedRows.Count > 0)
            {
                int idResena = (int)dataGridViewResenas.SelectedRows[0].Cells["IdResena"].Value;

                DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas eliminar este registro, ya no podras recuperarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    resenaController.Eliminar(idResena);
                    CargarResenas();
                }
            }
        }

        // Cargar Etiquetas
        private void CargarEtiquetas()
        {
            int idLibro = int.Parse(IdLibroDetalle.Text);
            libroController = new LibroController();
            dataGridViewEtiquetas.DataSource = libroController.ObtenerEtiquetas(idLibro);
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            if (dataGridViewEtiquetas.SelectedRows.Count > 0)
            {
                int id = (int)dataGridViewEtiquetas.SelectedRows[0].Cells["Id"].Value;

                DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas eliminar este registro, ya no podras recuperarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    libroController.EliminarEtiqueta(id);
                    CargarEtiquetas();
                }
            }
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            int idLibro = int.Parse(IdLibroDetalle.Text);
            int idEtiqueta = int.Parse(materialComboBox4.SelectedValue.ToString());

            libroController.AnadirEtiqueta(idLibro, idEtiqueta);
            CargarEtiquetas();

        }
    }
}
