using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DreamBot
{
    public class ServerConfig
    {
        public string ServerName { get; set; }
        public ulong ServerID { get; set; }
        public ulong OwnerID { get; set; }
        public bool Verifications { get; set; }
    }
}
