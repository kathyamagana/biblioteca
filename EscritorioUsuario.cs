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

namespace biblioteca
{
    public partial class EscritorioUsuario : MaterialForm
    {
        string connectionString = Conexion.connectionString;
        private LibroController libroController;
        private AutorController autorController;
        private GeneroController generoController;
        private EditorialController editorialController;

        public EscritorioUsuario()
        {
            InitializeComponent();
        }

        private void EscritorioUsuario_Load(object sender, EventArgs e)
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
        }

        // Filtrar
        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            string filtro = materialTextBox1.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
            {
                CargarLibros();
            }
            else
            {
                dataGridViewLibros.DataSource = libroController.Filtrar(filtro);
            }
        }

        // Ver detalles del libro
        private void dataGridViewLibros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idLibro = Convert.ToInt32(dataGridViewLibros.Rows[e.RowIndex].Cells["IdLibro"].Value);

                VerLibro verLibro = new VerLibro(idLibro);

                verLibro.ShowDialog();
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Hide();
        }
    }
}
