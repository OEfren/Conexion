using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conexion.VideoWin
{
    public partial class VideoDialog : Form
    {
        public VideoDialog()
        {
            InitializeComponent();
        }

        public void Show(string pathVideo)
        {
            base.Show();
            mvView.URL = pathVideo;
        }

    }
}
