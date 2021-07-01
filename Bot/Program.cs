using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.EventHandling;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using FirstBotDiscord.Commands;
using FirstBotDiscord.Configurations;
using FirstBotDiscord.Database;
using FirstBotDiscord.Repository;
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
        public CharactersRepository CharactersRepository { get; private set; }
        public AtributesRepository AtributesRepository { get; private set; }
        public MonsterRepository MonsterRepository { get; private set; }

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

            discord.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = System.TimeSpan.FromSeconds(30),
                PollBehaviour = PollBehaviour.KeepEmojis,
                PaginationBehaviour = PaginationBehaviour.Ignore,
                PaginationDeletion = PaginationDeletion.KeepEmojis
            });

            this.Database = new DataContext();
            this.StartsRepository = new StartsRepository(this.Database);
            this.ItemRepository = new ItemRepository(this.Database);
            this.CharactersRepository = new CharactersRepository(this.Database);
            this.AtributesRepository = new AtributesRepository(this.Database);
            this.MonsterRepository = new MonsterRepository(this.Database);


            var services = new ServiceCollection()
                .AddSingleton(this.Database)
                .AddSingleton(this.StartsRepository)
                .AddSingleton(this.ItemRepository)
                .AddSingleton(this.CharactersRepository)
                .AddSingleton(this.AtributesRepository)
                .AddSingleton(this.MonsterRepository)
                .BuildServiceProvider();


            var commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                Services = services,
                StringPrefixes = Parameters.Prefix,
                CaseSensitive = true,
                EnableDms = false
            });

            commands.RegisterCommands<StartCommands>();
            commands.RegisterCommands<AssignAtributtesCharacter>();
            commands.RegisterCommands<StatusCommands>();
            commands.RegisterCommands<ItemCommands>();
            commands.RegisterCommands<MonsterCommands>();
            commands.RegisterCommands<LavaLinkCommands>();
            commands.RegisterCommands<B3Commands>();

            //B3Api.B3Api.B3(args);

            // var endPoint = new ConnectionEndpoint
            // {
            //     Hostname = "127.0.0.1",
            //     Port = 2333
            // };

            // var lavaLinkConfig = new LavalinkConfiguration
            // {
            //     Password = "youshallnotpass",
            //     RestEndpoint = endPoint,
            //     SocketEndpoint = endPoint
            // };

            // var lavalink = discord.UseLavalink();
            await discord.ConnectAsync();
            // await lavalink.ConnectAsync(lavaLinkConfig);

            //espera infinita, para o bot ficar online continuamente.
            await Task.Delay(-1);
        }
    }
}

