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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace biblioteca.Views
{
    public partial class Prestamos : MaterialForm
    {
        string connectionString = Conexion.connectionString;
        private LibroController libroController;
        private UsuarioController usuarioController;
        private PrestamoController prestamoController;

        public Prestamos()
        {
            InitializeComponent();
        }

        private void Prestamos_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            CargarDatos();
        }

        // Cargar
        private void CargarDatos()
        {
            libroController = new LibroController();
            materialComboBox4.DataSource = libroController.ObtenerLibros();
            materialComboBox4.DisplayMember = "Titulo";
            materialComboBox4.ValueMember = "IdLibro";

            usuarioController = new UsuarioController();

            prestamoController = new PrestamoController();
            materialComboBox1.DataSource = prestamoController.ObtenerUsuariosValidos();
            materialComboBox1.ValueMember = "IdUsuario";
            materialComboBox1.DisplayMember = "Nombre";

            dataGridViewPrestamos.DataSource = prestamoController.Obtener();
            dataGridViewPrestamos.ClearSelection();
            dataGridViewPrestamos.CurrentCell = null;
        }

        // Crear
        private void materialButton1_Click(object sender, EventArgs e)
        {
            string usuario = materialComboBox1.SelectedValue.ToString();

            if (usuario.Trim() != "")
            {
                int idUsuario = int.Parse(materialComboBox1.SelectedValue.ToString());

                Prestamo prestamo = new Prestamo
                {
                    IdUsuario = idUsuario,
                    FechaPrestamo = dateTimePicker1.Value,
                    FechaEntrega = dateTimePicker2.Value,
                    Estado = "Activo",
                    Comentarios = materialTextBox21.Text,
                    Tipo = materialComboBox3.SelectedItem.ToString()
                };

                int idLibro = int.Parse(materialComboBox4.SelectedValue.ToString());

                prestamoController.Crear(prestamo, idLibro);
                MessageBox.Show("Prestamo Registrado.");
                CargarDatos();
            }
            else
            {
                MessageBox.Show("Por favor completa el formulario...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Filtrar
        private void materialTextBox3_TextChanged(object sender, EventArgs e)
        {
            string filtro = materialTextBox3.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                dataGridViewPrestamos.DataSource = prestamoController.Obtener();
            }
            else
            {
                dataGridViewPrestamos.DataSource = prestamoController.Filtrar(filtro);
            }
        }

        // Seleccionar
        private void dataGridViewPrestamos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewPrestamos.SelectedRows.Count > 0)
            {
                //string nombre = dataGridViewPrestamos.SelectedRows[0].Cells["Nombre"].Value.ToString();

                materialTextBox22.Text = dataGridViewPrestamos.SelectedRows[0].Cells["Comentarios"].Value.ToString();
            }
        }

        // Actualizar
        private void materialButton2_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Deseas actualizar este prestamo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                prestamoController.Actualizar(DateTime.Parse(dateTimePicker4.Text), materialTextBox22.Text, materialComboBox2.SelectedItem.ToString(), int.Parse(dataGridViewPrestamos.SelectedRows[0].Cells["IdPrestamo"].Value.ToString()));

                MessageBox.Show("Prestamo Actualizado.");
                CargarDatos();
            }
        }

        // Eliminar
        private void materialButton3_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Deseas eliminar este prestamo?, no podras recuperar el registro", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                prestamoController.Eliminar(int.Parse(dataGridViewPrestamos.SelectedRows[0].Cells["IdPrestamo"].Value.ToString()));

                MessageBox.Show("Prestamo Eliminado.");
                CargarDatos();
            }
        }
    }
}
