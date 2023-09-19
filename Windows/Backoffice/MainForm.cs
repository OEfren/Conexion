using Backoffice.Dialog;
using Conexion.Canal;

namespace Backoffice
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConexionDialog conexionDialog = new ConexionDialog();
            DialogResult result = conexionDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Servidor servidor = new Servidor();

                servidor.Nombre = conexionDialog.Nombre;
                servidor.Ip = conexionDialog.Ip;
                servidor.Port = conexionDialog.Puerto;
                servidor.ListenerNuevoMensaje = (mensaje) =>
                {
                    try
                    {
                        Invoke(() =>
                        {
                            txtHistorial.Text = mensaje;
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                };
                Task.Run(() => servidor.Iniciar());
            }
            else
            {
                MessageBox.Show("Error", "Configuración incompleta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ConexionDialog conexionDialog = new ConexionDialog();
            DialogResult result = conexionDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = conexionDialog.Nombre;
                cliente.Ip = conexionDialog.Ip;
                cliente.Port = conexionDialog.Puerto;
                Task.Run(() => cliente.Iniciar());
                lvContacto.Items.Add(cliente);
            }

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            var item = lvContacto.SelectedItem;
            if (item != null)
            {
                Cliente cliente = (Cliente)item;
                if (cliente != null)
                {
                    cliente.EnviarMensaje(txtMensaje.Text);
                    txtMensaje.Text = "";
                }
            }
        }

    }
}