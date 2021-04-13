using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class HelpCommands : BaseCommandModule
    {
        [Command("help")]
        [Description("Mostra todos os comandos do bot")]
        public async Task HelpCommand(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var embed = new DiscordEmbedBuilder();

            return;
        }
    }
}
