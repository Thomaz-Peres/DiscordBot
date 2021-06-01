using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using FirstBotDiscord.Database;
using MongoDB.Driver;

namespace FirstBotDiscord.Repository
{
    public class StatusRepository
    {
        private readonly DataContext _context;
        public StatusRepository(DataContext context) =>
            _context = context;

        public async void AddLifeStatus(double lifeValue, CommandContext ctx)
        {
            var player = await _context.CollectionPlayers.Find(x => x.PlayerId == ctx.User.Id).FirstOrDefaultAsync();

            if(player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints > 0)
            {

            }
        }
    }
}