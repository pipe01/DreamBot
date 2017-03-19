using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

using Newtonsoft.Json;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DreamBot.Modules
{
    public class Info : ModuleBase
    {
        // Invite
        [Command("invite")]
        [Summary("Invite the bot to your server.")]
        public async Task Invite()
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            await Context.Channel.SendMessageAsync($"You can invite me by clicking this link: https://discordapp.com/oauth2/authorize?permissions=2146958463&scope=bot&client_id=" + app.Id);
        }

        // Id
        [Command("id")]
        [Summary("Gets the id of specified user.")]
        public async Task Id([Remainder] SocketUser user)
        {
            await Context.Channel.SendMessageAsync($"{user.Mention}'s id is {user.Id}");
        }

        // Info
        [Command("info")]
        [Summary("Returns info about the bot.")]
        public async Task BotInfo()
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            await Context.Channel.SendMessageAsync(
                $"{Format.Bold("Info")}\n" +
                $"- Author: {app.Owner.Username}\n" +
                $"- Base: Discord.Net ({DiscordConfig.Version})\n" +
                $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                $"- Uptime: {(DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss")}\n\n" +
                $"{Format.Bold("Stats")}\n" +
                $"- Heap Size: {Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString()}\n" +
                $"- Servers: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
                $"- Channels: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}\n" +
                $"- Users: {((Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count))}"
            );
        }
    }
}
