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
    public partial class Usuarios : MaterialForm
    {
        string connectionString = Conexion.connectionString;
        private UsuarioController usuarioController;

        public Usuarios()
        {
            InitializeComponent();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            CargarUsuarios();

            dataGridViewUsuarios.AllowUserToAddRows = false;
            dataGridViewUsuarios.AllowUserToDeleteRows = false;
            dataGridViewUsuarios.ReadOnly = true;
        }

        // Cargar datagrid
        private void CargarUsuarios()
        {
            usuarioController = new UsuarioController();
            dataGridViewUsuarios.DataSource = usuarioController.Obtener("todos");
            dataGridViewUsuarios.ClearSelection();
            dataGridViewUsuarios.CurrentCell = null;

            var columnaContrasena = dataGridViewUsuarios.Columns["Contrasena"];

            if (columnaContrasena != null)
            {
                columnaContrasena.Visible = false;
            }

            materialCard3.Visible = false;
            materialCard4.Visible = false;
        }

        // Filtrar
        private void materialTextBox3_TextChanged(object sender, EventArgs e)
        {
            string filtro = materialTextBox3.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                CargarUsuarios();
            }
            else
            {
                dataGridViewUsuarios.DataSource = usuarioController.Filtrar(filtro, "todos");
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
            string Nombre = materialTextBox212.Text;
            string Apellido = materialTextBox213.Text;
            string Email = materialTextBox29.Text;
            string Direccion = materialTextBox26.Text;
            string Contrasena = materialTextBox216.Text;

            if (Nombre.Trim() != "" && Apellido.Trim() != "" && Email.Trim() != "" && Direccion.Trim() != "")
            {
                string contra = "";
                if(Contrasena.Trim() == ""){
                    contra = materialTextBox29.Text;
                }else{
                    contra = materialTextBox216.Text;
                }
               
                Usuario usuario = new Usuario
                {
                    Rol = materialComboBox5.SelectedItem.ToString(),
                    Nombre = materialTextBox212.Text,
                    Apellido = materialTextBox213.Text,
                    DUI = materialTextBox24.Text,
                    NIE = materialTextBox21.Text,
                    Genero = materialComboBox1.SelectedItem.ToString(),
                    Telefono = materialTextBox23.Text,
                    Email = materialTextBox29.Text,
                    Contrasena = contra,
                    Direccion = materialTextBox26.Text,
                    Institucion = materialTextBox27.Text,
                    Estado = "1"
                };

                usuarioController.Crear(usuario);

                materialCard4.Visible = true;

                await Task.Delay(4000);

                materialCard2.Visible = true;
                materialCard3.Visible = false;

                materialButton1.Visible = true;
                materialButton3.Visible = false;
                materialButton4.Visible = false;

                CargarUsuarios();

                materialCard4.Visible = false;

                materialComboBox5.SelectedIndex = -1;
                materialComboBox1.SelectedIndex = -1;
                materialTextBox212.Text = string.Empty;
                materialTextBox23.Text = string.Empty;
                materialTextBox24.Text = string.Empty;
                materialTextBox26.Text = string.Empty;
                materialTextBox21.Text = string.Empty;
                materialTextBox29.Text = string.Empty;
                materialComboBox5.SelectedIndex = -1;
                materialTextBox27.Text = string.Empty;
                materialTextBox213.Text = string.Empty;
                materialTextBox216.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Por favor completa el formulario...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idUsuario = Convert.ToInt32(dataGridViewUsuarios.Rows[e.RowIndex].Cells["IdUsuario"].Value);

                DetallesUsuario detallesUsuario = new DetallesUsuario(idUsuario);
                
                detallesUsuario.ShowDialog();
            }
        }
    }
}
