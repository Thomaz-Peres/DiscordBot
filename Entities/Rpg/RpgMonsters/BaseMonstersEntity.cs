using FirstBotDiscord.Entities.Rpg.UsefulnessOfPoints;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FirstBotDiscord.Entities.Rpg.RpgMonsters
{
    public class BaseMonstersEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public int? MonsterId { get; set; }
        public string MonsterName { get; set; }
        public int Level { get; set; }
        public bool IsBoss { get; set; }

        public AtributesEntity MonsterAtributes { get; set; } = new AtributesEntity();

        public StatusOfPointsEntity MonsterLifePoints { get; set; } = new StatusOfPointsEntity(30, 30);
        public StatusOfPointsEntity MonsterManaPoints { get; set; } = new StatusOfPointsEntity(30, 30);
        public StatusOfPointsEntity PhysicalAttack { get; set; } = new StatusOfPointsEntity(5, 5);
        public StatusOfPointsEntity MagicAttack { get; set; } = new StatusOfPointsEntity(5, 5);
        public StatusOfPointsEntity Armor { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity MagicResistence { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity Persuation { get; set; } = new StatusOfPointsEntity(10, 10);
        public StatusOfPointsEntity Luck { get; set; } = new StatusOfPointsEntity(10, 10);
        public StatusOfPointsEntity Evasion { get; set; } = new StatusOfPointsEntity(10, 10);

        public DateTime Spawn { get; set; }
    }
}
