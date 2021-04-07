using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Extensions
{
    public static class WaitingCommandResponse
    {
        public static async Task<string> WaitResponseCreate(this CommandContext ctx, string name, DiscordEmbed embed, TimeSpan? timeToOverride = null)
        {
            InteractivityResult<DiscordMessage> wait;

            wait = await WaitForMessageAsync(ctx, $"Seu nome é {name}", embed, timeToOverride);

            if(wait.TimedOut)
            {
                await ctx.RespondAsync("tempo de resposta expirado!");
            }

            return name;
        }

        public static async Task<InteractivityResult<DiscordMessage>> WaitForMessageAsync(this CommandContext ctx, string message, DiscordEmbed embed, TimeSpan? timeoutoverride = null)
        {
            var vity = ctx.Client.GetInteractivity();
            await ctx.RespondAsync(message, embed);
            return await vity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id, timeoutoverride: timeoutoverride);
        }
    }
}
