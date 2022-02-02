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

        public int AttackBasicMonsterBasic(CommandContext ctx, BaseMonstersEntity monster, PlayerEntity playerCharacter)
        {
            var baseAtaque = new Random().Next(5, 15) + new Random().Next((int)monster.PhysicalAttack.CurrentOrMinValuePoints, (int)monster.PhysicalAttack.MaxValuePoints);


            // formula sobre defesa a.atk / (1 + b.def / 100)
            /* O que essa formula significa? Significa que um personagem com 0 de DEF irá levar dano exatamente igual ao atk do agressor(atk / 1).Cada ponto de def além do primeiro irá aumentar o divisor em 0,01.Resultado:

            Um personagem com 0 DEF recebe 100 % do dano de um ataque.
            Um personagem com 100 DEF recebe 50 % do dano de um ataque.
            Um personagem com 200 DEF recebe 33,3 % do dano de um ataque.
            Um personagem com 300 DEF recebe 25 % do dano de um ataque.
            Um personagem com 400 DEF recebe 20 % do dano de um ataque. */

            var armor = new Random().Next((int)playerCharacter.MyCharacter.Armor.CurrentOrMinValuePoints, (int)playerCharacter.MyCharacter.Armor.MaxValuePoints);

            playerCharacter.MyCharacter.LifePoints.CurrentOrMinValuePoints -= baseAtaque / (1 + armor / 100);


            return 0;
        }
    }
}
