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
using Windows.Storage.Streams;

namespace Conexion.VideoWin
{
    public partial class VideoForm : Form
    {
        public VideoForm()
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
