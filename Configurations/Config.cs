using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace FirstBotDiscord.Configurations
{
    public class Parameters
    {
        public static readonly string token = "NzI0OTc1Njg3MjY5NjEzNjYw.XvIAOQ.bb39hGkSjtenu83zcF2Ys1Amvq0";  // Token your discord bot
        public static readonly string CredentialCode = "6A49D09F30438E21A34310965C7B97A1B783980134C0379FD75A9ED849848B2E128B7586DF058E6F483BBCAD7AFF20C6FD959B5C63F90DC0A1FB221DED95CD74"; // Token from B3 api 
        public static readonly string[] Prefix = { ";" };
        public static DiscordClient discord;   // using to interact with discord API.
    }
}
