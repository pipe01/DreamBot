using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace DreamBot
{
    public class CommandHandler
    {
        // Create cmds
        private CommandService cmds;

        // Create client
        private DiscordSocketClient client;
        
        // Create map
        private IDependencyMap map;
        
        public async Task Install(IDependencyMap _map)
        {
            // Define Client
            client = _map.Get<DiscordSocketClient>();

            // Define cmds
            cmds = new CommandService();

            // Add cmds To _map
            _map.Add(cmds);

            // Define map
            map = _map;

            await cmds.AddModulesAsync(Assembly.GetEntryAssembly());

            client.MessageReceived += HandleCommand;
        }

        public async Task HandleCommand(SocketMessage parmMsg)
        {
            var msg = (SocketUserMessage)parmMsg;
            if (msg == null) return;

            int argPos = 0;
            if (!(msg.HasMentionPrefix(client.CurrentUser, ref argPos) || msg.HasCharPrefix('/', ref argPos))) return;

            var context = new CommandContext(client, msg);

            var result = await cmds.ExecuteAsync(context, argPos, map);

            if (!result.IsSuccess)
                await msg.Channel.SendMessageAsync($"**ERROR** {result.ErrorReason}");
        }
    }
}
