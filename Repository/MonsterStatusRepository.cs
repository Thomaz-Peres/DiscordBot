using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Repository
{
    public class MonsterStatusRepository
    {
        public BaseMonstersEntity AddLifeStatus(BaseMonstersEntity monster)
        {
            if (monster.MonsterAtributes.Vitalidade.CurrentValuePoints > 0)
            {
                monster.MonsterLifePoints.MaxValuePoints = (monster.MonsterAtributes.Vitalidade.CurrentValuePoints * 3.00) + 30.00;

                if (monster.MonsterLifePoints.CurrentOrMinValuePoints == monster.MonsterLifePoints.MaxValuePoints)
                    monster.MonsterLifePoints.CurrentOrMinValuePoints = monster.MonsterLifePoints.MaxValuePoints;

                else if (monster.MonsterLifePoints.CurrentOrMinValuePoints < monster.MonsterLifePoints.MaxValuePoints && monster.MonsterLifePoints.CurrentOrMinValuePoints > 0.00)
                    monster.MonsterLifePoints.CurrentOrMinValuePoints += 1.00;
            }
            return monster;
        }

        public BaseMonstersEntity AddManaStatus(BaseMonstersEntity monster)
        {
            if (monster.MonsterAtributes.Sabedoria.CurrentValuePoints > 0)
            {
                monster.MonsterManaPoints.MaxValuePoints = (monster.MonsterAtributes.Sabedoria.CurrentValuePoints * 3.00) + 30.00;
                monster.MagicAttack.MaxValuePoints = (monster.MonsterAtributes.Sabedoria.CurrentValuePoints * 1.00) + 1.50;


                if (monster.MonsterManaPoints.CurrentOrMinValuePoints == monster.MonsterManaPoints.MaxValuePoints)
                    monster.MonsterManaPoints.CurrentOrMinValuePoints = monster.MonsterManaPoints.MaxValuePoints;

                else if (monster.MonsterManaPoints.CurrentOrMinValuePoints < monster.MonsterManaPoints.MaxValuePoints && monster.MonsterManaPoints.CurrentOrMinValuePoints > 0.00)
                    monster.MonsterManaPoints.CurrentOrMinValuePoints += 1.00;
            }
            return monster;
        }

        public BaseMonstersEntity AddMagicAttackStatus(BaseMonstersEntity monster)
        {
            if (monster.MonsterAtributes.Inteligencia.CurrentValuePoints > 0)
            {
                monster.MagicAttack.MaxValuePoints = (monster.MonsterAtributes.Inteligencia.CurrentValuePoints * 3.00) + 5.00;
                monster.MonsterManaPoints.MaxValuePoints = (monster.MonsterAtributes.Inteligencia.CurrentValuePoints * 1.00) + 8.00;
            }
            return monster;
        }

        public BaseMonstersEntity AddPhysicalAttackStatus(BaseMonstersEntity monster)
        {
            if (monster.MonsterAtributes.Forca.CurrentValuePoints > 0)
            {
                monster.PhysicalAttack.MaxValuePoints = (monster.MonsterAtributes.Forca.CurrentValuePoints * 3.00) + 5.00;
            }
            return monster;
        }

        public BaseMonstersEntity AddLuckStatus(BaseMonstersEntity monster)
        {
            if (monster.MonsterAtributes.Sorte.CurrentValuePoints > 0)
            {
                monster.Luck.MaxValuePoints = (monster.MonsterAtributes.Sorte.CurrentValuePoints * 3.00) + 10.00;
            }
            return monster;
        }

        public BaseMonstersEntity AddEvasionStatus(BaseMonstersEntity monster)
        {
            if (monster.MonsterAtributes.Agilidade.CurrentValuePoints > 0)
            {
                monster.Evasion.MaxValuePoints = (monster.MonsterAtributes.Agilidade.CurrentValuePoints * 3.00) + 10.00;
            }
            return monster;
        }

        public BaseMonstersEntity AddPersuationStatus(BaseMonstersEntity monster)
        {
            if (monster.MonsterAtributes.Carisma.CurrentValuePoints > 0)
            {
                monster.Persuation.MaxValuePoints = (monster.MonsterAtributes.Carisma.CurrentValuePoints * 3.00) + 10.00;
            }
            return monster;
        }
    }
}
