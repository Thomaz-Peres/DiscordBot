using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.Player;
using MongoDB.Driver;

namespace FirstBotDiscord.Repository
{
    public class StatusRepository
    {
        public StatusRepository() { }

        public PlayerEntity AddLifeStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints > 0)
            {
                player.MyCharacter.LifePoints.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints * 3.00) + 30.00;

                if (player.MyCharacter.LifePoints.CurrentOrMinValuePoints == player.MyCharacter.LifePoints.MaxValuePoints)
                    player.MyCharacter.LifePoints.CurrentOrMinValuePoints = player.MyCharacter.LifePoints.MaxValuePoints;

                else if (player.MyCharacter.LifePoints.CurrentOrMinValuePoints < player.MyCharacter.LifePoints.MaxValuePoints && player.MyCharacter.LifePoints.CurrentOrMinValuePoints > 0.00)
                    player.MyCharacter.LifePoints.CurrentOrMinValuePoints += 1.00;
            }
            return player;
        }

        public PlayerEntity AddManaStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints > 0)
            {
                player.MyCharacter.ManaPoints.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints * 3.00) + 30.00;
                player.MyCharacter.MagicAttack.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints * 1.00) + 1.50;


                if(player.MyCharacter.ManaPoints.CurrentOrMinValuePoints == player.MyCharacter.ManaPoints.MaxValuePoints)
                    player.MyCharacter.ManaPoints.CurrentOrMinValuePoints = player.MyCharacter.ManaPoints.MaxValuePoints;

                else if (player.MyCharacter.ManaPoints.CurrentOrMinValuePoints < player.MyCharacter.ManaPoints.MaxValuePoints && player.MyCharacter.ManaPoints.CurrentOrMinValuePoints > 0.00)
                    player.MyCharacter.ManaPoints.CurrentOrMinValuePoints += 1.00;
            }
            return player;
        }

        public PlayerEntity AddMagicAttackStatus(PlayerEntity player)
        {
            if (player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints > 0)
            {
                player.MyCharacter.MagicAttack.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints * 3.00) + 5.00;
                player.MyCharacter.ManaPoints.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints * 1.00) + 8.00;
            }
            return player;
        }

        public PlayerEntity AddPhysicalAttackStatus(PlayerEntity player)
        {
            if (player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints > 0)
            {
                player.MyCharacter.PhysicalAttack.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints * 3.00) + 5.00;
            }
            return player;
        }

        public PlayerEntity AddLuckStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints > 0)
            {
                player.MyCharacter.Luck.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints * 3.00) + 10.00;
            }
            return player;
        }

        public PlayerEntity AddEvasionStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints > 0)
            {
                player.MyCharacter.Evasion.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints * 3.00) + 10.00;
            }
            return player;
        }

        public PlayerEntity AddPersuationStatus(PlayerEntity player)
        {
            if(player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints > 0)
            {
                player.MyCharacter.Persuation.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints * 3.00) + 10.00;
            }
            return player;
        }
    }
}