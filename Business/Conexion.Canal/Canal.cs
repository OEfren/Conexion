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
        public string IdPropietario { get; set; }

        [JsonProperty]
        public Servidor Servidor { get; set; }
        [JsonProperty]
        public string Mensaje { get; set; }

        [JsonProperty]
        public Broadcast Broadcast { get; set; }
    }


    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Canal
    {
        
        [JsonProperty]
        public string Nombre;
        
        [JsonProperty]
        public string Ip;

        [JsonProperty]
        public int Port;

        public int MaxCanales;

        public abstract void Iniciar();

        public abstract void Detener();
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Broadcast
    {
        [JsonProperty]
        public string ID { get; set; }
        [JsonProperty]
        public string Nombre { get; set; }

        [JsonProperty]
        public string Descripcion { get; set; }

        [JsonProperty]
        public string IdPropietario { get; set; }

        public List<string> Mensajes { get; set; }

        public Broadcast()
        {
            Mensajes = new List<string>();
        }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
