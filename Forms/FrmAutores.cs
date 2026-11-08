#nullable disable
using System;
using System.Windows.Forms;
using BibliotecaApp.Business;
using BibliotecaApp.Models;

namespace BibliotecaApp.Forms
{
    public partial class FrmAutores : Form
    {
        private AutorNegocio autorNegocio = new AutorNegocio();
        private int idAutorSeleccionado = 0;

        private TextBox txtNombre = new TextBox();
        private TextBox txtApellido = new TextBox();
        private DataGridView dgvAutores = new DataGridView();
        private Button btnGuardar = new Button();
        private Button btnEliminar = new Button();
        private Button btnNuevo = new Button();
        private Label lblNombre = new Label();
        private Label lblApellido = new Label();

        public FrmAutores()
        {
            // InitializeComponent(); // Removido porque creamos la interfaz por código
            ConstruirInterfaz();
            CargarAutores();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Gestión de Autores";
            this.Size = new System.Drawing.Size(650, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblNombre.Text = "Nombre:";
            lblNombre.Location = new System.Drawing.Point(20, 20);
            txtNombre.Location = new System.Drawing.Point(90, 20);
            txtNombre.Size = new System.Drawing.Size(180, 20);

            lblApellido.Text = "Apellido:";
            lblApellido.Location = new System.Drawing.Point(20, 60);
            txtApellido.Location = new System.Drawing.Point(90, 60);
            txtApellido.Size = new System.Drawing.Size(180, 20);

            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new System.Drawing.Point(20, 100);
            btnGuardar.Click += BtnGuardar_Click;

            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new System.Drawing.Point(105, 100);
            btnEliminar.Click += BtnEliminar_Click;

            btnNuevo.Text = "Limpiar";
            btnNuevo.Location = new System.Drawing.Point(190, 100);
            btnNuevo.Click += (s, e) => LimpiarCampos();

            dgvAutores.Location = new System.Drawing.Point(290, 20);
            dgvAutores.Size = new System.Drawing.Size(330, 310);
            dgvAutores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAutores.MultiSelect = false;
            dgvAutores.ReadOnly = true;
            dgvAutores.CellClick += DgvAutores_CellClick;

            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblApellido);
            this.Controls.Add(txtApellido);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(btnNuevo);
            this.Controls.Add(dgvAutores);
        }

        private void CargarAutores()
        {
            dgvAutores.DataSource = autorNegocio.Listar();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Autor autor = new Autor(idAutorSeleccionado, txtNombre.Text, txtApellido.Text);
                if (autorNegocio.Guardar(autor))
                {
                    MessageBox.Show("Autor guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarAutores();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idAutorSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un autor de la tabla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este autor?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (autorNegocio.Eliminar(idAutorSeleccionado))
                {
                    MessageBox.Show("Autor eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarAutores();
                }
            }
        }

        private void DgvAutores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvAutores.Rows[e.RowIndex];
                idAutorSeleccionado = Convert.ToInt32(fila.Cells["IdAutor"].Value);
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txtApellido.Text = fila.Cells["Apellido"].Value?.ToString() ?? "";
            }
        }

        private void LimpiarCampos()
        {
            idAutorSeleccionado = 0;
            txtNombre.Clear();
            txtApellido.Clear();
        }
    }
}