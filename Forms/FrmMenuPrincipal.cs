#nullable disable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BibliotecaApp.Forms
{
    public partial class FrmMenuPrincipal : Form
    {
        private Button btnAutores = new Button();
        private Button btnEditoriales = new Button();
        private Button btnUsuarios = new Button();
        private Button btnLibros = new Button();
        private Button btnPrestamos = new Button();
        private Label lblTitulo = new Label();

        public FrmMenuPrincipal()
        {
            ConstruirInterfaz();
        }

        private void ConstruirInterfaz()
        {
            this.Text = "Sistema de Gestión de Biblioteca";
            this.Size = new System.Drawing.Size(400, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            lblTitulo.Text = "Menú Principal - Biblioteca";
            lblTitulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitulo.Location = new Point(50, 20);
            lblTitulo.AutoSize = true;

            btnAutores.Text = "Gestión de Autores";
            btnAutores.Location = new Point(90, 70);
            btnAutores.Size = new Size(200, 35);
            btnAutores.Click += (s, e) => new FrmAutores().ShowDialog();

            btnEditoriales.Text = "Gestión de Editoriales";
            btnEditoriales.Location = new Point(90, 120);
            btnEditoriales.Size = new Size(200, 35);
            btnEditoriales.Click += (s, e) => new FrmEditoriales().ShowDialog();

            btnUsuarios.Text = "Gestión de Usuarios";
            btnUsuarios.Location = new Point(90, 170);
            btnUsuarios.Size = new Size(200, 35);
            btnUsuarios.Click += (s, e) => new FrmUsuarios().ShowDialog();

            btnLibros.Text = "Gestión de Libros";
            btnLibros.Location = new Point(90, 220);
            btnLibros.Size = new Size(200, 35);
            btnLibros.Click += (s, e) => new FrmLibros().ShowDialog();

            btnPrestamos.Text = "Gestión de Préstamos";
            btnPrestamos.Location = new Point(90, 270);
            btnPrestamos.Size = new Size(200, 35);
            btnPrestamos.Click += (s, e) => new FrmPrestamos().ShowDialog();

            this.Controls.Add(lblTitulo);
            this.Controls.Add(btnAutores);
            this.Controls.Add(btnEditoriales);
            this.Controls.Add(btnUsuarios);
            this.Controls.Add(btnLibros);
            this.Controls.Add(btnPrestamos);
        }
    }
}