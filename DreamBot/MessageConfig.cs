using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DreamBot
{
    class MessageConfig
    {
        [JsonProperty("NoPermission")]
        public string NoPermission { get; set; }
    }
}
