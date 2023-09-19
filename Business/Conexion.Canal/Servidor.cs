using System.Net.Sockets;
using System.Net;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;

namespace Conexion.Canal
{
    public class Servidor : Canal
    {

        public override void Iniciar()
        {
            try
            {
                IPAddress address = IPAddress.Parse(Ip);
                IPEndPoint localEndPoint = new IPEndPoint(address, Port);

                Socket listener = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Socket = listener.Accept();

                while (true)
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = Socket.Receive(bytes);
                    string mensaje = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                    if (mensaje != null)
                    {
                        MensajeInfo info = JsonConvert.DeserializeObject<MensajeInfo>(mensaje);

                        if (info != null && ListenerNuevoMensaje != null)
                            ListenerNuevoMensaje(info);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Detener();
            }
        }

        
    }
}