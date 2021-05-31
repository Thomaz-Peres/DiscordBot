using FirstBotDiscord.Database;

namespace FirstBotDiscord.Repository
{
    public class StatusRepository
    {
        private readonly DataContext _context;
        public StatusRepository(DataContext context) =>
            _context = context;
    }
}