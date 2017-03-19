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

namespace DreamBot.Modules
{
    // Permissions
    public class Permissions : ModuleBase
    {
        MessageConfig msgConfig = new Functions().GetMessageConfig();
        // Add Admin
        [Command("addadmin"), Summary("Adds the admin for the server.")]
        public async Task AddAdmin([Remainder] SocketUser user)
        {
            new Functions().CheckFile((SocketGuild)Context.Guild);
            ServerConfig config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            var adminList = config.Admins.ToList();
            if (Context.User.Id == config.OwnerID)
            {
                if (!adminList.Contains(user.Id))
                {
                    adminList.Add(user.Id);
                    var jsonData = JsonConvert.SerializeObject(new ServerConfig { ServerName = config.ServerName, ServerID = config.ServerID, OwnerID = config.OwnerID, Admins = adminList.ToArray(), Verifications = config.Verifications });
                    File.WriteAllText($"configs/{Context.Guild.Id}.json", jsonData);
                    await Context.Channel.SendMessageAsync(msgConfig.AddAdminSuc.Replace("{0}", user.Mention));
                }
                else
                {
                    await Context.Channel.SendMessageAsync(msgConfig.AddAdminErr.Replace("{0}", user.Mention));
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(msgConfig.NoPermission.Replace("{0}", Context.User.Mention));
            }
        }
        // Remove Admin
        [Command("remadmin"), Summary("Removes the user from the admins.")]
        public async Task RemAdmin([Remainder] SocketUser user)
        {
            new Functions().CheckFile((SocketGuild)Context.Guild);
            ServerConfig config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            var adminList = config.Admins.ToList();
            if (Context.User.Id == config.OwnerID)
            {
                if (adminList.Contains(user.Id))
                {
                    adminList.Remove(user.Id);
                    var jsonData = JsonConvert.SerializeObject(new ServerConfig { ServerName = config.ServerName, ServerID = config.ServerID, OwnerID = config.OwnerID, Admins = adminList.ToArray(), Verifications = config.Verifications });
                    File.WriteAllText($"configs/{Context.Guild.Id}.json", jsonData);
                    await Context.Channel.SendMessageAsync(msgConfig.RemAdminSuc.Replace("{0}", user.Mention));
                }
                else
                {
                    await Context.Channel.SendMessageAsync(msgConfig.RemAdminErr.Replace("{0}", user.Mention));
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(msgConfig.NoPermission.Replace("{0}", Context.User.Mention));
            }
        }
    }
}
