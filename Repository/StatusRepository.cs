using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.Player;
using MongoDB.Driver;

namespace FirstBotDiscord.Repository
{
    public class StatusRepository
    {
        private readonly DataContext _context;
        public StatusRepository(DataContext context) =>
            _context = context;

        public PlayerEntity AddLifeStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints > 0)
            {
                player.MyCharacter.LifePoints.CurrentValuePoints = player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints * 1.25;
                player.MyCharacter.LifePoints.MaxValuePoints = player.MyCharacter.LifePoints.CurrentValuePoints;

                
            }
            return player;
        }
    }
}