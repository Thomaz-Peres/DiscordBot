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
                player.MyCharacter.LifePoints.MaxValuePoints = player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints * 3.00;
            }
            return player;
        }

        public PlayerEntity AddManaStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints > 0)
            {
                player.MyCharacter.ManaPoints.MaxValuePoints = player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints * 3.00;
                player.MyCharacter.MagicAttack.MaxValuePoints = player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints * 1.00;
            }
            return player;
        }

        public PlayerEntity AddMagicAttackStatus(PlayerEntity player)
        {
            if (player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints > 0)
            {
                player.MyCharacter.MagicAttack.MaxValuePoints = player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints * 3.00;
                player.MyCharacter.ManaPoints.MaxValuePoints = player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints * 1.00;
            }
            return player;
        }

        public PlayerEntity AddPhysicalAttackStatus(PlayerEntity player)
        {
            if (player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints > 0)
            {
                player.MyCharacter.PhysicalAttack.MaxValuePoints = player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints * 3.00;
            }
            return player;
        }

        public PlayerEntity AddLuckStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints > 0)
            {
                player.MyCharacter.Luck.MaxValuePoints = player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints * 3.00;
            }
            return player;
        }

        public PlayerEntity AddEvasionStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints > 0)
            {
                player.MyCharacter.Evasion.MaxValuePoints = player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints * 3.00;
            }
            return player;
        }

        public PlayerEntity AddPersuationStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints > 0)
            {
                player.MyCharacter.Evasion.MaxValuePoints = player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints * 3.00;
            }
            return player;
        }
    }
}