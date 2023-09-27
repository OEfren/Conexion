using Backoffice.Dialog;
using Conexion.Canal;
using System.Windows.Forms;

namespace Backoffice
{
    public partial class MainForm : Form
    {

        Servidor CanalServidor;
        List<Cliente> CanalClientes = new List<Cliente>();
        List<Broadcast> Broadcasts = new List<Broadcast>();

        string Device;

        public MainForm()
        {
            InitializeComponent();
            Device = Guid.NewGuid().ToString();
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
                            if (info.Broadcast != null)
                            {
                                var broadcast = Broadcasts.FirstOrDefault(c => c.ID == info.Broadcast.ID);
                                if (broadcast == null)
                                {
                                    broadcast = AgregarBroadcast(info.Broadcast.ID, info.Broadcast.Nombre, info.Broadcast.Descripcion, info.Broadcast.IdPropietario);
                                }
                                string mensaje = string.Format("{0}:{1} {2}: {3}", info.Servidor.Ip, info.Servidor.Ip, info.Servidor.Nombre, info.Mensaje);
                                broadcast.Mensajes.Add(mensaje);

                                // Siendo el due�o del broadcast vamos a propagar a los demas
                                if (broadcast.IdPropietario == Device)
                                {
                                    foreach (var cliente in lvContacto.Items)
                                    {
                                        if (cliente != null && cliente is Cliente)
                                        {
                                            Cliente cancalCliente = (Cliente)cliente;
                                            if (!(cancalCliente.Ip == info.Servidor.Ip && cancalCliente.Port == info.Servidor.Port))
                                                EnviarMensaje(cancalCliente, mensaje, broadcast);
                                        }
                                    }
                                } 
                            }
                            else
                            {
                                var canal = CanalClientes.FirstOrDefault(c => c.Ip == info.Servidor.Ip && c.Port == info.Servidor.Port);
                                if (canal == null)
                                {
                                    canal = AgregarCliente(info.Servidor.Nombre, info.Servidor.Ip, info.Servidor.Port);
                                }
                                string mensaje = string.Format("Recibido: {0}", info.Mensaje);
                                canal.Mensajes.Add(mensaje);
                            }
                            RefrescarHistorial();
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                };
                Task.Run(() => CanalServidor.Iniciar());
                Text = string.Format("{0}:{1}", CanalServidor.Ip, CanalServidor.Port);
            }
            else
            {
                MessageBox.Show("Configuraci�n incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string mensaje = txtMensaje.Text;
                if (item is Cliente)
                {
                    Cliente cliente = (Cliente)item;
                    EnviarMensaje(cliente, mensaje);
                }
                else if (item is Broadcast)
                {
                    Broadcast broadcast = (Broadcast)item;
                    broadcast.Mensajes.Add(txtMensaje.Text);

                    foreach (var cliente in lvContacto.Items)
                    {
                        if (cliente != null && cliente is Cliente)
                        {
                            EnviarMensaje((Cliente)cliente, mensaje, (Broadcast)item);
                        }
                    }
                }
                string mensajeText = string.Format("Enviado: {0}", mensaje);
                txtHistorial.AppendText(mensajeText);
                txtHistorial.AppendText(Environment.NewLine);
                txtMensaje.Text = "";
            }
            else
            {
                MessageBox.Show("Seleccione un contacto de la lista", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EnviarMensaje(Cliente cliente, string mensaje, Broadcast broadcast = null)
        {
            MensajeInfo info = new MensajeInfo();
            info.Servidor = CanalServidor;
            info.Mensaje = mensaje;
            info.Broadcast = broadcast;

            if (broadcast == null)
            {
                cliente.Mensajes.Add(string.Format("Enviado: {0}", info.Mensaje));
            }
            cliente.EnviarMensaje(info);
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

        private Broadcast AgregarBroadcast(string id, string nombre, string descripcion, string idPropietario)
        {
            Broadcast broadcast = new Broadcast();
            broadcast.ID = id;
            broadcast.Nombre = nombre;
            broadcast.Descripcion = descripcion;
            broadcast.IdPropietario = idPropietario;
            Broadcasts.Add(broadcast);
            lvContacto.Items.Add(broadcast);
            return broadcast;
        }

        private void lvContacto_SelectedValueChanged(object sender, EventArgs e)
        {
            RefrescarHistorial();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CanalClientes.ForEach(cliente => cliente.Detener());
            CanalServidor?.Detener();
        }

        private void RefrescarHistorial()
        {
            var item = lvContacto.SelectedItem;
            if (item != null)
            {
                if (item is Cliente)
                {
                    Cliente cliente = (Cliente)item;
                    Text = string.Format("{0}:{1} Chateando con {0} ", CanalServidor.Ip, CanalServidor.Port, cliente.Nombre);
                    txtHistorial.Text = string.Empty;
                    cliente.Mensajes.ForEach(mensaje =>
                    {
                        txtHistorial.AppendText(mensaje);
                        txtHistorial.AppendText(Environment.NewLine);
                    });
                }
                else
                {
                    Broadcast broadcast = (Broadcast)item;
                    Text = string.Format("Canal {0} ", broadcast.Nombre);
                    txtHistorial.Text = string.Empty;
                    broadcast.Mensajes.ForEach(mensaje =>
                    {
                        txtHistorial.AppendText(mensaje);
                        txtHistorial.AppendText(Environment.NewLine);
                    });
                }
            }
            else
            {
                Text = string.Format("{0}:{1}", CanalServidor.Ip, CanalServidor.Port);
            }

        }

        private void btnCanal_Click(object sender, EventArgs e)
        {
            CanalDialog dlgCanal = new CanalDialog();
            DialogResult result = dlgCanal.ShowDialog();
            if (result == DialogResult.OK)
            {
                AgregarBroadcast(Guid.NewGuid().ToString(), dlgCanal.Nombre, dlgCanal.Descripcion, Device);
            }
        }
    }
}