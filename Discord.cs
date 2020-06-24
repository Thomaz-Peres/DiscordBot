using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.VoiceNext;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;
using DSharpPlus.EventArgs;
using DSharpPlus.Net;
using DSharpPlus;


namespace FirstBotDiscord
{
    class Discord
    {
        static DiscordClient client;    // using to interact with discord API.
        static CommandsNextModule commands;
        static InteractivityModule interactivity;
        static VoiceNextClient voice;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        
        static async Task MainAsync(string[] args)
        {
            
            client = new DiscordClient(new DiscordConfiguration
            {       //criando a configuraçao do bot 
                Token = "NzI0OTc1Njg3MjY5NjEzNjYw.XvINuw.kgZ77Qng2eknF7hGSL1qDPNnXGc",
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
                EnableDms = false,
                StringPrefix = "-"
            });

            commands.RegisterCommands<MyCommands>();
            voice = client.UseVoiceNext();
            interactivity = client.UseInteractivity(new InteractivityConfiguration());
            
            
            await client.ConnectAsync();
            await Task.Delay(-1);   //espera infinita, para o bot ficar online continuamente.
        }
    }
}
