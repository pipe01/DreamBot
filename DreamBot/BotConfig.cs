using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DreamBot
{
    class BotConfig
    {
        public ulong BotID { get; set; }
        public string BotToken { get; set; }
        public int LogLevel { get; set; }
        public ulong[] BotOwners { get; set; } 
    }
}
