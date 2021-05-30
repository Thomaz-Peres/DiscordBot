using FirstBotDiscord.Entities.Rpg.Class;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstBotDiscord.Entities.Rpg.Player
{
    public class CharacterEntity
    {
        public InventoryEntity Inventory { get; set; } = new InventoryEntity();
        public MoneyEntity Money { get; set; } = new MoneyEntity();
        public AtributesEntity AtributesCharacter { get; set; } = new AtributesEntity();
        public string MyTitle { get; set; }
        public RaceEnum Race { get; set; }
        public ClassEnum MyClass { get; set; }

        public StatePointsEntity Experience { get; set; }
        public StatePointsEntity LifePoints { get; set; }
        public StatePointsEntity ManaPoints { get; set; }
        public StatePointsEntity KarmaPoints { get; set; }
        public float PhysicalAttack { get; set; }
        public float MagicAttack { get; set; }
        public float Armor { get; set; }
        public float MagicResistence { get; set; }
        public float Persuation { get; set; }
        public float Luck { get; set; }

        public LocalizationEntity CurrentLocalization { get; set; } = new LocalizationEntity();

        public int PlayersKill { get; set; } = 0;
        public int MonsterKill { get; set; } = 0;
        public int Deaths { get; set; } = 0;
    }
}
