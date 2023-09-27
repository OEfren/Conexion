namespace Backoffice
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtHistorial = new TextBox();
            txtMensaje = new TextBox();
            label1 = new Label();
            btnEnviar = new Button();
            lvContacto = new ListBox();
            label2 = new Label();
            label3 = new Label();
            btnAgregar = new Button();
            btnCanal = new Button();
            SuspendLayout();
            // 
            // txtHistorial
            // 
            txtHistorial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtHistorial.Location = new Point(154, 36);
            txtHistorial.Margin = new Padding(3, 4, 3, 4);
            txtHistorial.Multiline = true;
            txtHistorial.Name = "txtHistorial";
            txtHistorial.ReadOnly = true;
            txtHistorial.ScrollBars = ScrollBars.Vertical;
            txtHistorial.Size = new Size(516, 527);
            txtHistorial.TabIndex = 0;
            // 
            // txtMensaje
            // 
            txtMensaje.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMensaje.Location = new Point(154, 609);
            txtMensaje.Margin = new Padding(3, 4, 3, 4);
            txtMensaje.Name = "txtMensaje";
            txtMensaje.Size = new Size(423, 27);
            txtMensaje.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(154, 585);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 2;
            label1.Text = "Mensaje";
            // 
            // btnEnviar
            // 
            btnEnviar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnviar.Location = new Point(585, 609);
            btnEnviar.Margin = new Padding(3, 4, 3, 4);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(86, 31);
            btnEnviar.TabIndex = 3;
            btnEnviar.Text = "Enviar";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // lvContacto
            // 
            lvContacto.FormattingEnabled = true;
            lvContacto.ItemHeight = 20;
            lvContacto.Location = new Point(10, 36);
            lvContacto.Margin = new Padding(3, 4, 3, 4);
            lvContacto.Name = "lvContacto";
            lvContacto.Size = new Size(137, 524);
            lvContacto.TabIndex = 4;
            lvContacto.SelectedValueChanged += lvContacto_SelectedValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 12);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 5;
            label2.Text = "Contactos";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(154, 12);
            label3.Name = "label3";
            label3.Size = new Size(65, 20);
            label3.TabIndex = 6;
            label3.Text = "Historial";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(14, 571);
            btnAgregar.Margin = new Padding(3, 4, 3, 4);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(134, 31);
            btnAgregar.TabIndex = 7;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnCanal
            // 
            btnCanal.Location = new Point(14, 609);
            btnCanal.Margin = new Padding(3, 4, 3, 4);
            btnCanal.Name = "btnCanal";
            btnCanal.Size = new Size(134, 31);
            btnCanal.TabIndex = 8;
            btnCanal.Text = "Crear canal";
            btnCanal.UseVisualStyleBackColor = true;
            btnCanal.Click += btnCanal_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(685, 660);
            Controls.Add(btnCanal);
            Controls.Add(btnAgregar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lvContacto);
            Controls.Add(btnEnviar);
            Controls.Add(label1);
            Controls.Add(txtMensaje);
            Controls.Add(txtHistorial);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Chat";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtHistorial;
        private TextBox txtMensaje;
        private Label label1;
        private Button btnEnviar;
        private ListBox lvContacto;
        private Label label2;
        private Label label3;
        private Button btnAgregar;
        private Button btnCanal;
    }
}