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
//Se ha finalizado la interfaz gráfica de Autores.
namespace biblioteca.Views
{
    public partial class Autores : MaterialForm
    {
        string connectionString = Conexion.connectionString;
        private AutorController autorController;

        public Autores()
        {
            InitializeComponent();
        }

        private void Autores_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            CargarAutores();
        }

        // Cargar datagrid
        private void CargarAutores()
        {
            autorController = new AutorController();
            dataGridViewAutores.DataSource = autorController.Obtener();
            dataGridViewAutores.ClearSelection();
            dataGridViewAutores.CurrentCell = null;

            materialCard4.Visible = false;
            materialCard6.Visible = false;
        }

        // Seleccionar
        private void dataGridViewAutores_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewAutores.SelectedRows.Count > 0)
            {
                string nombre = dataGridViewAutores.SelectedRows[0].Cells["Nombre"].Value.ToString();
                string nacionalidad = dataGridViewAutores.SelectedRows[0].Cells["Nacionalidad"].Value.ToString();

                materialTextBox4.Text = nombre;
                materialTextBox5.Text = nacionalidad;
            }
        }

        // Filtrar
        private void materialTextBox3_TextChanged(object sender, EventArgs e)
        {
            string filtro = materialTextBox3.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                CargarAutores();
            }
            else
            {
                dataGridViewAutores.DataSource = autorController.Filtrar(filtro);
            }
        }
        
        // Crear
        private async void materialButton1_Click_1(object sender, EventArgs e)
        {
            string nombre = materialTextBox1.Text;
            string nacionalidad = materialTextBox2.Text;

            if (nombre.Trim() != "" && nacionalidad.Trim() != "")
            {
                Autor autor = new Autor
                {
                    Nombre = materialTextBox1.Text,
                    Nacionalidad = materialTextBox2.Text
                };

                autorController.Crear(autor);
                CargarAutores();

                materialTextBox1.Text = "";
                materialTextBox2.Text = "";

                materialCard4.Visible = true;

                await Task.Delay(4000);

                materialCard4.Visible = false;
            }
            else
            {
                MessageBox.Show("Por favor completa el formulario...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Editar
        private async void materialButton2_Click(object sender, EventArgs e)
        {
            if (dataGridViewAutores.SelectedRows.Count > 0)
            {
                string nombre = materialTextBox4.Text;
                string nacionalidad = materialTextBox5.Text;
                
                if (nombre.Trim() != "" && nacionalidad.Trim() != "")
                {
                    int idAutor = (int)dataGridViewAutores.SelectedRows[0].Cells["IdAutor"].Value;

                    DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas actualizar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        Autor autor = new Autor
                        {
                            IdAutor = idAutor,
                            Nombre = materialTextBox4.Text,
                            Nacionalidad = materialTextBox5.Text
                        };

                        autorController.Actualizar(autor);
                        CargarAutores();

                        materialCard6.Visible = true;
                        materialLabel11.Text = "¡Actualizado";

                        await Task.Delay(4000);

                        materialCard6.Visible = false;
                    }
                }else{
                    MessageBox.Show("Por favor completa el formulario...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              
            }
        }

        // Eliminar
        private async void materialButton4_Click(object sender, EventArgs e)
        {
            if (dataGridViewAutores.SelectedRows.Count > 0)
            {
                int idAutor = (int)dataGridViewAutores.SelectedRows[0].Cells["IdAutor"].Value;

                DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas eliminar este registro, ya no podras recuperarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    autorController.Eliminar(idAutor);
                    CargarAutores();

                    materialCard6.Visible = true;
                    materialLabel11.Text = "¡Eliminado";

                    await Task.Delay(4000);

                    materialCard6.Visible = false;
                }
            }
        }
    }
}
