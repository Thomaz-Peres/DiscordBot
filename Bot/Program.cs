using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using FirstBotDiscord.Commands;
using FirstBotDiscord.Configurations;
using FirstBotDiscord.Database;
using FirstBotDiscord.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FirstBotDiscord.Bot
{
    public class Bot
    {
        public DataContext Database { get; private set; }
        public StartsRepository StartsRepository { get; private set; }
        public ItemRepository ItemRepository { get; private set; }

        public static void Main(string[] args) =>
            new Bot().RodandoBot(args).GetAwaiter().GetResult();

        public async Task RodandoBot(string[] args)
        {
            var discord = new DiscordClient(new DiscordConfiguration
            {
                Token = Parameters.token,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });


            discord.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = System.TimeSpan.FromSeconds(30),
                PollBehaviour = PollBehaviour.KeepEmojis,
                PaginationBehaviour = PaginationBehaviour.Ignore,
                PaginationDeletion = PaginationDeletion.KeepEmojis,
            });


            this.Database = new DataContext();
            this.StartsRepository = new StartsRepository(this.Database);
            this.ItemRepository = new ItemRepository(this.Database);

            var services = new ServiceCollection()
                .AddSingleton(this.Database)
                .AddSingleton(this.StartsRepository)
                .AddSingleton(this.ItemRepository)
                .BuildServiceProvider();


            var commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                Services = services,
                StringPrefixes = Parameters.Prefix,
                CaseSensitive = true,
                EnableDms = false
            });

            commands.RegisterCommands<StartCommands>();
            commands.RegisterCommands<LavaLinkCommands>();
            commands.RegisterCommands<ItemCommands>();
            commands.RegisterCommands<B3Commands>();

            //B3Api.B3Api.B3(args);

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

        private Task C_InteractionCreated(DiscordClient sender, InteractionCreateEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private Task Discord_MessageReactionAdded(DiscordClient sender, MessageReactionAddEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}

