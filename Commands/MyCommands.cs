using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class MyCommands
    {
        [Command("oi")]
        public async Task Hi(CommandContext ctx)
        {
            await ctx.RespondAsync($"Eae meu querido {ctx.User.Mention}");
        }

        [Command("p")]
        public async Task Random(CommandContext ctx, int min, int max)
        {
            var rnd = new Random();
            await ctx.RespondAsync($"Your random number is: {rnd.Next(min, max)}");
        }

        


    }
}