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
    public partial class ConexionDialog : Form
    {

        public string Nombre { get; set; }
        public string Ip { get; set; }
        public int Puerto { get; set; }

        public ConexionDialog()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Nombre = txtNombre.Text;
                Ip = txtIp.Text;
                Puerto = int.Parse(txtPuerto.Text);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
