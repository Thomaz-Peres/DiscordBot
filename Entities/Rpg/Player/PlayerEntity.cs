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
        public ulong PlayerId { get; set; }
        public string NamePlayer { get; set; }
        public DateTime DateCreatePlayer { get; set; } = DateTime.Now;
        public HashSet<CharacterEntity> MyCharacter { get; set; } = new HashSet<CharacterEntity>();

        public int PlayersKill { get; set; }
        public int MonsterKill { get; set; }
        public int Deaths { get; set; }
    }
}
