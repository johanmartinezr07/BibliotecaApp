#nullable disable
using System;
using System.Windows.Forms;
using BibliotecaApp.Business;
using BibliotecaApp.Models;

namespace BibliotecaApp.Forms
{
    public partial class FrmLibros : Form
    {
        private LibroNegocio libroNegocio = new LibroNegocio();
        private AutorNegocio autorNegocio = new AutorNegocio();
        private EditorialNegocio editorialNegocio = new EditorialNegocio();

        private int idLibroSeleccionado = 0;

        private TextBox txtTitulo = new TextBox();
        private ComboBox cboAutor = new ComboBox();
        private ComboBox cboEditorial = new ComboBox();
        private NumericUpDown txtAnio = new NumericUpDown();
        private NumericUpDown txtStock = new NumericUpDown();

        private DataGridView dgvLibros = new DataGridView();
        private Button btnGuardar = new Button();
        private Button btnEliminar = new Button();
        private Button btnNuevo = new Button();

        private Label lblTitulo = new Label();
        private Label lblAutor = new Label();
        private Label lblEditorial = new Label();
        private Label lblAnio = new Label();
        private Label lblStock = new Label();

        public FrmLibros()
        {
            ConstruirInterfaz();
            CargarCombos();
            CargarLibros();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Gestión de Libros";
            this.Size = new System.Drawing.Size(800, 420);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitulo.Text = "Título:";
            lblTitulo.Location = new System.Drawing.Point(20, 20);
            txtTitulo.Location = new System.Drawing.Point(100, 20);
            txtTitulo.Size = new System.Drawing.Size(180, 20);

            lblAutor.Text = "Autor:";
            lblAutor.Location = new System.Drawing.Point(20, 50);
            cboAutor.Location = new System.Drawing.Point(100, 50);
            cboAutor.Size = new System.Drawing.Size(180, 21);
            cboAutor.DropDownStyle = ComboBoxStyle.DropDownList;

            lblEditorial.Text = "Editorial:";
            lblEditorial.Location = new System.Drawing.Point(20, 80);
            cboEditorial.Location = new System.Drawing.Point(100, 80);
            cboEditorial.Size = new System.Drawing.Size(180, 21);
            cboEditorial.DropDownStyle = ComboBoxStyle.DropDownList;

            lblAnio.Text = "Año Pub.:";
            lblAnio.Location = new System.Drawing.Point(20, 110);
            txtAnio.Location = new System.Drawing.Point(100, 110);
            txtAnio.Size = new System.Drawing.Size(180, 20);
            txtAnio.Minimum = 1500;
            txtAnio.Maximum = 2100;
            txtAnio.Value = 2024;

            lblStock.Text = "Stock:";
            lblStock.Location = new System.Drawing.Point(20, 140);
            txtStock.Location = new System.Drawing.Point(100, 140);
            txtStock.Size = new System.Drawing.Size(180, 20);
            txtStock.Minimum = 0;
            txtStock.Maximum = 10000;
            txtStock.Value = 1;

            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new System.Drawing.Point(20, 180);
            btnGuardar.Click += BtnGuardar_Click;

            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new System.Drawing.Point(110, 180);
            btnEliminar.Click += BtnEliminar_Click;

            btnNuevo.Text = "Limpiar";
            btnNuevo.Location = new System.Drawing.Point(200, 180);
            btnNuevo.Click += (s, e) => LimpiarCampos();

            dgvLibros.Location = new System.Drawing.Point(300, 20);
            dgvLibros.Size = new System.Drawing.Size(460, 330);
            dgvLibros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLibros.MultiSelect = false;
            dgvLibros.ReadOnly = true;
            dgvLibros.CellClick += DgvLibros_CellClick;

            this.Controls.Add(lblTitulo);
            this.Controls.Add(txtTitulo);
            this.Controls.Add(lblAutor);
            this.Controls.Add(cboAutor);
            this.Controls.Add(lblEditorial);
            this.Controls.Add(cboEditorial);
            this.Controls.Add(lblAnio);
            this.Controls.Add(txtAnio);
            this.Controls.Add(lblStock);
            this.Controls.Add(txtStock);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(btnNuevo);
            this.Controls.Add(dgvLibros);
        }

        private void CargarCombos()
        {
            var autores = autorNegocio.Listar();
            cboAutor.DataSource = autores;
            cboAutor.DisplayMember = "Nombre";
            cboAutor.ValueMember = "IdAutor";

            var editoriales = editorialNegocio.Listar();
            cboEditorial.DataSource = editoriales;
            cboEditorial.DisplayMember = "Nombre";
            cboEditorial.ValueMember = "IdEditorial";
        }

        private void CargarLibros()
        {
            dgvLibros.DataSource = libroNegocio.Listar();
            if (dgvLibros.Columns["IdAutor"] != null) dgvLibros.Columns["IdAutor"].Visible = false;
            if (dgvLibros.Columns["IdEditorial"] != null) dgvLibros.Columns["IdEditorial"].Visible = false;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idAutor = Convert.ToInt32(cboAutor.SelectedValue);
                int idEditorial = Convert.ToInt32(cboEditorial.SelectedValue);

                Libro libro = new Libro(idLibroSeleccionado, txtTitulo.Text, idAutor, idEditorial, Convert.ToInt32(txtAnio.Value), Convert.ToInt32(txtStock.Value));
                if (libroNegocio.Guardar(libro))
                {
                    MessageBox.Show("Libro guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarLibros();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idLibroSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un libro de la tabla.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este libro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (libroNegocio.Eliminar(idLibroSeleccionado))
                {
                    MessageBox.Show("Libro eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarLibros();
                }
            }
        }

        private void DgvLibros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvLibros.Rows[e.RowIndex];
                idLibroSeleccionado = Convert.ToInt32(fila.Cells["IdLibro"].Value);
                txtTitulo.Text = fila.Cells["Titulo"].Value?.ToString() ?? "";
                cboAutor.SelectedValue = Convert.ToInt32(fila.Cells["IdAutor"].Value);
                cboEditorial.SelectedValue = Convert.ToInt32(fila.Cells["IdEditorial"].Value);
                txtAnio.Value = Convert.ToDecimal(fila.Cells["AnioPublicacion"].Value);
                txtStock.Value = Convert.ToDecimal(fila.Cells["Stock"].Value);
            }
        }

        private void LimpiarCampos()
        {
            idLibroSeleccionado = 0;
            txtTitulo.Clear();
            txtAnio.Value = 2024;
            txtStock.Value = 1;
            if (cboAutor.Items.Count > 0) cboAutor.SelectedIndex = 0;
            if (cboEditorial.Items.Count > 0) cboEditorial.SelectedIndex = 0;
        }
    }
}