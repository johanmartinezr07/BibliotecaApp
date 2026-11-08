#nullable disable
using System;
using System.Windows.Forms;
using BibliotecaApp.Business;
using BibliotecaApp.Models;

namespace BibliotecaApp.Forms
{
    public partial class FrmUsuarios : Form
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private int idUsuarioSeleccionado = 0;

        private TextBox txtNombre = new TextBox();
        private TextBox txtApellido = new TextBox();
        private TextBox txtDocumento = new TextBox();
        private TextBox txtTelefono = new TextBox();
        private TextBox txtCorreo = new TextBox();

        private DataGridView dgvUsuarios = new DataGridView();
        private Button btnGuardar = new Button();
        private Button btnEliminar = new Button();
        private Button btnNuevo = new Button();

        private Label lblNombre = new Label();
        private Label lblApellido = new Label();
        private Label lblDocumento = new Label();
        private Label lblTelefono = new Label();
        private Label lblCorreo = new Label();

        public FrmUsuarios()
        {
            ConstruirInterfaz();
            CargarUsuarios();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Gestión de Usuarios";
            this.Size = new System.Drawing.Size(750, 420);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblNombre.Text = "Nombre:";
            lblNombre.Location = new System.Drawing.Point(20, 20);
            txtNombre.Location = new System.Drawing.Point(100, 20);
            txtNombre.Size = new System.Drawing.Size(170, 20);

            lblApellido.Text = "Apellido:";
            lblApellido.Location = new System.Drawing.Point(20, 50);
            txtApellido.Location = new System.Drawing.Point(100, 50);
            txtApellido.Size = new System.Drawing.Size(170, 20);

            lblDocumento.Text = "Documento:";
            lblDocumento.Location = new System.Drawing.Point(20, 80);
            txtDocumento.Location = new System.Drawing.Point(100, 80);
            txtDocumento.Size = new System.Drawing.Size(170, 20);

            lblTelefono.Text = "Teléfono:";
            lblTelefono.Location = new System.Drawing.Point(20, 110);
            txtTelefono.Location = new System.Drawing.Point(100, 110);
            txtTelefono.Size = new System.Drawing.Size(170, 20);

            lblCorreo.Text = "Correo:";
            lblCorreo.Location = new System.Drawing.Point(20, 140);
            txtCorreo.Location = new System.Drawing.Point(100, 140);
            txtCorreo.Size = new System.Drawing.Size(170, 20);

            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new System.Drawing.Point(20, 180);
            btnGuardar.Click += BtnGuardar_Click;

            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new System.Drawing.Point(105, 180);
            btnEliminar.Click += BtnEliminar_Click;

            btnNuevo.Text = "Limpiar";
            btnNuevo.Location = new System.Drawing.Point(190, 180);
            btnNuevo.Click += (s, e) => LimpiarCampos();

            dgvUsuarios.Location = new System.Drawing.Point(290, 20);
            dgvUsuarios.Size = new System.Drawing.Size(430, 330);
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.CellClick += DgvUsuarios_CellClick;

            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblApellido);
            this.Controls.Add(txtApellido);
            this.Controls.Add(lblDocumento);
            this.Controls.Add(txtDocumento);
            this.Controls.Add(lblTelefono);
            this.Controls.Add(txtTelefono);
            this.Controls.Add(lblCorreo);
            this.Controls.Add(txtCorreo);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(btnNuevo);
            this.Controls.Add(dgvUsuarios);
        }

        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = usuarioNegocio.Listar();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario(idUsuarioSeleccionado, txtNombre.Text, txtApellido.Text, txtDocumento.Text, txtTelefono.Text, txtCorreo.Text);
                if (usuarioNegocio.Guardar(usuario))
                {
                    MessageBox.Show("Usuario guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la tabla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (usuarioNegocio.Eliminar(idUsuarioSeleccionado))
                {
                    MessageBox.Show("Usuario eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarUsuarios();
                }
            }
        }

        private void DgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];
                idUsuarioSeleccionado = Convert.ToInt32(fila.Cells["IdUsuario"].Value);
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txtApellido.Text = fila.Cells["Apellido"].Value?.ToString() ?? "";
                txtDocumento.Text = fila.Cells["Documento"].Value?.ToString() ?? "";
                txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString() ?? "";
                txtCorreo.Text = fila.Cells["Correo"].Value?.ToString() ?? "";
            }
        }

        private void LimpiarCampos()
        {
            idUsuarioSeleccionado = 0;
            txtNombre.Clear();
            txtApellido.Clear();
            txtDocumento.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
        }
    }
}