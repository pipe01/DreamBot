using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

using Newtonsoft.Json;

using static DreamBot.Functions;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DreamBot.Modules
{
    public class Info : ModuleBase
    {
        MessageConfig msgConfig = GetMessageConfig();
        // Invite
        [Command("invite")]
        [Summary("Invite the bot to your server.")]
        public async Task Invite()
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            await Context.Channel.SendMessageAsync(msgConfig.InviteMsg.Replace("{0}", app.Id.ToString()));
        }

        // Id
        [Command("id")]
        [Summary("Gets the id of specified user.")]
        public async Task Id([Remainder] SocketUser user)
        {
            await Context.Channel.SendMessageAsync(MSGReplace(msgConfig.IdMsg, Context, user));
        }

        // Stats
        [Command("stats")]
        [Summary("Returns stats of the bot.")]
        public async Task Stats()
        {
            var client = Context.Client as DiscordSocketClient;
            var servers = client.Guilds;
            var app = await Context.Client.GetApplicationInfoAsync();
            await Context.Channel.SendMessageAsync($"{Format.Bold("Stats:")}\n" + 
                $"Servers: {servers.Count}\n" +
                $"Channels: {servers.Sum(c => c.Channels.Count)}\n" +
                $"Roles: {servers.Sum(r => r.Roles.Count)}\n" +
                $"Users: {servers.Sum(u => u.Users.Count)}"
            );
        }
    }
}
