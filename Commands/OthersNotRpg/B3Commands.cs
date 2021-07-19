using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class B3Commands : BaseCommandModule
    {
        [Command("ticker")]
        public async Task Ticker(CommandContext context, string ticker)
        {
            await context.RespondAsync($"O ultimo preço foi {B3Api.B3Api.UpdateList(ticker)}");
        }
    }
}
