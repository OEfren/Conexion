using System.Net.Sockets;
using System.Net;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;

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
                                byte[] bytes = new byte[1024];
                                int bytesRec = socket.Receive(bytes);
                                string mensaje = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                                if (mensaje != null)
                                {
                                    MensajeInfo info = JsonConvert.DeserializeObject<MensajeInfo>(mensaje);
                                    if (info != null && ListenerNuevoMensaje != null)
                                        ListenerNuevoMensaje(info);
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