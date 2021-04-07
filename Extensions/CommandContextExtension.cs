using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Extensions
{
    public static class CommandContextExtension
    {
        public static Task<DiscordMessage> RespondAsync(this CommandContext ctx, string mensagem)
            => ctx.RespondAsync($"{ctx.User.Mention}, {mensagem}");

        public static Task RespondAsync(this CommandContext ctx, string mensagem, DiscordEmbed embed)
            => ctx.RespondAsync($"{ctx.User.Mention}, {mensagem}", embed: embed);

        public static Task RespondAsync(this CommandContext ctx, DiscordEmbed embed)
            => ctx.RespondAsync($"{ctx.User.Mention}", embed: embed);
    }
}
