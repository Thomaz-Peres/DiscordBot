using DSharpPlus.CommandsNext;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg.RpgMonsters
{
    internal class MonsterAttacks
    {
        private readonly DataContext _dataContext;
        public MonsterAttacks (DataContext dataContext) =>
            _dataContext = dataContext;

        public int BasicAttack(CommandContext ctx, BaseMonstersEntity monster, PlayerEntity playerCharacter)
        {




            return 0;
        }
    }
}
