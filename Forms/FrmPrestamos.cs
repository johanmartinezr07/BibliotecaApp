#nullable disable
using System;
using System.Windows.Forms;
using BibliotecaApp.Business;
using BibliotecaApp.Models;

namespace BibliotecaApp.Forms
{
    public partial class FrmPrestamos : Form
    {
        private PrestamoNegocio prestamoNegocio = new PrestamoNegocio();
        private LibroNegocio libroNegocio = new LibroNegocio();
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        private int idPrestamoSeleccionado = 0;
        private int idLibroSeleccionado = 0;

        private ComboBox cboLibro = new ComboBox();
        private ComboBox cboUsuario = new ComboBox();
        private DateTimePicker dtpFechaPrestamo = new DateTimePicker();

        private DataGridView dgvPrestamos = new DataGridView();
        private Button btnPrestar = new Button();
        private Button btnDevolver = new Button();

        private Label lblLibro = new Label();
        private Label lblUsuario = new Label();
        private Label lblFecha = new Label();

        public FrmPrestamos()
        {
            ConstruirInterfaz();
            CargarCombos();
            CargarPrestamos();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Gestión de Préstamos y Devoluciones";
            this.Size = new System.Drawing.Size(850, 420);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblLibro.Text = "Libro:";
            lblLibro.Location = new System.Drawing.Point(20, 20);
            cboLibro.Location = new System.Drawing.Point(100, 20);
            cboLibro.Size = new System.Drawing.Size(200, 21);
            cboLibro.DropDownStyle = ComboBoxStyle.DropDownList;

            lblUsuario.Text = "Usuario:";
            lblUsuario.Location = new System.Drawing.Point(20, 60);
            cboUsuario.Location = new System.Drawing.Point(100, 60);
            cboUsuario.Size = new System.Drawing.Size(200, 21);
            cboUsuario.DropDownStyle = ComboBoxStyle.DropDownList;

            lblFecha.Text = "F. Préstamo:";
            lblFecha.Location = new System.Drawing.Point(20, 100);
            dtpFechaPrestamo.Location = new System.Drawing.Point(100, 100);
            dtpFechaPrestamo.Size = new System.Drawing.Size(200, 20);

            btnPrestar.Text = "Registrar Préstamo";
            btnPrestar.Location = new System.Drawing.Point(20, 150);
            btnPrestar.Size = new System.Drawing.Size(135, 30);
            btnPrestar.Click += BtnPrestar_Click;

            btnDevolver.Text = "Registrar Devolución";
            btnDevolver.Location = new System.Drawing.Point(165, 150);
            btnDevolver.Size = new System.Drawing.Size(135, 30);
            btnDevolver.Click += BtnDevolver_Click;

            dgvPrestamos.Location = new System.Drawing.Point(320, 20);
            dgvPrestamos.Size = new System.Drawing.Size(490, 330);
            dgvPrestamos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrestamos.MultiSelect = false;
            dgvPrestamos.ReadOnly = true;
            dgvPrestamos.CellClick += DgvPrestamos_CellClick;

            this.Controls.Add(lblLibro);
            this.Controls.Add(cboLibro);
            this.Controls.Add(lblUsuario);
            this.Controls.Add(cboUsuario);
            this.Controls.Add(lblFecha);
            this.Controls.Add(dtpFechaPrestamo);
            this.Controls.Add(btnPrestar);
            this.Controls.Add(btnDevolver);
            this.Controls.Add(dgvPrestamos);
        }

        private void CargarCombos()
        {
            cboLibro.DataSource = libroNegocio.Listar();
            cboLibro.DisplayMember = "Titulo";
            cboLibro.ValueMember = "IdLibro";

            cboUsuario.DataSource = usuarioNegocio.Listar();
            cboUsuario.DisplayMember = "Nombre";
            cboUsuario.ValueMember = "IdUsuario";
        }

        private void CargarPrestamos()
        {
            dgvPrestamos.DataSource = prestamoNegocio.Listar();
            if (dgvPrestamos.Columns["IdLibro"] != null) dgvPrestamos.Columns["IdLibro"].Visible = false;
            if (dgvPrestamos.Columns["IdUsuario"] != null) dgvPrestamos.Columns["IdUsuario"].Visible = false;
        }

        private void BtnPrestar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboLibro.SelectedValue == null || cboUsuario.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un libro y un usuario válidos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Prestamo prestamo = new Prestamo()
                {
                    IdLibro = Convert.ToInt32(cboLibro.SelectedValue),
                    IdUsuario = Convert.ToInt32(cboUsuario.SelectedValue),
                    FechaPrestamo = dtpFechaPrestamo.Value
                };

                if (prestamoNegocio.RegistrarPrestamo(prestamo))
                {
                    MessageBox.Show("Préstamo registrado exitosamente y stock actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarPrestamos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDevolver_Click(object sender, EventArgs e)
        {
            try
            {
                if (idPrestamoSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un préstamo activo de la lista para registrar la devolución.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (prestamoNegocio.RegistrarDevolucion(idPrestamoSeleccionado, idLibroSeleccionado))
                {
                    MessageBox.Show("Devolución registrada correctamente y el libro regresó al stock.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    idPrestamoSeleccionado = 0;
                    idLibroSeleccionado = 0;
                    CargarPrestamos();
                }
                else
                {
                    MessageBox.Show("El préstamo seleccionado ya figuraba como devuelto.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPrestamos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvPrestamos.Rows[e.RowIndex];
                idPrestamoSeleccionado = Convert.ToInt32(fila.Cells["IdPrestamo"].Value);
                idLibroSeleccionado = Convert.ToInt32(fila.Cells["IdLibro"].Value);
            }
        }
    }
}