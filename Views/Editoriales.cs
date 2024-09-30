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

namespace biblioteca.Views
{
    public partial class Editoriales : MaterialForm
    {
        string connectionString = Conexion.connectionString;
        private EditorialController editorialController;

        public Editoriales()
        {
            InitializeComponent();
        }

        private void Editoriales_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            CargarEditoriales();
        }

        // Cargar datagrid
        private void CargarEditoriales()
        {
            editorialController = new EditorialController();
            dataGridViewEditoriales.DataSource = editorialController.Obtener();
            dataGridViewEditoriales.ClearSelection();
            dataGridViewEditoriales.CurrentCell = null;

            materialCard4.Visible = false;
            materialCard6.Visible = false;
        }

        // Seleccionar
        private void dataGridViewEditoriales_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEditoriales.SelectedRows.Count > 0)
            {
                string nombre = dataGridViewEditoriales.SelectedRows[0].Cells["Nombre"].Value.ToString();
                string nacionalidad = dataGridViewEditoriales.SelectedRows[0].Cells["Nacionalidad"].Value.ToString();

                materialTextBox4.Text = nombre;
                materialTextBox5.Text = nacionalidad;
            }
        }

        // Filtrar
        private void materialTextBox3_TextChanged_1(object sender, EventArgs e)
        {
            string filtro = materialTextBox3.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                CargarEditoriales();
            }
            else
            {
                dataGridViewEditoriales.DataSource = editorialController.Filtrar(filtro);
            }
        }

        // Crear
        private async void materialButton1_Click(object sender, EventArgs e)
        {
            string nombre = materialTextBox1.Text;
            string nacionalidad = materialTextBox2.Text;

            if (nombre.Trim() != "" && nacionalidad.Trim() != "")
            {
                Editorial editorial = new Editorial
                {
                    Nombre = materialTextBox1.Text,
                    Nacionalidad = materialTextBox2.Text
                };

                editorialController.Crear(editorial);
                CargarEditoriales();

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
            if (dataGridViewEditoriales.SelectedRows.Count > 0)
            {
                string nombre = materialTextBox4.Text;
                string nacionalidad = materialTextBox5.Text;
                
                if (nombre.Trim() != "" && nacionalidad.Trim() != "")
                {
                    int idEditorial = (int)dataGridViewEditoriales.SelectedRows[0].Cells["IdEditorial"].Value;

                    DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas actualizar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        Editorial editorial = new Editorial
                        {
                            IdEditorial = idEditorial,
                            Nombre = materialTextBox4.Text,
                            Nacionalidad = materialTextBox5.Text
                        };

                        editorialController.Actualizar(editorial);
                        CargarEditoriales();

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
            if (dataGridViewEditoriales.SelectedRows.Count > 0)
            {
                int idEditorial = (int)dataGridViewEditoriales.SelectedRows[0].Cells["IdEditorial"].Value;

                DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas eliminar este registro, ya no podras recuperarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    editorialController.Eliminar(idEditorial);
                    CargarEditoriales();

                    materialCard6.Visible = true;
                    materialLabel11.Text = "¡Eliminado";

                    await Task.Delay(4000);

                    materialCard6.Visible = false;
                }
            }
        }
    }
}
