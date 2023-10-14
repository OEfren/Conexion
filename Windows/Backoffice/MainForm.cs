using Backoffice.Dialog;
using Backoffice.Game.TicTacToe;
using Conexion.Canal;
using Conexion.Security;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Backoffice
{
    public partial class MainForm : Form
    {

        Servidor CanalServidor;
        List<Cliente> CanalClientes = new List<Cliente>();
        List<Broadcast> Broadcasts = new List<Broadcast>();
        List<TicTacToeForm> TicTacToes = new List<TicTacToeForm>();
        List<ArchivoInfo> Archivos = new List<ArchivoInfo>();

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

                                    if (info?.Archivo != null)
                                    {
                                        if(info.Archivo.IsCompleto)
                                        {
                                            Task.Run(() =>
                                            {
                                                var archivo = Archivos.FirstOrDefault(a => a.ID == info.Archivo.ID);
                                                if (archivo != null)
                                                {
                                                    DirectoryInfo directorio = System.IO.Directory.CreateDirectory("Recibido");
                                                    archivo.Path = directorio.FullName + "\\" + info.Archivo.Name;
                                                    using (var fs = new FileStream(archivo.Path, FileMode.Create, FileAccess.Write))
                                                    {
                                                        archivo.Paths.ForEach(path =>
                                                        {
                                                            byte[] content = File.ReadAllBytes(path);
                                                            fs.Write(content, 0, content.Length);
                                                        });                                                        
                                                    }

                                                    archivo.Paths.ForEach(path => System.IO.File.Delete(path));
                                                }

                                                Invoke(() =>
                                                {
                                                    MessageBox.Show(string.Format("Se ha recibio el archivo {0}, se encuentra en la carpeta {1}", archivo.Name, archivo.Path), "Archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                });

                                            });
                                            return;
                                        }
                                        else
                                        {
                                            DirectoryInfo directorio = System.IO.Directory.CreateDirectory("Recibido");
                                            var archivo = Archivos.FirstOrDefault(a => a.ID == info.Archivo.ID);
                                            if (archivo == null)
                                            {
                                                Archivos.Add(archivo = info.Archivo);
                                            }
                                            string tempName = directorio.FullName + "\\" + Guid.NewGuid().ToString();
                                            archivo.Paths.Add(tempName);
                                            System.IO.File.WriteAllBytes(tempName, info.Archivo.Content);
                                            archivo.Content = null;

                                            return;
                                        }
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

        private void EnviarMensaje(Cliente cliente, string mensaje, Broadcast broadcast = null, TicTacToeInfo ticTacToeInfo = null, ArchivoInfo archivo = null)
        {
            MensajeInfo info = new MensajeInfo();
            info.Data = Guid.NewGuid().ToString();
            info.Servidor = CanalServidor;
            info.Mensaje = mensaje;
            info.Broadcast = broadcast;
            info.TicTacToe = ticTacToeInfo;
            info.Archivo = archivo;

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

        private void mnuItemEnviarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                var item = lvContacto.SelectedItem;
                if (item != null)
                {
                    DialogResult result = dlgFile.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Task.Run(() => 
                        {
                            string fileName = dlgFile.FileName;
                            FileInfo fileInfo = new FileInfo(fileName);
                            string ID = Guid.NewGuid().ToString();
                            using (Stream source = File.OpenRead(fileName))
                            {
                                ArchivoInfo archivo;
                                byte[] buffer = new byte[1024];
                                int bytesRead;
                                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    archivo = new ArchivoInfo();
                                    archivo.ID = ID;
                                    archivo.Content = buffer;

                                    Invoke(() =>
                                    {
                                        Cliente cliente = (Cliente)item;
                                        EnviarMensaje(cliente, string.Empty, null, null, archivo);
                                    });
                                }

                                archivo = new ArchivoInfo();
                                archivo.ID = ID;
                                archivo.IsCompleto = true;
                                archivo.Name = fileInfo.Name;
                                Invoke(() =>
                                {
                                    Cliente cliente = (Cliente)item;
                                    EnviarMensaje(cliente, string.Format("Archivo {0} enviado", fileInfo.Name), null, null, archivo);
                                });
                            }
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Ocurrió un problema al enviar el archivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}