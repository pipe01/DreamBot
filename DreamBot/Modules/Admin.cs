﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

using static DreamBot.Functions;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace DreamBot.Modules
{
    // Admin
    public class Admin : ModuleBase
    {
        MessageConfig msgConfig = GetMessageConfig();
        // Kick
        [Command("kick")]
        [Summary("Kicks the user.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task Kick(SocketGuildUser user = null)
        {
            if (user != null)
            {
                var users = await Context.Guild.GetUsersAsync();
                if (users.Contains(user))
                {
                    if (user != Context.User)
                    {
                        await user.KickAsync();
                        await Context.Channel.SendMessageAsync(MSGReplace(msgConfig.KickMsg, Context, user));
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync(MSGReplace(msgConfig.ErrSelf, Context, user));
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync(MSGReplace(msgConfig.KickErrNoUser, Context, user));
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(MSGReplace(msgConfig.KickErrArgs, Context, user));
            }
        }

        // Ban
        [Command("ban")]
        [Summary("Bans the user.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task Ban([Remainder] SocketGuildUser user)
        {
            if (user != Context.User)
            {
                await Context.Guild.AddBanAsync(user, 0);
                await Context.Channel.SendMessageAsync(msgConfig.BanMsg.Replace("{0}", user.Mention));
            }
            else
            {
                await Context.Channel.SendMessageAsync(msgConfig.ErrSelf.Replace("{0}", Context.User.Mention));
            }
        }

        // Prune
        [Command("prune")]
        [Summary("Removes messages.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task Prune([Remainder] int amount)
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

        // Prune
        [Command("uprune")]
        [Summary("Removes messages by user.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task UPrune(SocketUser user, [Remainder] int amount)
        {
            amount = amount + 1;
            if (amount <= 101)
            {
                var messages = await Context.Channel.GetMessagesAsync(amount).Flatten();
                await Context.Channel.DeleteMessagesAsync(messages);
            }
            else
            {
                await Context.Channel.SendMessageAsync(msgConfig.UPruneErr);
            }
        }

        // Verify
        [Command("verify")]
        [Summary("Verifies the user on the server.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task Verify([Remainder] SocketGuildUser user)
        {
            var config = GetServerConfig((SocketGuild)Context.Guild);
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

        // UnVerify
        [Command("unverify")]
        [Summary("Unverifies the user on the server.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task UnVerify([Remainder] SocketGuildUser user)
        {
            var config = GetServerConfig((SocketGuild)Context.Guild);
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
    }
}
