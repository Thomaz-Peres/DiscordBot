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
    class Bot
    {
        static readonly string token = "";  // Token your discord bot
        static readonly string CredentialCode = ""; // Token from B3 api 
        static DiscordClient discord;   // using to interact with discord API.
        static CommandsNextExtension commands;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            #region Conexao discord
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });
            #endregion

            #region Comandos
            commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "/" }
            });

            commands.RegisterCommands<MyCommands>();
            commands.RegisterCommands<LavaLinkCommands>();
            #endregion

            #region B3 api webmat
            HubConnection connection;

            connection = new HubConnectionBuilder().WithUrl(new System.Uri("http://b3api.webmat.com.br/HubConnection?Token=" + CredentialCode)).WithAutomaticReconnect().Build();
            connection.On<string>("LogOut", (msg) => LogOut(msg));

            connection.On<IEnumerable<Ticks>>("UpdateList", (ticklist) => UpdateList(ticklist));

            await connection.StartAsync();
            #endregion


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

        public static void LogOut(string msg)
        {
            System.Console.WriteLine(msg);
        }

        public static void UpdateList(IEnumerable<Ticks> tickList)
        {
            System.Console.Clear();
            foreach (Ticks data in tickList.OrderBy(q => q.Customer.Symbol))
            {
                System.Console.WriteLine(data.Time + " - " + data.Customer.Symbol);
            }
        }
    }
}

