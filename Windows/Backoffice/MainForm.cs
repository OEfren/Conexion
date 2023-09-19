using Backoffice.Dialog;
using Conexion.Canal;
using System.Windows.Forms;

namespace Backoffice
{
    public partial class MainForm : Form
    {

        Servidor CanalServidor;
        List<Cliente> CanalClientes = new List<Cliente>();

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
                CanalServidor = new Servidor();
                CanalServidor.Nombre = conexionDialog.Nombre;
                CanalServidor.Ip = conexionDialog.Ip;
                CanalServidor.Port = conexionDialog.Puerto;
                CanalServidor.ListenerNuevoMensaje = (info) =>
                {
                    try
                    {
                        Invoke(() =>
                        {
                            var canal = CanalClientes.Where(c => c.Ip == info.Servidor.Ip && c.Port == info.Servidor.Port).FirstOrDefault();
                            if (canal == null)
                            {
                                canal = AgregarCliente(info.Servidor.Nombre, info.Servidor.Ip, info.Servidor.Port);
                            }
                            string mensaje = string.Format("Recibido: {0}", info.Mensaje);
                            canal.Mensajes.Add(mensaje);

                            lvContacto.ClearSelected();
                            lvContacto.SelectedItem = canal;
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                };
                Task.Run(() => CanalServidor.Iniciar());
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
                AgregarCliente(conexionDialog.Nombre, conexionDialog.Ip, conexionDialog.Puerto);
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
                    MensajeInfo info = new MensajeInfo();
                    info.Servidor = CanalServidor;
                    info.Mensaje = txtMensaje.Text;

                    string mensaje = string.Format("Enviado: {0}", info.Mensaje);
                    txtHistorial.AppendText(mensaje);
                    txtHistorial.AppendText(Environment.NewLine);

                    cliente.Mensajes.Add(string.Format("Enviado: {0}", info.Mensaje));
                    cliente.EnviarMensaje(info);
                    txtMensaje.Text = "";
                }
                else
                {
                    MessageBox.Show("Seleccione un cliente de la lista", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private Cliente AgregarCliente(string nombre, string ip, int puerto)
        {
            Cliente cliente = new Cliente();
            cliente.Nombre = nombre;
            cliente.Ip = ip;
            cliente.Port = puerto;
            Task.Run(() => cliente.Iniciar());
            CanalClientes.Add(cliente);
            lvContacto.Items.Add(cliente);
            return cliente;
        }

        private void lvContacto_SelectedValueChanged(object sender, EventArgs e)
        {
            var item = lvContacto.SelectedItem;
            if (item != null)
            {
                Cliente cliente = (Cliente)item;
                Text = string.Format("Chatenado con {0} ", cliente.Nombre);
                txtHistorial.Text = string.Empty;
                cliente.Mensajes.ForEach(mensaje =>
                {
                    txtHistorial.AppendText(mensaje);
                    txtHistorial.AppendText(Environment.NewLine);
                });
            }
            else
            {
                Text = "Sin canal abierto";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CanalClientes.ForEach(cliente => cliente.Detener());
            CanalServidor.Detener();
        }
    }
}