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

namespace biblioteca.Views.Libros
{
    public partial class Libros : MaterialForm
    {
        private string rutaImagen;
        public string nombreArchivo = "default.jpeg";

        string connectionString = Conexion.connectionString;
        private LibroController libroController;
        private AutorController autorController;
        private GeneroController generoController;
        private EditorialController editorialController;

        public Libros()
        {
            InitializeComponent();
        }

        private void Libros_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            CargarLibros();
        }

        // Cargar datagrid
        private void CargarLibros()
        {
            libroController = new LibroController();
            dataGridViewLibros.DataSource = libroController.Obtener();
            dataGridViewLibros.ClearSelection();
            dataGridViewLibros.CurrentCell = null;

            materialCard3.Visible = false;
            materialCard4.Visible = false;
        }

        // Filtrar
        private void materialTextBox3_TextChanged_1(object sender, EventArgs e)
        {
            string filtro = materialTextBox3.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                CargarLibros();
            }
            else
            {
                dataGridViewLibros.DataSource = libroController.Filtrar(filtro);
            }
        }

        // Hace visible crear nuevo
        private void materialButton1_Click(object sender, EventArgs e)
        {
            materialCard2.Visible = false;
            materialCard3.Visible = true;

            materialButton1.Visible = false;
            materialButton3.Visible = true;
            materialButton4.Visible = true;

            CargarCombos();
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
        }
        
        private void materialButton2_Click(object sender, EventArgs e)
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
        // Cancelar
        private void materialButton4_Click(object sender, EventArgs e)
        {
            materialCard2.Visible = true;
            materialCard3.Visible = false;

            materialButton1.Visible = true;
            materialButton3.Visible = false;
            materialButton4.Visible = false;
        }

        // Guardar
        private async void materialButton3_Click(object sender, EventArgs e)
        {
            string Titulo = materialTextBox212.Text;
            string ISBN = materialTextBox21.Text;
            string Ejemplares = numericUpDown1.Text;
            string Disponibles = numericUpDown2.Text;
            string Descripcion = materialTextBox213.Text;

            if (Titulo.Trim() != "" && Descripcion.Trim() != "" && ISBN.Trim() != "" && int.Parse(Ejemplares) > 0 && int.Parse(Disponibles) > 0)
            {
                if (!string.IsNullOrEmpty(rutaImagen))
                {
                    nombreArchivo = Path.GetFileName(rutaImagen);
                    string destino = Path.Combine(Application.StartupPath, "Libros", nombreArchivo);

                    if (!File.Exists(destino))
                    {
                        File.Copy(rutaImagen, destino);
                    }
                }

                Libro libro = new Libro
                {
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
                    Imagen = nombreArchivo,
                    Procedencia = materialTextBox216.Text,
                };

                libroController.Crear(libro);

                materialCard4.Visible = true;

                await Task.Delay(4000);

                materialCard2.Visible = true;
                materialCard3.Visible = false;

                materialButton1.Visible = true;
                materialButton3.Visible = false;
                materialButton4.Visible = false;

                CargarLibros();

                materialCard4.Visible = false;

                materialComboBox1.SelectedIndex = -1;
                materialComboBox2.SelectedIndex = -1;
                materialComboBox3.SelectedIndex = -1;
                materialTextBox212.Text = string.Empty;
                numericUpDown3.Value = 1000;
                materialTextBox23.Text = string.Empty;
                materialTextBox24.Text = string.Empty;
                materialTextBox25.Text = string.Empty;
                materialTextBox26.Text = string.Empty;
                materialTextBox21.Text = string.Empty;
                materialTextBox28.Text = string.Empty;
                materialTextBox29.Text = string.Empty;
                materialTextBox210.Text = string.Empty;
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
                materialComboBox5.SelectedIndex = -1;
                materialTextBox211.Text = string.Empty;
                materialTextBox27.Text = string.Empty;
                materialTextBox215.Text = string.Empty;
                materialTextBox214.Text = string.Empty;
                materialTextBox213.Text = string.Empty;
                nombreArchivo = string.Empty;
                materialTextBox216.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Por favor completa el formulario...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewLibros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idLibro = Convert.ToInt32(dataGridViewLibros.Rows[e.RowIndex].Cells["IdLibro"].Value);

                DetallesLibro detallesLibro = new DetallesLibro(idLibro);
                
                detallesLibro.ShowDialog();
            }
        }
    }
}


