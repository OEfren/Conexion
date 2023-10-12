using System.Net.Sockets;
using System.Net;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using Conexion.Security;
using System.Globalization;

namespace Conexion.Canal
{
    public class Servidor : Canal
    {

        private List<Socket> Sockets = new List<Socket>();
        private CancellationTokenSource CancellationToken;
        
        public OnNuevoMensaje ListenerNuevoMensaje;

        public delegate void OnNuevoMensaje(MensajeInfo mensaje);

        public override void Iniciar()
        {
            try
            {
                CancellationToken = new CancellationTokenSource();
                IPAddress address = IPAddress.Parse(Ip);
                IPEndPoint localEndPoint = new IPEndPoint(address, Port);
                Socket listener = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(10);
                while (true)
                {
                    Socket socket = listener.Accept();
                    var task = Task.Factory.StartNew(() =>
                    {
                        while (true && !CancellationToken.IsCancellationRequested)
                        {
                            try
                            {
                                byte[] bytes = new byte[944];
                                int bytesRec = socket.Receive(bytes);
                                lock (socket)
                                {
                                    string mensaje = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                                    System.Diagnostics.Debug.WriteLine("Recibiendo: " + mensaje);
                                    if (mensaje != null)
                                    {
                                        Console.WriteLine(mensaje.ToString());
                                        string key = mensaje.Substring(0, 36);
                                        mensaje = mensaje.Substring(36);
                                        MensajeInfo info = JsonConvert.DeserializeObject<MensajeInfo>(Encrypter.DecryptString(mensaje, key));
                                        if (info != null && ListenerNuevoMensaje != null)
                                            ListenerNuevoMensaje(info);
                                    }
                                }
                            }
                            catch (System.Net.Sockets.SocketException ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    });
                    Sockets.Add(socket);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public override void Detener()
        {
            CancellationToken?.Cancel();
            Sockets.ForEach(s =>
            {
                s.Disconnect(false);
                s.Close();
            });
        }


    }
}