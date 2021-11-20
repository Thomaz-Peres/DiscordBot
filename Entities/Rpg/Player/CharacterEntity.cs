using FirstBotDiscord.Entities.Rpg.UsefulnessOfPoints;

namespace FirstBotDiscord.Entities.Rpg.Player
{
    public class CharacterEntity
    {
        public InventoryEntity Inventory { get; set; } = new InventoryEntity();
        public MoneyEntity Money { get; set; } = new MoneyEntity();
        public AtributesEntity AtributesCharacter { get; set; } = new AtributesEntity();
        public string MyTitle { get; set; }
        public string Race { get; set; }
        public string MyClass { get; set; }

        public LevelEntity Experience { get; set; } = new LevelEntity();
        public StatusOfPointsEntity LifePoints { get; set; } = new StatusOfPointsEntity(30, 30);
        public StatusOfPointsEntity ManaPoints { get; set; } = new StatusOfPointsEntity(30, 30);
        public StatusOfPointsEntity KarmaPoints { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity PhysicalAttack { get; set; } = new StatusOfPointsEntity(5, 5);
        public StatusOfPointsEntity MagicAttack { get; set; } = new StatusOfPointsEntity(5, 5);
        public StatusOfPointsEntity Armor { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity MagicResistence { get; set; } = new StatusOfPointsEntity(0, 0);
        public StatusOfPointsEntity Persuation { get; set; } = new StatusOfPointsEntity(10, 10);
        public StatusOfPointsEntity Luck { get; set; } = new StatusOfPointsEntity(10, 10);
        public StatusOfPointsEntity Evasion { get; set; } = new StatusOfPointsEntity(10, 10);

        public LocalizationEntity CurrentLocalization { get; set; } = new LocalizationEntity();

        public int PlayersKill { get; set; } = 0;
        public int MonsterKill { get; set; } = 0;
        public int Deaths { get; set; } = 0;
    }
}
