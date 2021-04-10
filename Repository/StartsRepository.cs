using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg;
using FirstBotDiscord.Entities.Rpg.Player;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace FirstBotDiscord.Repository
{
    public class StartsRepository
    {
        private readonly DataContext _context;
        public StartsRepository(DataContext context)
            => _context = context;

        public async Task<PlayerEntity> FindUser(DiscordUser user) =>
            await _context.CollectionPlayers.Find(x => x.PlayerId == user.Id).FirstOrDefaultAsync();
    }
}
