using DSharpPlus.CommandsNext;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg.Player
{
    public class PlayerAttacks
    {
        public void ChoiseAttack(CommandContext ctx, CharacterEntity character, BaseMonstersEntity monster, string choise)
        {
            var choiseNumber = Convert.ToInt64(choise);
            switch(choiseNumber)
            {
                case 1:
                    BasicAttack(ctx, character, monster);
                    break;
            }
        }

        public BaseMonstersEntity BasicAttack(CommandContext ctx, CharacterEntity character, BaseMonstersEntity monster)
        {
            var baseAtaque = new Random().Next(5, 15) + new Random().Next((int)character.PhysicalAttack.CurrentOrMinValuePoints, (int)character.PhysicalAttack.MaxValuePoints);

            var armor = new Random().Next(5, 10) + new Random().Next((int)monster.Armor.CurrentOrMinValuePoints, (int)monster.Armor.MaxValuePoints);

            monster.MonsterLifePoints.CurrentOrMinValuePoints -= baseAtaque / (1 + armor / 100);

            return monster;
        }
    }
}
