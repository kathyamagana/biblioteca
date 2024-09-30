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
    public partial class Etiquetas : MaterialForm
    {
        string connectionString = Conexion.connectionString;
        private EtiquetaController etiquetaController;
        
        public Etiquetas()
        {
            InitializeComponent();
        }

        private void Etiquetas_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            CargarEtiquetas();
        }

        // Cargar datagrid
        private void CargarEtiquetas()
        {
            etiquetaController = new EtiquetaController();
            dataGridViewEtiquetas.DataSource = etiquetaController.Obtener();
            dataGridViewEtiquetas.ClearSelection();
            dataGridViewEtiquetas.CurrentCell = null;

            materialCard4.Visible = false;
            materialCard6.Visible = false;
        }

        // Seleccionar
        private void dataGridViewEtiquetas_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEtiquetas.SelectedRows.Count > 0)
            {
                string nombre = dataGridViewEtiquetas.SelectedRows[0].Cells["Nombre"].Value.ToString();

                materialTextBox4.Text = nombre;
            }
        }

        // Filtrar
        private void materialTextBox3_TextChanged(object sender, EventArgs e)
        {
            string filtro = materialTextBox3.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                CargarEtiquetas();
            }
            else
            {
                dataGridViewEtiquetas.DataSource = etiquetaController.Filtrar(filtro);
            }
        }

        // Crear
        private async void materialButton1_Click(object sender, EventArgs e)
        {
            string nombre = materialTextBox1.Text;

            if (nombre.Trim() != "")
            {
                Etiqueta etiqueta = new Etiqueta
                {
                    Nombre = materialTextBox1.Text,
                };

                etiquetaController.Crear(etiqueta);
                CargarEtiquetas();

                materialTextBox1.Text = "";

                materialCard4.Visible = true;

                await Task.Delay(4000);

                materialCard4.Visible = false;
            }
            else
            {
                MessageBox.Show("Por favor completa el formulario...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Actualizar
        private async void materialButton2_Click(object sender, EventArgs e)
        {
            if (dataGridViewEtiquetas.SelectedRows.Count > 0)
            {
                string nombre = materialTextBox4.Text;
                
                if (nombre.Trim() != "")
                {
                    int idEtiqueta = (int)dataGridViewEtiquetas.SelectedRows[0].Cells["IdEtiqueta"].Value;

                    DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas actualizar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        Etiqueta etiqueta = new Etiqueta
                        {
                            IdEtiqueta = idEtiqueta,
                            Nombre = materialTextBox4.Text
                        };

                        etiquetaController.Actualizar(etiqueta);
                        CargarEtiquetas();

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
            if (dataGridViewEtiquetas.SelectedRows.Count > 0)
            {
                int idEtiqueta = (int)dataGridViewEtiquetas.SelectedRows[0].Cells["IdEtiqueta"].Value;

                DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas eliminar este registro, ya no podras recuperarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    etiquetaController.Eliminar(idEtiqueta);
                    CargarEtiquetas();

                    materialCard6.Visible = true;
                    materialLabel11.Text = "¡Eliminado";

                    await Task.Delay(4000);

                    materialCard6.Visible = false;
                }
            }
        }
    }
}
