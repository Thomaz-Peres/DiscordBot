using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.VoiceNext;
using DSharpPlus.VoiceNext.Codec;
using FirstBotDiscord.Commands;
using Newtonsoft.Json;


namespace FirstBotDiscord.Bot
{
    class Discord
    {
        static DiscordClient client;    // using to interact with discord API.
        static CommandsNextModule commands;
        //static InteractivityModule interactivity;
        static VoiceNextClient voice;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        
        static async Task MainAsync(string[] args)
        {
            
            client = new DiscordClient(new DiscordConfiguration
            {       //criando a configuraçao do bot 
                Token = "Token your bot",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });
  
            client.MessageCreated += async e =>
            {   //command module initialisation
                if (e.Message.Content.ToLower().StartsWith("salve"))
                    await e.Message.RespondAsync("Dalee!");
            };

            //bot class
            commands = client.UseCommandsNext(new CommandsNextConfiguration 
            {   //comandos para "ativar" o bot.
                EnableDms = true,
                EnableMentionPrefix = true,
                StringPrefix = "-"
            });

            commands.RegisterCommands<MyCommands>();
            voice = client.UseVoiceNext();
            //interactivity = client.UseInteractivity(new InteractivityConfiguration());
            
            
            await client.ConnectAsync();
            await Task.Delay(-1);   //espera infinita, para o bot ficar online continuamente.
        }
    }
}
