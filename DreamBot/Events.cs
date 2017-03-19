using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace DreamBot
{
    public class Events
    {
        DiscordSocketClient client;
        public Events(DiscordSocketClient _client)
        {
            client = _client;
        }

        public async Task CreateEvents()
        {

        }
    }
}