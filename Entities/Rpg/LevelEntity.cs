using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg
{
    public class LevelEntity
    {
        public int Level { get; set; } = 1;
        public int CurrentExperience { get; set; }
        public int NextLevelExperience { get; set; }
    }
}
