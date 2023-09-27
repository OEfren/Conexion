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
    public partial class CanalDialog : Form
    {

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public CanalDialog()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Nombre = txtNombre.Text;
            Descripcion = txtDescripcion.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
