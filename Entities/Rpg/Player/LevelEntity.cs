namespace FirstBotDiscord.Entities.Rpg.Player
{
    public class LevelEntity
    {
        public int Level { get; set; } = 1;
        public double CurrentExperience { get; set; } = 0.00;
        public double ExperienceForNextLevel { get; set; } = 100.00;
    }
}