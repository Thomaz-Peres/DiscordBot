namespace FirstBotDiscord.Entities.Rpg.Player
{
    public class LevelEntity
    {
        public int Level { get; set; } = 1;
        public double CurrentExperience { get; set; }
        public double ExperienceForNextLevel { get; set; }

        public void AddExperience(double xp)
        {
            this.CurrentExperience += xp;

            do
            {
                this.LevelUp();
            }
            while(this.CurrentExperience >= this.ExperienceForNextLevel);
        }

        public void LevelUp()
        {
            this.Level++;
            ExperienceNextLevel(this.Level);
        }

        public void ExperienceNextLevel(int level)
        {

        }
    }
}