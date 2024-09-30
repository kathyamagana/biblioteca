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
    public partial class Generos : MaterialForm
    {
        string connectionString = Conexion.connectionString;
        private GeneroController generoController;

        public Generos()
        {
            InitializeComponent();
        }

        private void Generos_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            CargarGeneros();
        }

        // Cargar datagrid
        private void CargarGeneros()
        {
            generoController = new GeneroController();
            dataGridViewGeneros.DataSource = generoController.Obtener();
            dataGridViewGeneros.ClearSelection();
            dataGridViewGeneros.CurrentCell = null;

            materialCard4.Visible = false;
            materialCard6.Visible = false;
        }

        // Seleccionar
        private void dataGridViewGeneros_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewGeneros.SelectedRows.Count > 0)
            {
                string nombre = dataGridViewGeneros.SelectedRows[0].Cells["Nombre"].Value.ToString();
                string descripcion = dataGridViewGeneros.SelectedRows[0].Cells["Descripcion"].Value.ToString();

                materialTextBox4.Text = nombre;
                materialTextBox5.Text = descripcion;
            }
        }

        // Filtrar
        private void materialTextBox3_TextChanged(object sender, EventArgs e)
        {
            string filtro = materialTextBox3.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                CargarGeneros();
            }
            else
            {
                dataGridViewGeneros.DataSource = generoController.Filtrar(filtro);
            }
        }

        // Crear
        private async void materialButton1_Click(object sender, EventArgs e)
        {
            string nombre = materialTextBox1.Text;
            string descripcion = materialTextBox2.Text;

            if (nombre.Trim() != "" && descripcion.Trim() != "")
            {
                Genero genero = new Genero
                {
                    Nombre = materialTextBox1.Text,
                    Descripcion = materialTextBox2.Text
                };

                generoController.Crear(genero);
                CargarGeneros();

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
            if (dataGridViewGeneros.SelectedRows.Count > 0)
            {
                string nombre = materialTextBox4.Text;
                string descripcion = materialTextBox5.Text;
                
                if (nombre.Trim() != "" && descripcion.Trim() != "")
                {
                    int idGenero = (int)dataGridViewGeneros.SelectedRows[0].Cells["IdGenero"].Value;

                    DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas actualizar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        Genero genero = new Genero
                        {
                            IdGenero = idGenero,
                            Nombre = materialTextBox4.Text,
                            Descripcion = materialTextBox5.Text
                        };

                        generoController.Actualizar(genero);
                        CargarGeneros();

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
            if (dataGridViewGeneros.SelectedRows.Count > 0)
            {
                int idGenero = (int)dataGridViewGeneros.SelectedRows[0].Cells["IdGenero"].Value;

                DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas eliminar este registro, ya no podras recuperarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    generoController.Eliminar(idGenero);
                    CargarGeneros();

                    materialCard6.Visible = true;
                    materialLabel11.Text = "¡Eliminado";

                    await Task.Delay(4000);

                    materialCard6.Visible = false;
                }
            }
        }

    }
}
