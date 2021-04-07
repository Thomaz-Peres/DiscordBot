//using DSharpPlus;
//using DSharpPlus.CommandsNext;
//using DSharpPlus.Lavalink;
//using FirstBotDiscord.Commands;
//using FirstBotDiscord.Configurations;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FirstBotDiscord.Bot
//{
//    public class Bot
//    {
//        public DiscordClient Client { get; private set; }
//        public CommandsNextExtension Commands { get; private set; }

//        public Bot(DiscordConfiguration discordConfiguration) => Client = new DiscordClient(discordConfiguration);

//        public Task ConectarAsync() => Client.ConnectAsync();

//        public void GeneralCommands(CommandsNextConfiguration config)
//        {
//            this.Commands = Client.UseCommandsNext(config);

//            this.Commands.RegisterCommands<StartCommands>();
//            this.Commands.RegisterCommands<LavaLinkCommands>();
//            this.Commands.RegisterCommands<B3Commands>();
//        }
//    }
//}
