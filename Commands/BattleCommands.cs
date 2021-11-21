using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using FirstBotDiscord.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{    
    public class BattleCommands : BaseCommandModule
    {
        private readonly BattleService _battleService;
        public BattleCommands(BattleService battleService)
        {
            _battleService = battleService;
        }

        [Command("SearchEnemy")]
        [Aliases("se")]
        [Description("Procura inimigo para batalhar")]
        public async Task SearchEnemyCommand(CommandContext ctx, int monsterLevel)
        {
            await ctx.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder();
            embed.WithDescription("Procurando um monstro");
            await ctx.RespondAsync(embed.Build());

            await _battleService.SearchEnemy(ctx, monsterLevel);
        }
    }
}
