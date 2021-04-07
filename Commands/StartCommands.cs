using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using FirstBotDiscord.Extensions;
using FirstBotDiscord.Entities.Rpg;
using System;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class StartCommands : BaseCommandModule
    {
        [Command("create")]
        [Description("Criar um personagem")]
        public async Task CreateCharacter(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var player = new PlayerBaseEntity();
        }
    }
}