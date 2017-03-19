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
    // Admin
    public class Admin : ModuleBase
    {
        MessageConfig msgConfig = new Functions().GetMessageConfig();
        // Kick
        [Command("kick")]
        [Summary("Kicks the user from the server.")]
        public async Task Kick([Remainder] SocketGuildUser user)
        {
            var config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            if (config.Admins.ToList().Contains(Context.User.Id) || config.OwnerID == Context.User.Id)
            {
                await user.KickAsync();
                await Context.Channel.SendMessageAsync(msgConfig.KickMsg.Replace("{0}", user.Mention));
            }
            else
            {
                await Context.Channel.SendMessageAsync(msgConfig.NoPermission.Replace("{0}", Context.User.Mention));
            }
        }

        // Ban
        [Command("ban")]
        [Summary("Ban the user from the server.")]
        public async Task Ban([Remainder] SocketGuildUser user)
        {
            var config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            if (config.Admins.ToList().Contains(Context.User.Id) || config.OwnerID == Context.User.Id)
            {
                await Context.Guild.AddBanAsync(user, 0);
                await Context.Channel.SendMessageAsync(msgConfig.BanMsg.Replace("{0}", user.Mention));
            }
            else
            {
                await Context.Channel.SendMessageAsync(msgConfig.NoPermission.Replace("{0}", Context.User.Mention));
            }
        }

        // Prune
        [Command("prune")]
        [Summary("Removes x amount of messages.")]
        public async Task Prune([Remainder] int amount)
        {
            var config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            if (config.Admins.ToList().Contains(Context.User.Id) || config.OwnerID == Context.User.Id)
            {
                amount = amount + 1;
                if (amount <= 101)
                {
                    var messages = await Context.Channel.GetMessagesAsync(amount).Flatten();
                    await Context.Channel.DeleteMessagesAsync(messages);
                }
                else
                {
                    await Context.Channel.SendMessageAsync(msgConfig.PruneErr);
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(msgConfig.NoPermission.Replace("{0}", Context.User.Mention));
            }
        }

        // Verify
        [Command("verify")]
        [Summary("Verifies the user on the server.")]
        public async Task Verify([Remainder] SocketGuildUser user)
        {
            var config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            if (config.Admins.Contains(Context.User.Id) || config.OwnerID == Context.User.Id)
            {
                if (config.Verifications == true)
                {
                    var role1 = Context.Guild.Roles.First(f => f.Name == "unverified");
                    var role2 = Context.Guild.Roles.First(f => f.Name == "verified");
                    var roles = user.Roles;
                    if (roles.Contains(role1))
                    {
                        await user.RemoveRolesAsync(role1);
                        await user.AddRolesAsync(role2);
                        await Context.Channel.SendMessageAsync(msgConfig.VerifySuc.Replace("{0}", user.Mention));
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync(msgConfig.VerifyErr.Replace("{0}", user.Mention));
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync(msgConfig.VerificationsOff);
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(msgConfig.NoPermission.Replace("{0}", Context.User.Mention));
            }
        }

        // UnVerify
        [Command("unverify")]
        [Summary("Unverifies the user on the server.")]
        public async Task UnVerify([Remainder] SocketGuildUser user)
        {
            var config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            if (config.Admins.Contains(Context.User.Id) || config.OwnerID == Context.User.Id)
            {
                if (config.Verifications == true)
                {
                    var role1 = Context.Guild.Roles.First(f => f.Name == "unverified");
                    var role2 = Context.Guild.Roles.First(f => f.Name == "verified");
                    var roles = user.Roles;
                    if (roles.Contains(role2))
                    {
                        await user.RemoveRolesAsync(role2);
                        await user.AddRolesAsync(role1);
                        await Context.Channel.SendMessageAsync(msgConfig.UnverifySuc.Replace("{0}", user.Mention));
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync(msgConfig.UnverifyErr.Replace("{0}", user.Mention));
                    }
                }
                else
                {
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
