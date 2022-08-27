using FirstBotDiscord.Entities.Rpg.Player;

namespace FirstBotDiscord.Extensions
{
    public static class PlayersExtensions
    {
        public static double GetCurrentFreePoints(this PlayerEntity playerEntity)
        {
            return playerEntity.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints;
        }
    }
}