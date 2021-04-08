using FirstBotDiscord.Entities.Rpg.Class;
using System;
using System.ComponentModel.DataAnnotations;

namespace FirstBotDiscord.Entities.Rpg.Player
{
    public class CharacterEntity
    {
        public InventoryEntity Inventory { get; set; } = new InventoryEntity();
        public MoneyEntity Money { get; set; }
        public AtributesEntity AtributesCharacter { get; set; } = new AtributesEntity();
        public RaceEntity Race { get; set; } = new RaceEntity();
        public ClassEntity MyClass { get; set; } = new ClassEntity();

        public StatePointsEntity LifePoints { get; set; } = new StatePointsEntity();
        public StatePointsEntity ManaPoints { get; set; } = new StatePointsEntity();
        public StatePointsEntity KarmaPoints { get; set; } = new StatePointsEntity();
    }
}
