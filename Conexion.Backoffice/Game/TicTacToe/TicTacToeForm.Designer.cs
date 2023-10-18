using System.Drawing;
using System.Windows.Forms;

namespace Backoffice.Game.TicTacToe
{
    partial class TicTacToeForm
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            label1 = new Label();
            label2 = new Label();
            lblJugador1 = new Label();
            lblJugador2 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(67, 49);
            button1.Name = "button1";
            button1.Size = new Size(72, 43);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button_Click;
            // 
            // button2
            // 
            button2.Location = new Point(177, 49);
            button2.Name = "button2";
            button2.Size = new Size(72, 43);
            button2.TabIndex = 1;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button_Click;
            // 
            // button3
            // 
            button3.Location = new Point(280, 49);
            button3.Name = "button3";
            button3.Size = new Size(72, 43);
            button3.TabIndex = 2;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button_Click;
            // 
            // button4
            // 
            button4.Location = new Point(67, 126);
            button4.Name = "button4";
            button4.Size = new Size(72, 43);
            button4.TabIndex = 3;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button_Click;
            // 
            // button5
            // 
            button5.Location = new Point(177, 126);
            button5.Name = "button5";
            button5.Size = new Size(72, 43);
            button5.TabIndex = 4;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button_Click;
            // 
            // button6
            // 
            button6.Location = new Point(280, 126);
            button6.Name = "button6";
            button6.Size = new Size(72, 43);
            button6.TabIndex = 5;
            button6.UseVisualStyleBackColor = true;
            button6.Click += button_Click;
            // 
            // button7
            // 
            button7.Location = new Point(67, 201);
            button7.Name = "button7";
            button7.Size = new Size(72, 43);
            button7.TabIndex = 6;
            button7.UseVisualStyleBackColor = true;
            button7.Click += button_Click;
            // 
            // button8
            // 
            button8.Location = new Point(177, 201);
            button8.Name = "button8";
            button8.Size = new Size(72, 43);
            button8.TabIndex = 7;
            button8.UseVisualStyleBackColor = true;
            button8.Click += button_Click;
            // 
            // button9
            // 
            button9.Location = new Point(280, 201);
            button9.Name = "button9";
            button9.Size = new Size(72, 43);
            button9.TabIndex = 8;
            button9.UseVisualStyleBackColor = true;
            button9.Click += button_Click;
            // 
            // label1
            // 
            label1.Location = new Point(53, 295);
            label1.Name = "label1";
            label1.Size = new Size(121, 19);
            label1.TabIndex = 9;
            label1.Text = "Jugador 1";
            label1.Visible = false;
            // 
            // label2
            // 
            label2.Location = new Point(255, 295);
            label2.Name = "label2";
            label2.Size = new Size(121, 19);
            label2.TabIndex = 10;
            label2.Text = "Jugador 2";
            label2.Visible = false;
            // 
            // lblJugador1
            // 
            lblJugador1.BorderStyle = BorderStyle.Fixed3D;
            lblJugador1.Location = new Point(53, 325);
            lblJugador1.Name = "lblJugador1";
            lblJugador1.Size = new Size(121, 19);
            lblJugador1.TabIndex = 11;
            lblJugador1.Visible = false;
            // 
            // lblJugador2
            // 
            lblJugador2.BorderStyle = BorderStyle.Fixed3D;
            lblJugador2.Location = new Point(255, 325);
            lblJugador2.Name = "lblJugador2";
            lblJugador2.Size = new Size(121, 19);
            lblJugador2.TabIndex = 12;
            lblJugador2.Visible = false;
            // 
            // TicTacToeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(413, 364);
            Controls.Add(lblJugador2);
            Controls.Add(lblJugador1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "TicTacToeForm";
            Text = "TicTacToeForm";
            Load += TicTacToeForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Label label1;
        private Label label2;
        private Label lblJugador1;
        private Label lblJugador2;
    }
}