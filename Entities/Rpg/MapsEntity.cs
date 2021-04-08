using FirstBotDiscord.Enums;

namespace FirstBotDiscord.Entities.Rpg
{
    public class MapsEntity
    {
        public ulong MapId { get; set; }
        public string MapName { get; set; }
        public MapsType MapsType { get; set; }
        public int MonstersQuantity { get; set; } = 0;
    }
}
