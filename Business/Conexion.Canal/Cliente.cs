using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Conexion.Security;

namespace Conexion.Canal
{
    public class Cliente : Canal
    {
        public string? ID { get; set; }

        public List<string> Mensajes = new List<string>();

        protected Socket Socket;

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

        public void EnviarMensaje(MensajeInfo info)
        {
            try
            {
                if (Socket != null)
                {
                    string mensaje = Newtonsoft.Json.JsonConvert.SerializeObject(info);
                    byte[] content = Encoding.UTF8.GetBytes(info.Data + Encrypter.EncryptString(mensaje, info.Data));
                    Socket.Send(content);
                }
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

        public override void Detener()
        {
            if (Socket != null)
            {
                Socket.Close();
            }
        }


    }
}
