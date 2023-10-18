using System.Drawing;
using System.Windows.Forms;

namespace Backoffice.Dialog
{
    partial class ConexionDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnAceptar = new Button();
            txtNombre = new TextBox();
            txtIp = new TextBox();
            label2 = new Label();
            txtPuerto = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 0;
            label1.Text = "Nombre";
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(165, 126);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(75, 23);
            btnAceptar.TabIndex = 1;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(12, 36);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(359, 23);
            txtNombre.TabIndex = 2;
            txtNombre.Text = "My-PC";
            // 
            // txtIp
            // 
            txtIp.Location = new Point(12, 80);
            txtIp.Name = "txtIp";
            txtIp.Size = new Size(257, 23);
            txtIp.TabIndex = 4;
            txtIp.Text = "127.0.0.1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 62);
            label2.Name = "label2";
            label2.Size = new Size(17, 15);
            label2.TabIndex = 3;
            label2.Text = "IP";
            // 
            // txtPuerto
            // 
            txtPuerto.Location = new Point(275, 80);
            txtPuerto.Name = "txtPuerto";
            txtPuerto.Size = new Size(96, 23);
            txtPuerto.TabIndex = 6;
            txtPuerto.Text = "10000";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(275, 62);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 5;
            label3.Text = "Puerto";
            // 
            // ConexionDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 164);
            Controls.Add(txtPuerto);
            Controls.Add(label3);
            Controls.Add(txtIp);
            Controls.Add(label2);
            Controls.Add(txtNombre);
            Controls.Add(btnAceptar);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "ConexionDialog";
            Text = "Datos de conexión";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnAceptar;
        private TextBox txtNombre;
        private TextBox txtIp;
        private Label label2;
        private TextBox txtPuerto;
        private Label label3;
    }
}