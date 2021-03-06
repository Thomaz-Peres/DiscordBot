using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using FirstBotDiscord.Commands;
using FirstBotDiscord.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstBotDiscord.Bot
{
    public class Bot
    {        
        static void Main(string[] args) => MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            #region Conexao discord
            var discord = Configurations.Parameters.discord;

            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = Configurations.Parameters.token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });
            #endregion

            #region Comandos
            var commands = Configurations.Parameters.commands;

            commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { ";" }
            });

            commands.RegisterCommands<MyCommands>();
            commands.RegisterCommands<LavaLinkCommands>();
            #endregion

            B3Api.B3Api.B3(args);

            var endPoint = new ConnectionEndpoint
            {
                Hostname = "127.0.0.1",
                Port = 2333
            };

            var lavaLinkConfig = new LavalinkConfiguration
            {
                Password = "youshallnotpass",
                RestEndpoint = endPoint,
                SocketEndpoint = endPoint
            };

            var lavalink = discord.UseLavalink();

            await discord.ConnectAsync();

            await lavalink.ConnectAsync(lavaLinkConfig);

            //espera infinita, para o bot ficar online continuamente.
            await Task.Delay(-1);
        }
    }
}

