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

using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using biblioteca.Views;

namespace biblioteca
{
    public partial class Login : MaterialForm
    {
        string connectionString = Conexion.connectionString;
        public Login()
        {
            InitializeComponent();
        }
        private void materialButton2_Click(object sender, EventArgs e)
        {
            if(materialTextBox21.Text == "" || materialTextBox22.Text == "")
            {
                label1.Visible = true;
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);

                string email = materialTextBox21.Text;
                string contrasena = CalcularMD5(materialTextBox22.Text);

                string consulta = "SELECT * FROM usuarios WHERE email = @email AND contrasena = @contrasena AND estado = 1";
                SqlCommand cmd = new SqlCommand(consulta, connection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@contrasena", contrasena);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string rol = reader["rol"].ToString();
                    reader.Close();
                    connection.Close();

                    switch (rol)
                    {
                        case "admin":
                            EscritorioAdmin EscritorioAdmin = new EscritorioAdmin();
                            EscritorioAdmin.Show();
                            this.Hide();
                            break;
                        case "bibliotecario":
                            EscritorioBibliotecario EscritorioBibliotecario = new EscritorioBibliotecario();
                            EscritorioBibliotecario.Show();
                            this.Hide();
                            break;
                        default:
                            MessageBox.Show("Credenciales incorrectas, por favor intenta de nuevo...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    MessageBox.Show("Credenciales incorrectas, por favor intenta de nuevo...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string CalcularMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            EscritorioUsuario EscritorioUsuario = new EscritorioUsuario();
            EscritorioUsuario.Show();
            this.Hide();
        }

    }
}
