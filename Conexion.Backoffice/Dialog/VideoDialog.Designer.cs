namespace Conexion.VideoWin
{
    partial class VideoDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoDialog));
            this.mvView = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.mvView)).BeginInit();
            this.SuspendLayout();
            // 
            // mvView
            // 
            this.mvView.Enabled = true;
            this.mvView.Location = new System.Drawing.Point(12, 12);
            this.mvView.Name = "mvView";
            this.mvView.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mvView.OcxState")));
            this.mvView.Size = new System.Drawing.Size(776, 426);
            this.mvView.TabIndex = 0;
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mvView);
            this.Name = "VideoForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mvView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer mvView;
    }
}

