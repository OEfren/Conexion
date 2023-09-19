using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.Canal
{
    public abstract class Canal
    {

        protected Socket Socket;

        public string Nombre;
        public string Ip;
        public int Port;
        public int MaxCanales;

        public OnNuevoMensaje ListenerNuevoMensaje;

        public delegate void OnNuevoMensaje(string message);

        public abstract void Iniciar();

        public virtual void EnviarMensaje(string mensaje)
        {
            if (Socket != null)
            {
                byte[] content = Encoding.UTF8.GetBytes(mensaje);
                Socket.Send(content);
            }
        }

        public virtual void Detener()
        {
            if (Socket != null)
            {
                Socket.Shutdown(SocketShutdown.Both);
                Socket.Close();
            }
        }

    }
}
