using FirstBotDiscord.Entities.Rpg.UsefulnessOfPoints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg.RpgMonsters
{
    public class BaseMonstersEntity
    {
        public int MonsterId { get; set; }
        public string MonsterName { get; set; }
        public int Level { get; set; }
        public AtributesEntity MonsterAtributes { get; set; } = new AtributesEntity();

        public StatusOfPointsEntity MonsterLifePoints { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity MonsterManaPoints { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity PhysicalAttack { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity MagicAttack { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity Armor { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity MagicResistence { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity Persuation { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity Luck { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity Evasion { get; set; } = new StatusOfPointsEntity(0, 0);
        
        public DateTime Spawn { get; set; }
    }
}
