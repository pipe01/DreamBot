﻿using System;
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
        // Kick
        [Command("kick")]
        [Summary("Kicks the user from the server.")]
        public async Task Kick([Remainder] SocketGuildUser user)
        {
            var config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            if (config.Admins.ToList().Contains(Context.User.Id) || config.OwnerID == Context.User.Id)
            {
                await user.KickAsync();
                await Context.Channel.SendMessageAsync($"{user.Mention} has been kicked.");
            }
            else
            {
                await Context.Channel.SendMessageAsync(msg.NoPermission.Replace("{0}", Context.User.Mention));
            }
        }

        // Verify
        [Command("verify")]
        [Summary("Verifies the user on the server.")]
        public async Task Verify([Remainder] SocketGuildUser user)
        {
            var config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            if (config.Verifications == true)
            {
                if (config.Admins.Contains(Context.User.Id))
                {
                    var role1 = Context.Guild.Roles.First(f => f.Name == "unverified");
                    var role2 = Context.Guild.Roles.First(f => f.Name == "verified");
                    var roles = user.Roles;
                    if (roles.Contains(role1))
                    {
                        await user.RemoveRolesAsync(role1);
                        await user.AddRolesAsync(role2);
                        await Context.Channel.SendMessageAsync($"{user.Mention} has been verified by {Context.User.Mention}");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{user.Mention} is already verified.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} you are not allowed to do that!");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("This server doesn't use verification service.");
            }
        }

        // UnVerify
        [Command("unverify")]
        [Summary("Unverifies the user on the server.")]
        public async Task UnVerify([Remainder] SocketGuildUser user)
        {
            var config = new Functions().GetServerConfig((SocketGuild)Context.Guild);
            if (config.Verifications == true)
            {
                if (config.Admins.Contains(Context.User.Id))
                {
                    var role1 = Context.Guild.Roles.First(f => f.Name == "unverified");
                    var role2 = Context.Guild.Roles.First(f => f.Name == "verified");
                    var roles = user.Roles;
                    if (roles.Contains(role2))
                    {
                        await user.RemoveRolesAsync(role2);
                        await user.AddRolesAsync(role1);
                        await Context.Channel.SendMessageAsync($"{user.Mention} has been unverified by {Context.User.Mention}");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{user.Mention} is already unverified member.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} you are not allowed to do that!");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("This server doesn't use verification service.");
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
                await Context.Channel.SendMessageAsync($"{user.Mention} has been banned.");
            }
            else
            {
                await Context.Channel.SendMessageAsync(msg.NoPermission.Replace("{0}", Context.User.Mention));
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
                    await Context.Channel.SendMessageAsync($"You can only prune maximum of 100 messages at the time.");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync(msg.NoPermission.Replace("{0}", Context.User.Mention));
            }
        }
    }
}
