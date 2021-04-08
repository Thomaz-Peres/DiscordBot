using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg
{
    public class InventoryEntity
    {
        public InventoryEntity() { }

        public int MinSlots { get; set; } = 10;
        public int MaxSlots { get; set; } = 32;
    }
}
