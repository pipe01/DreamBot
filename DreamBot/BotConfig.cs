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
        [JsonProperty("BotID")]
        public ulong BotID { get; set; }
        [JsonProperty("BotToken")]
        public string BotToken { get; set; }
        [JsonProperty("LogLevel")]
        public int LogLevel { get; set; }
        [JsonProperty("BotOwners")]
        public ulong[] BotOwners { get; set; } 
    }
}
