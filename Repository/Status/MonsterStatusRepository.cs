using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Repository.Status
{
    public class MonsterStatusRepository
    {
        public void AddLifeStatus(BaseMonstersEntity monster)
        {
            monster.MonsterLifePoints.MaxValuePoints += (monster.MonsterAtributes.Vitalidade.CurrentValuePoints * 3.00) + 30.00;
            monster.MonsterLifePoints.CurrentOrMinValuePoints = monster.MonsterLifePoints.MaxValuePoints;
        }

        public void AddManaStatus(BaseMonstersEntity monster)
        {
            monster.MonsterManaPoints.MaxValuePoints += (monster.MonsterAtributes.Sabedoria.CurrentValuePoints * 3.00) + 30.00;
            monster.MonsterManaPoints.CurrentOrMinValuePoints = monster.MonsterManaPoints.MaxValuePoints;
            
            monster.MagicAttack.MaxValuePoints += 1.50;
        }

        public void AddMagicAttackStatus(BaseMonstersEntity monster)
        {
            monster.MagicAttack.MaxValuePoints = (monster.MonsterAtributes.Inteligencia.CurrentValuePoints * 3.00) + 5.00;
            
            monster.MonsterManaPoints.MaxValuePoints += 8.00;
            monster.MonsterManaPoints.CurrentOrMinValuePoints = monster.MonsterManaPoints.MaxValuePoints;
        }

        public void AddPhysicalAttackStatus(BaseMonstersEntity monster)
        {
            monster.PhysicalAttack.MaxValuePoints = (monster.MonsterAtributes.Forca.CurrentValuePoints * 3.00) + 5.00;
        }

        public void AddLuckStatus(BaseMonstersEntity monster)
        {
            monster.Luck.MaxValuePoints = (monster.MonsterAtributes.Sorte.CurrentValuePoints * 3.00) + 10.00;
        }

        public void AddEvasionStatus(BaseMonstersEntity monster)
        {
            monster.Evasion.MaxValuePoints = (monster.MonsterAtributes.Agilidade.CurrentValuePoints * 3.00) + 10.00;
        }

        public void AddPersuationStatus(BaseMonstersEntity monster)
        {
            monster.Persuation.MaxValuePoints = (monster.MonsterAtributes.Carisma.CurrentValuePoints * 3.00) + 10.00;
        }
    }
}
