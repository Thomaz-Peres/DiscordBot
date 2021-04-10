using FirstBotDiscord.Entities.Rpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg.Player
{
    public class PlayerEntity
    {
        public PlayerEntity() { }

        public ulong PlayerId { get; set; }
        public string NamePlayer { get; set; }
        public DateTime DateCreatePlayer { get; set; } = DateTime.Now;
        public CharacterEntity MyCharacter { get; set; } = new CharacterEntity();
        public bool PlayDice { get; set; } = false;
    }
}
