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

        public List<string> Mensajes = new List<string>();

        public bool Conectado { get; internal set; }

        public override void Iniciar()
        {

            IPAddress address = IPAddress.Parse(Ip);
            IPEndPoint remoteEndPoint = new IPEndPoint(address, Port);

            Socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Socket.Connect(remoteEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
