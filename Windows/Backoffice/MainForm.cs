using Backoffice.Dialog;
using Backoffice.Game.TicTacToe;
using Conexion.Canal;
using Newtonsoft.Json;
using System.Globalization;
using System.Windows.Forms;

namespace Backoffice
{
    public partial class MainForm : Form
    {

        Servidor CanalServidor;
        List<Cliente> CanalClientes = new List<Cliente>();
        List<Broadcast> Broadcasts = new List<Broadcast>();
        List<TicTacToeForm> TicTacToes = new List<TicTacToeForm>();

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
                        lock (info)
                        {
                            Invoke(() =>
                            {
                                if (info != null)
                                {
                                    string mensaje = string.Format("Recibido: {0}", info.Mensaje);

                                    Cliente? canal = null;

                                    if (info?.Broadcast != null)
                                    {
                                        var broadcast = Broadcasts.FirstOrDefault(c => c.ID == info.Broadcast.ID);
                                        if (broadcast != null)
                                        {
                                            foreach (var cliente in CanalClientes)
                                            {
                                                if (cliente != null && cliente is Cliente)
                                                {
                                                    if (!(cliente.Ip == info.Servidor.Ip && cliente.Port == info.Servidor.Port && cliente.ID == null))
                                                        EnviarMensaje(cliente, mensaje);
                                                }
                                            }
                                            broadcast?.Mensajes?.Add(mensaje);
                                            goto refrescar;
                                        }
                                        else
                                        {
                                            canal = CanalClientes.FirstOrDefault(c => c.ID == info.Broadcast.ID);
                                        }
                                    }
                                    else
                                    {
                                        canal = CanalClientes.FirstOrDefault(c => c.Ip == info?.Servidor.Ip && c.Port == info.Servidor.Port);
                                    }

                                    if (canal == null)
                                    {
                                        canal = AgregarCliente(info?.Broadcast?.Nombre ?? info.Servidor.Nombre, info.Servidor.Ip, info.Servidor.Port);
                                        canal.ID = info?.Broadcast?.ID ?? null;
                                    }

                                    if (info?.TicTacToe != null)
                                    {

                                        CrearTicTacToe
                                        (
                                            info.TicTacToe.IdJuego,
                                            Device,
                                            info.TicTacToe.IdJugador1,
                                            Device,
                                            info.TicTacToe.Turno,
                                            canal,
                                            info.TicTacToe.Posicion
                                        );
                                        return;
                                    }
                                    
                                    canal?.Mensajes?.Add(mensaje);

                                refrescar:
                                    RefrescarHistorial();
                                }

                                    
                            });
                        }

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
                MessageBox.Show("Configuración incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    EnviarMensaje(cliente, mensaje, cliente.ID != null ? new() { ID = cliente.ID, Nombre = cliente.Nombre } : null);
                }
                else if (item is Broadcast)
                {
                    Broadcast broadcast = (Broadcast)item;
                    if (broadcast.IdPropietario == Device)
                    {
                        broadcast.Mensajes.Add(txtMensaje.Text);
                        foreach (var cliente in lvContacto.Items)
                        {
                            if (cliente != null && cliente is Cliente)
                            {
                                if (((Cliente)cliente).ID == null)
                                    EnviarMensaje((Cliente)cliente, mensaje, (Broadcast)item);
                            }
                        }
                    }
                    else
                    {
                        Cliente? cliente = CanalClientes.FirstOrDefault(c => c.ID == broadcast?.ID);
                        if (cliente != null)
                            EnviarMensaje(cliente, mensaje);
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

        private void EnviarMensaje(Cliente cliente, string mensaje, Broadcast broadcast = null, TicTacToeInfo ticTacToeInfo = null)
        {
            MensajeInfo info = new MensajeInfo();
            info.Servidor = CanalServidor;
            info.Mensaje = mensaje;
            info.Broadcast = broadcast;
            info.TicTacToe = ticTacToeInfo;

            if (!(broadcast?.IdPropietario == Device))
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

        private void mnuItemTicTacToe_Click(object sender, EventArgs e)
        {
            var item = lvContacto.SelectedItem;
            if (item != null)
            {
                CrearTicTacToe(null, Device, Device, null, Device, (Cliente)item);
            }
        }

        private void CrearTicTacToe(string? idJuego, string idDevice, string idJugador1, string? idJugador2, string turno, Cliente cliente, int? posicion = null)
        {
            var juego = TicTacToes.FirstOrDefault(t => t.IdJuego == idJuego);

            if (juego == null)
            {
                juego = new TicTacToeForm();
                juego.Text = Text;
                juego.ClienteCanal = cliente;
                juego.IdJuego = idJuego ?? Guid.NewGuid().ToString();
                juego.IdJugador = idDevice;
                juego.IdJugador1 = idJugador1;
                if (idJugador2 != null)
                    juego.IdJugador2 = idJugador2;
                juego.Turno = turno;
                juego.OnSelectedItem = (string idJuego, string idJugador, int posicion) =>
                {
                    TicTacToeInfo ticTacToe = new TicTacToeInfo();
                    ticTacToe.IdJuego = idJuego;
                    ticTacToe.IdDevice = Device;
                    ticTacToe.IdJugador1 = juego.IdJugador1;
                    ticTacToe.IdJugador2 = juego.IdJugador2;
                    ticTacToe.Posicion = posicion;
                    ticTacToe.Turno = idJugador;
                    EnviarMensaje(juego.ClienteCanal, string.Empty, null, ticTacToe);
                };

                juego.Show();
                TicTacToes.Add(juego);   
            }
            else
            {
                if (juego.IdJugador2 == null && idJugador2 != null)
                {
                    juego.IdJugador2 = idJugador2;
                }
            }

            if (posicion.HasValue)
            {
                juego.InWrite(turno, posicion.Value);
            }
        }
    }
}