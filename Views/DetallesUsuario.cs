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

using System.Security.Cryptography;

namespace biblioteca.Views
{
    public partial class DetallesUsuario : Form
    {
        string connectionString = Conexion.connectionString;
        private UsuarioController usuarioController;
        private PrestamoController prestamoController;

        public DetallesUsuario(int idUsuario)
        {
            InitializeComponent();
            IdUsuarioDetalle.Text = idUsuario.ToString();
        }

        private void DetallesUsuario_Load(object sender, EventArgs e)
        {
            materialCard6.Visible = false;
            materialCard4.Visible = false;
            materialButton4.Visible = false;

            usuarioController = new UsuarioController();

            int idUsuario = int.Parse(IdUsuarioDetalle.Text);
            List<Usuario> dLUsuario = usuarioController.ObtenerUsuario(idUsuario);

            if (dLUsuario.Count > 0)
            {
                Usuario usuario = (Usuario)dLUsuario[0];
                this.Text = usuario.Nombre;

                materialTextBox212.Text = usuario.Nombre;
                materialTextBox213.Text = usuario.Apellido;
                materialTextBox24.Text = usuario.DUI;
                materialTextBox21.Text = usuario.NIE;
                materialComboBox1.SelectedItem = usuario.Genero;
                materialTextBox23.Text = usuario.Telefono;
                materialTextBox29.Text = usuario.Email;
                materialTextBox26.Text = usuario.Direccion;
                materialTextBox27.Text = usuario.Institucion;

                materialLabel17.Text = usuario.Rol;
                materialLabel17.Visible = false;

                materialTextBox1.Text = usuario.Contrasena;
                materialTextBox1.Visible = false;


                if (usuario.Rol == "bibliotecario"){
                    materialCard1.Visible = false;
                    materialCard2.Visible = false;
                    materialCard5.Visible = false;
                    materialButton3.Visible = false;
                }else{
                    List<MetricaPrestamos> metricaPrestamos = usuarioController.MetricaPrestamos(idUsuario);

                    if (metricaPrestamos.Count > 0)
                    {
                        MetricaPrestamos metrica = metricaPrestamos[0];
                        materialLabel7.Text = metrica.Prestamos.ToString(); 
                        materialLabel8.Text = metrica.Activos.ToString();
                        materialLabel11.Text = metrica.Vencidos.ToString();
                    }

                    materialTextBox216.Visible = false;
                    materialLabel19.Visible = false;
                    materialLabel25.Visible = false;
                }
            }
        }

        // Actualizar
        private async void materialButton1_Click(object sender, EventArgs e)
        {
            string Nombre = materialTextBox212.Text;
            string Apellido = materialTextBox213.Text;
            string Email = materialTextBox29.Text;
            string Direccion = materialTextBox26.Text;

            if (Nombre.Trim() != "" && Apellido.Trim() != "" && Email.Trim() != "" && Direccion.Trim() != "")
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas actualizar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    string contrasenaEncriptada = "";

                    if(materialTextBox216.Text != ""){
                        contrasenaEncriptada = materialTextBox1.Text;
                    }else{
                        contrasenaEncriptada = CrearMD5(materialTextBox216.Text);
                    }

                    string estado = "1";
                    if (materialComboBox2.SelectedItem.ToString() == "Activo")
                    {
                        estado = "1";
                    }
                    if (materialComboBox2.SelectedItem.ToString() == "Inactivo")
                    {
                        estado = "2";
                    }
                    if (materialComboBox2.SelectedItem.ToString() == "Baneado")
                    {
                        estado = "3";
                    }

                    Usuario usuario = new Usuario
                    {
                        IdUsuario = int.Parse(IdUsuarioDetalle.Text),
                        Nombre = materialTextBox212.Text,
                        Apellido = materialTextBox213.Text,
                        DUI = materialTextBox24.Text,
                        NIE = materialTextBox21.Text,
                        Genero = materialComboBox1.SelectedItem.ToString(),
                        Telefono = materialTextBox23.Text,
                        Email = materialTextBox29.Text,
                        Direccion = materialTextBox26.Text,
                        Institucion = materialTextBox27.Text,
                        Rol = materialLabel17.Text,
                        Estado = estado,
                        Contrasena = contrasenaEncriptada
                    };

                    usuarioController.Actualizar(usuario);

                    materialCard6.Visible = true;
                    materialLabel14.Text = "¡Actualizado";

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

        private string CrearMD5(string input)
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

        // Eliminar
        private async void materialButton2_Click(object sender, EventArgs e)
        {
            int idUsuario = int.Parse(IdUsuarioDetalle.Text);

            DialogResult resultado = MessageBox.Show("¿Está seguro de que deseas eliminar este registro, ya no podras recuperarlo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                usuarioController.Eliminar(idUsuario);

                materialCard6.Visible = true;
                materialLabel14.Text = "¡Eliminado";

                await Task.Delay(4000);

                materialCard6.Visible = false;
                this.Close();
            }
        }

        // Ver historial de prestamos
        private void materialButton3_Click(object sender, EventArgs e)
        {
            materialCard4.Visible = true;
            materialCard3.Visible = false;
            materialButton1.Visible = false;
            materialButton2.Visible = false;
            materialButton4.Visible = true;
            materialButton3.Visible = false;

            int idUsuario = int.Parse(IdUsuarioDetalle.Text);
            prestamoController = new PrestamoController();
            dataGridView1.DataSource = prestamoController.PorUsuario(idUsuario);
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            materialCard4.Visible = false;
            materialCard3.Visible = true;
            materialButton1.Visible = true;
            materialButton2.Visible = true;
            materialButton4.Visible = false;
            materialButton3.Visible = true;
        }
    }
}
