using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backoffice.Dialog
{
    public partial class VideoDialog : Form
    {
        public VideoDialog()
        {
            InitializeComponent();
        }

        private void VideoDialog_Load(object sender, EventArgs e)
        {
            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;

            // Create the WPF UserControl.
            HostingWpfUserControlInWf.UserControl1 uc =
                new HostingWpfUserControlInWf.UserControl1();

            // Assign the WPF UserControl to the ElementHost control's
            // Child property.
            host.Child = uc;

            // Add the ElementHost control to the form's
            // collection of child controls.
            this.Controls.Add(host);
        }
    }
}
