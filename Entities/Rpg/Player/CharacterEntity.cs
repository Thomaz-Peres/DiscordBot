using FirstBotDiscord.Entities.Rpg.Class;
using FirstBotDiscord.Entities.Rpg.UsefulnessOfPoints;

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

        public LevelEntity Experience { get; set; }
        public StatusOfPointsEntity LifePoints { get; set; }
        public StatusOfPointsEntity ManaPoints { get; set; }
        public StatusOfPointsEntity KarmaPoints { get; set; }
        public double PhysicalAttack { get; set; }
        public double MagicAttack { get; set; }
        public double Armor { get; set; }
        public double MagicResistence { get; set; }
        public double Persuation { get; set; }
        public double Luck { get; set; }

        public LocalizationEntity CurrentLocalization { get; set; } = new LocalizationEntity();

        public int PlayersKill { get; set; } = 0;
        public int MonsterKill { get; set; } = 0;
        public int Deaths { get; set; } = 0;
    }
}
