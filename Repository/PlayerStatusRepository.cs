using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.Player;
using MongoDB.Driver;

namespace FirstBotDiscord.Repository
{
    public class PlayerStatusRepository
    {
        public PlayerStatusRepository() { }

        public void AddLifeStatus(PlayerEntity player)
        {
            if (player.MyCharacter.LifePoints.CurrentOrMinValuePoints == player.MyCharacter.LifePoints.MaxValuePoints)
            {
                player.MyCharacter.LifePoints.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints * 3.00) + 30.00;
                player.MyCharacter.LifePoints.CurrentOrMinValuePoints = player.MyCharacter.LifePoints.MaxValuePoints;
                return;
            }

            if ((player.MyCharacter.LifePoints.CurrentOrMinValuePoints < player.MyCharacter.LifePoints.MaxValuePoints && player.MyCharacter.LifePoints.CurrentOrMinValuePoints > 0.00) || (player.MyCharacter.LifePoints.CurrentOrMinValuePoints < 0.00))
            {
                player.MyCharacter.LifePoints.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints * 3.00) + 30.00;
                player.MyCharacter.LifePoints.CurrentOrMinValuePoints += 1.00;
                return;
            }
        }

        public void AddManaStatus(PlayerEntity player)
        {
            player.MyCharacter.MagicAttack.MaxValuePoints += 1.50;


            if (player.MyCharacter.ManaPoints.CurrentOrMinValuePoints == player.MyCharacter.ManaPoints.MaxValuePoints)
            {
                player.MyCharacter.ManaPoints.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints * 3.00) + 30.00;
                player.MyCharacter.ManaPoints.CurrentOrMinValuePoints = player.MyCharacter.ManaPoints.MaxValuePoints;
                return;
            }

            if (player.MyCharacter.ManaPoints.CurrentOrMinValuePoints < player.MyCharacter.ManaPoints.MaxValuePoints && player.MyCharacter.ManaPoints.CurrentOrMinValuePoints > 0.00)
            {
                player.MyCharacter.ManaPoints.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints * 3.00) + 30.00;
                player.MyCharacter.ManaPoints.CurrentOrMinValuePoints += 1.00;
                return;
            }
        }

        //   This stats do not increase the minimum
        public void AddMagicAttackStatus(PlayerEntity player)
        {
            player.MyCharacter.MagicAttack.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints * 3.00) + 5.00;
            player.MyCharacter.ManaPoints.MaxValuePoints += + 8.00;
        }

        //   This stats do not increase the minimum
        public void AddPhysicalAttackStatus(PlayerEntity player)
        {
            player.MyCharacter.PhysicalAttack.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints * 3.00) + 5.00;
        }

        //   This stats do not increase the minimum
        public void AddLuckStatus(PlayerEntity player)
        {
            player.MyCharacter.Luck.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints * 3.00) + 10.00;
        }

        //   This stats do not increase the minimum
        public void AddEvasionStatus(PlayerEntity player)
        {
            player.MyCharacter.Evasion.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints * 3.00) + 10.00;
        }

        //   This stats do not increase the minimum
        public void AddPersuationStatus(PlayerEntity player)
        {
            player.MyCharacter.Persuation.MaxValuePoints = (player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints * 3.00) + 10.00;
                     
        }
    }
}