﻿using System;
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
    public class Manage : ModuleBase
    {
        MessageConfig msgConfig = GetMessageConfig();

        // Verification
        [Command("verifications")]
        [Summary("Enables/Disables verifications.")]
        public async Task Verifications()
        {
            ServerConfig config = GetServerConfig((SocketGuild)Context.Guild);
            if (Context.User.Id == config.OwnerID)
            {
                if (config.Verifications == false)
                {
                    var jsonData = JsonConvert.SerializeObject(new ServerConfig { ServerName = config.ServerName, ServerID = config.ServerID, OwnerID = config.OwnerID, Verifications = true });
                    File.WriteAllText($"configs/{Context.Guild.Id}.json", jsonData);
                    EnableVerifications((SocketGuild)Context.Guild);
                    await Context.Channel.SendMessageAsync(msgConfig.VerificationsEnabled);
                }
                else
                {
                    var jsonData = JsonConvert.SerializeObject(new ServerConfig { ServerName = config.ServerName, ServerID = config.ServerID, OwnerID = config.OwnerID, Verifications = false });
                    File.WriteAllText($"configs/{Context.Guild.Id}.json", jsonData);
                    DisableVerifications((SocketGuild)Context.Guild);
                    await Context.Channel.SendMessageAsync(msgConfig.VerificationsDisabled);
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(msgConfig.NoPermission.Replace("{0}", Context.User.Mention));
            }
        }
    }
}
