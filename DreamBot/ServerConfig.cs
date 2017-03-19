using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DreamBot
{
    class ServerConfig
    {
        [JsonProperty("ServerName")]
        public string ServerName { get; set; }
        [JsonProperty("ServerID")]
        public ulong ServerID { get; set; }
        [JsonProperty("OwnerID")]
        public ulong OwnerID { get; set; }
        [JsonProperty("Admins")]
        public ulong[] Admins { get; set; }
        [JsonProperty("Verifications")]
        public bool Verifications { get; set; }
    }
}
