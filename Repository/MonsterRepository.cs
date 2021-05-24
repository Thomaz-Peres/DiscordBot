using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using System.Threading.Tasks;

namespace FirstBotDiscord.Repository
{
    public class MonsterRepository
    {
        private readonly DataContext _context;

        public MonsterRepository(DataContext context) =>
            _context = context;

        public async Task CreateMonster()
        {
            var monster = new BaseMonstersEntity();
        }
    }
}
