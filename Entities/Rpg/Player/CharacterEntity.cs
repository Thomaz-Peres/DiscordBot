using FirstBotDiscord.Entities.Rpg.Class;
using System;

namespace FirstBotDiscord.Entities.Rpg.Player
{
    public class CharacterEntity
    {
        public int CharacterId { get; set; }
        public string NameCharacter { get; set; }
        public DateTime DateCreateCharacter { get; set; }


        public InventoryEntity Inventory { get; set; } = new InventoryEntity();
        public MoneyEntity Money { get; set; }
        public AtributesEntity Atributes { get; set; } = new AtributesEntity();
        public RaceEntity Race { get; set; } = new RaceEntity();
        public ClassEntity MyClass { get; set; } = new ClassEntity();


        public StatePointsEntity LifePoints { get; set; } = new StatePointsEntity();
        public StatePointsEntity KarmaPoints { get; set; } = new StatePointsEntity();
    }
}
