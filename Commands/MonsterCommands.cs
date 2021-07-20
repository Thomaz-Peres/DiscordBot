using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using FirstBotDiscord.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class MonsterCommands : BaseCommandModule
    {
        private readonly MonsterRepository _monsterRepository;

        public MonsterCommands(MonsterRepository monsterRepository) =>
            _monsterRepository = monsterRepository;

        [Command("CreateMonster")]
        [Aliases("cm")]
        [Description("Permite criar um monstro")]
        public async Task CreateMonster(CommandContext ctx, string monsterName, int level, bool isBoss)
        {
            await ctx.TriggerTypingAsync();

            await _monsterRepository.CreateMonster(ctx, monsterName, level, isBoss);
        }

    }
}
