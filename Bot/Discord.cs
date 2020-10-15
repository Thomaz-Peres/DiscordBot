using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace FirstBotDiscord.Bot
{
    class Discord
    {
        static string token = "NzI0OTc1Njg3MjY5NjEzNjYw.XvIAOQ.UhNPI6dWzQN4OS95lLCJyJ_RFWU";
        static DiscordClient discord;   // using to interact with discord API.
        static CommandsNextModule commands;
        
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                //criando a configuraçao do bot 
                Token = token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            discord.MessageCreated += async e =>
            {   
                //command module initialisation
                if (e.Message.Content.ToLower().StartsWith("salve"))
                {
                    await e.Message.RespondAsync("Dalee!");
                }
            };

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = ";;"
            });

            commands.RegisterCommands<FirstBotDiscord.Commands.MyCommands>();

            await discord.ConnectAsync();
            //espera infinita, para o bot ficar online continuamente.
            await Task.Delay(-1);
        }
    }
}
