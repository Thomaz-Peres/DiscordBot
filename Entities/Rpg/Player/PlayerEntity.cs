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
        public CharacterEntity MyCharacter { get; set; }

        public int PlayersKill { get; set; }
        public int MonsterKill { get; set; }
        public int Deaths { get; set; }
    }
}
