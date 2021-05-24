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
        public RaceEnum Race { get; set; }
        public ClassEnum MyClass { get; set; }

        public StatePointsEntity LifePoints { get; set; } = new StatePointsEntity();
        public StatePointsEntity ManaPoints { get; set; } = new StatePointsEntity();
        public StatePointsEntity KarmaPoints { get; set; } = new StatePointsEntity();

        public LocalizationEntity CurrentLocalization { get; set; } = new LocalizationEntity();

        public int PlayersKill { get; set; } = 0;
        public int MonsterKill { get; set; } = 0;
        public int Deaths { get; set; } = 0;
    }
}
