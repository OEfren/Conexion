using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Conexion.Canal
{

    [JsonObject(MemberSerialization.OptIn)]
    public class MensajeInfo
    {
        [JsonProperty]
        public Servidor Servidor { get; set; }
        [JsonProperty]
        public string Mensaje { get; set; }
    }


    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Canal
    {

        protected Socket Socket;
        [JsonProperty]
        public string Nombre;
        [JsonProperty]
        public string Ip;
        [JsonProperty]
        public int Port;
        public int MaxCanales;

        public OnNuevoMensaje ListenerNuevoMensaje;

        public delegate void OnNuevoMensaje(MensajeInfo mensaje);

        public abstract void Iniciar();

        public virtual void EnviarMensaje(MensajeInfo info)
        {
            if (Socket != null)
            {
                string mensaje = Newtonsoft.Json.JsonConvert.SerializeObject(info);
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
