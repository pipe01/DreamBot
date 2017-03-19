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
    class Functions
    {
        public MessageConfig GetMessageConfig()
        {
            string filePath = "configs/messages.json";
            var jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<MessageConfig>(jsonString);
        }
        public BotConfig GetBotConfig()
        {
            string filePath = "configs/bot.json";
            var jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<BotConfig>(jsonString);
        }

        public ServerConfig GetServerConfig(SocketGuild server)
        {
            string filePath = $"configs/{server.Id}.json";
            var jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ServerConfig>(jsonString);
        }

        public void CheckFile(SocketGuild server)
        {
            var filePath = $"configs/{server.Id}.json";
            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);
                var config = JsonConvert.DeserializeObject<ServerConfig>(content);
                if (config.ServerName == "" || config.ServerID == 0 || config.OwnerID == 0 || config.Admins == null || config.Verifications != false && config.Verifications != true)
                {
                    var jsonString = JsonConvert.SerializeObject(new ServerConfig { ServerName = server.Name, ServerID = server.Id, OwnerID = server.Owner.Id, Admins = Array.Empty<ulong>(), Verifications = false });
                    File.WriteAllText(filePath, jsonString);
                }
            }
            else
            {
                var jsonString = JsonConvert.SerializeObject(new ServerConfig { ServerName = server.Name, ServerID = server.Id, OwnerID = server.Owner.Id, Admins = Array.Empty<ulong>(), Verifications = false });
                File.WriteAllText(filePath, jsonString);
            }
        }

        public async void EnableVerifications(SocketGuild server)
        {
            IRole unverified;
            IRole verified;
            if (!server.Roles.Any(a => a.Name == "unverified"))
            {
                var perms = new GuildPermissions(createInstantInvite: false, kickMembers: false, banMembers: false, administrator: false, manageChannels: false, manageGuild: false, addReactions: false, readMessages: true, sendMessages: false, sendTTSMessages: false, manageMessages: false, embedLinks: false, attachFiles: false, readMessageHistory: true, mentionEveryone: false, useExternalEmojis: false, connect: false, speak: false, muteMembers: false, deafenMembers: false, moveMembers: false, useVoiceActivation: false, changeNickname: false, manageNicknames: false, manageRoles: false, manageWebhooks: false, manageEmojis: false);
                unverified = await server.CreateRoleAsync("unverified", perms, new Color(40, 40, 40));
            }
            else
            {
                unverified = server.Roles.First(f => f.Name.Equals("unverified"));
            }

            if (!server.Roles.Any(a => a.Name == "verified"))
            {
                verified = await server.CreateRoleAsync("verified");
            }
            else
            {
                verified = server.Roles.First(f => f.Name.Equals("verified"));
            }

            foreach (SocketGuildChannel channel in server.Channels)
            {
                var perms = new OverwritePermissions(createInstantInvite: PermValue.Deny, manageChannel: PermValue.Deny, addReactions: PermValue.Deny, readMessages: PermValue.Allow, sendMessages: PermValue.Deny, sendTTSMessages: PermValue.Deny, manageMessages: PermValue.Deny, embedLinks: PermValue.Deny, attachFiles: PermValue.Deny, readMessageHistory: PermValue.Allow, mentionEveryone: PermValue.Deny, useExternalEmojis: PermValue.Deny, connect: PermValue.Deny, speak: PermValue.Deny, muteMembers: PermValue.Deny, deafenMembers: PermValue.Deny, moveMembers: PermValue.Deny, useVoiceActivation: PermValue.Deny, manageWebhooks: PermValue.Deny, managePermissions: PermValue.Deny);
                if (!channel.GetPermissionOverwrite(unverified).Equals(perms))
                {
                    await channel.AddPermissionOverwriteAsync(unverified, perms);
                }
            }

            foreach (SocketGuildUser user in server.Users)
            {
                var roles = user.Roles.ToList();
                if (roles.Count() == 0)
                {
                    await user.AddRolesAsync(server.Roles.First(f => f.Name == "unverified"));
                }
            }
        }

        public async void DisableVerifications(SocketGuild server)
        {
            foreach (SocketGuildUser user in server.Users)
            {
                var role1 = server.Roles.First(f => f.Name == "unverified");
                var role2 = server.Roles.First(f => f.Name == "verified");
                var roles = user.Roles.ToList();

                if (roles.Contains(role1))
                {
                    await user.RemoveRolesAsync(role1);
                }
                else if (roles.Contains(role2))
                {
                    await user.RemoveRolesAsync(role2);
                }
            }
        }
    }
}
