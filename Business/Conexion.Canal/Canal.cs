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
}
