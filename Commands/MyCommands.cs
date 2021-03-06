using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class MyCommands : BaseCommandModule
    {
        [Command("oi")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"Eae meu querido {ctx.User.Mention}");
        }

        [Command("ticker")]
        public async Task Ticker(CommandContext context, string ticker)
        {
            await context.RespondAsync($"O ultimo preço foi {B3Api.B3Api.UpdateList(ticker)}");
        }
    }
}