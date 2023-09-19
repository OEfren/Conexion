using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.Canal
{
    public class Cliente : Canal
    {

        public bool Conectado { get; internal set; }

        public override void Iniciar()
        {

            IPAddress address = IPAddress.Parse(Ip);
            IPEndPoint remoteEndPoint = new IPEndPoint(address, Port);

            Socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // Connect to Remote EndPoint
                Socket.Connect(remoteEndPoint);
                Conectado = true;


                byte[] bytes = new byte[1024];
                int bytesRec = Socket.Receive(bytes);
                string mensaje = Encoding.UTF8.GetString(bytes, 0, bytesRec);

                if (ListenerNuevoMensaje != null)
                    ListenerNuevoMensaje(mensaje);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public override void EnviarMensaje(string mensaje)
        {
            mensaje = string.Format("Cliente - {0}: {1} ", Nombre, mensaje);
            base.EnviarMensaje(mensaje);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
