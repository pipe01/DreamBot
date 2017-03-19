using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

using Newtonsoft.Json;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace DreamBot
{
    class DreamBot
    {
        // Create _client
        private DiscordSocketClient _client;

        // Create _cmds
        private CommandHandler _cmds;

        // Create _map
        private DependencyMap _map;

        // Create _config
        private DiscordSocketConfig _config;

        public async Task Start()
        {
            // Get BotConfig
            var config = new Functions().GetBotConfig();

            // Console
            Console.Title = "DreamBot";

            // Define _config
            _config = new DiscordSocketConfig()
            {
                LogLevel = (LogSeverity)config.LogLevel
            };

            // Define _client
            _client = new DiscordSocketClient(_config);

            // Define _client.Log
            _client.Log += Log;
            
            // Login & Connect
            await _client.LoginAsync(TokenType.Bot, config.BotToken);

            await _client.StartAsync();

            // Define _map
            _map = new DependencyMap();
            _map.Add(_client);

            // Define _cmds
            _cmds = new CommandHandler();
            await _cmds.Install(_map);

            // Define _client.Ready
            _client.Ready += ClientReady;

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine($"[{msg.Severity}] [{msg.Source}] [{msg.Message}]");
            return Task.CompletedTask;
        }

        public async Task ClientReady()
        {
            foreach(SocketGuild server in _client.Guilds)
            {
                Console.WriteLine($"[{LogSeverity.Info}] [Guild] [{server.Name}]");
                new Functions().CheckFile(server);
                ServerConfig config = new Functions().GetServerConfig(server);
                if (config.Verifications == true)
                {
                    new Functions().EnableVerifications(server);
                }
            }
        }
    }
}
