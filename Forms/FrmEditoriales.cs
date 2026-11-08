#nullable disable
using System;
using System.Windows.Forms;
using BibliotecaApp.Business;
using BibliotecaApp.Models;

namespace BibliotecaApp.Forms
{
    public partial class FrmEditoriales : Form
    {
        private EditorialNegocio editorialNegocio = new EditorialNegocio();
        private int idEditorialSeleccionada = 0;

        private TextBox txtNombre = new TextBox();
        private TextBox txtPais = new TextBox();
        private DataGridView dgvEditoriales = new DataGridView();
        private Button btnGuardar = new Button();
        private Button btnEliminar = new Button();
        private Button btnNuevo = new Button();
        private Label lblNombre = new Label();
        private Label lblPais = new Label();

        public FrmEditoriales()
        {
            ConstruirInterfaz();
            CargarEditoriales();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Gestión de Editoriales";
            this.Size = new System.Drawing.Size(650, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblNombre.Text = "Nombre:";
            lblNombre.Location = new System.Drawing.Point(20, 20);
            txtNombre.Location = new System.Drawing.Point(90, 20);
            txtNombre.Size = new System.Drawing.Size(180, 20);

            lblPais.Text = "País:";
            lblPais.Location = new System.Drawing.Point(20, 60);
            txtPais.Location = new System.Drawing.Point(90, 60);
            txtPais.Size = new System.Drawing.Size(180, 20);

            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new System.Drawing.Point(20, 100);
            btnGuardar.Click += BtnGuardar_Click;

            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new System.Drawing.Point(105, 100);
            btnEliminar.Click += BtnEliminar_Click;

            btnNuevo.Text = "Limpiar";
            btnNuevo.Location = new System.Drawing.Point(190, 100);
            btnNuevo.Click += (s, e) => LimpiarCampos();

            dgvEditoriales.Location = new System.Drawing.Point(290, 20);
            dgvEditoriales.Size = new System.Drawing.Size(330, 310);
            dgvEditoriales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEditoriales.MultiSelect = false;
            dgvEditoriales.ReadOnly = true;
            dgvEditoriales.CellClick += DgvEditoriales_CellClick;

            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblPais);
            this.Controls.Add(txtPais);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(btnNuevo);
            this.Controls.Add(dgvEditoriales);
        }

        private void CargarEditoriales()
        {
            dgvEditoriales.DataSource = editorialNegocio.Listar();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Editorial editorial = new Editorial(idEditorialSeleccionada, txtNombre.Text, txtPais.Text);
                if (editorialNegocio.Guardar(editorial))
                {
                    MessageBox.Show("Editorial guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarEditoriales();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idEditorialSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una editorial de la tabla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar esta editorial?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (editorialNegocio.Eliminar(idEditorialSeleccionada))
                {
                    MessageBox.Show("Editorial eliminada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarEditoriales();
                }
            }
        }

        private void DgvEditoriales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvEditoriales.Rows[e.RowIndex];
                idEditorialSeleccionada = Convert.ToInt32(fila.Cells["IdEditorial"].Value);
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txtPais.Text = fila.Cells["Pais"].Value?.ToString() ?? "";
            }
        }

        private void LimpiarCampos()
        {
            idEditorialSeleccionada = 0;
            txtNombre.Clear();
            txtPais.Clear();
        }
    }
}