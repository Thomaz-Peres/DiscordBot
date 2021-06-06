using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg
{
    public class LocalizationEntity
    {
        public ulong ChannelId { get; set; }
        public string ChannelName { get; set; }
        public ulong GuildID { get; set; }

        public LocalizationEntity(ulong channelID, string channelName, ulong guildID)
        {
            this.ChannelId = channelID;
            this.ChannelName = channelName;
            this.GuildID = guildID;
        }
        public LocalizationEntity()
        {

        }
    }
}
