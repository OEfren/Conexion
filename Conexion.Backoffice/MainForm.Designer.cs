﻿using System.Drawing;
using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            this.txtHistorial = new System.Windows.Forms.TextBox();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.lvContacto = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItemTicTacToe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemEnviarArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnCanal = new System.Windows.Forms.Button();
            this.dlgFile = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtHistorial
            // 
            this.txtHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHistorial.Location = new System.Drawing.Point(116, 23);
            this.txtHistorial.Multiline = true;
            this.txtHistorial.Name = "txtHistorial";
            this.txtHistorial.ReadOnly = true;
            this.txtHistorial.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistorial.Size = new System.Drawing.Size(388, 344);
            this.txtHistorial.TabIndex = 0;
            // 
            // txtMensaje
            // 
            this.txtMensaje.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMensaje.Location = new System.Drawing.Point(116, 396);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(319, 20);
            this.txtMensaje.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 380);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mensaje";
            // 
            // btnEnviar
            // 
            this.btnEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnviar.Location = new System.Drawing.Point(439, 396);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(64, 20);
            this.btnEnviar.TabIndex = 3;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // lvContacto
            // 
            this.lvContacto.ContextMenuStrip = this.contextMenuStrip1;
            this.lvContacto.FormattingEnabled = true;
            this.lvContacto.Location = new System.Drawing.Point(8, 23);
            this.lvContacto.Name = "lvContacto";
            this.lvContacto.Size = new System.Drawing.Size(103, 342);
            this.lvContacto.TabIndex = 4;
            this.lvContacto.SelectedValueChanged += new System.EventHandler(this.lvContacto_SelectedValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemTicTacToe,
            this.mnuItemEnviarArchivo});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(162, 48);
            // 
            // mnuItemTicTacToe
            // 
            this.mnuItemTicTacToe.Name = "mnuItemTicTacToe";
            this.mnuItemTicTacToe.Size = new System.Drawing.Size(180, 22);
            this.mnuItemTicTacToe.Text = "Jugar Tic Tac Toe";
            this.mnuItemTicTacToe.Click += new System.EventHandler(this.mnuItemTicTacToe_Click);
            // 
            // mnuItemEnviarArchivo
            // 
            this.mnuItemEnviarArchivo.Name = "mnuItemEnviarArchivo";
            this.mnuItemEnviarArchivo.Size = new System.Drawing.Size(180, 22);
            this.mnuItemEnviarArchivo.Text = "Enviar archivo...";
            this.mnuItemEnviarArchivo.Click += new System.EventHandler(this.mnuItemEnviarArchivo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Contactos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Historial";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(10, 371);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(100, 20);
            this.btnAgregar.TabIndex = 7;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnCanal
            // 
            this.btnCanal.Location = new System.Drawing.Point(10, 396);
            this.btnCanal.Name = "btnCanal";
            this.btnCanal.Size = new System.Drawing.Size(100, 20);
            this.btnCanal.TabIndex = 8;
            this.btnCanal.Text = "Crear canal";
            this.btnCanal.UseVisualStyleBackColor = true;
            this.btnCanal.Click += new System.EventHandler(this.btnCanal_Click);
            // 
            // dlgFile
            // 
            this.dlgFile.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 429);
            this.Controls.Add(this.btnCanal);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvContacto);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.txtHistorial);
            this.Name = "MainForm";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem mnuItemTicTacToe;
        private ToolStripMenuItem mnuItemEnviarArchivo;
        private OpenFileDialog dlgFile;
    }
}