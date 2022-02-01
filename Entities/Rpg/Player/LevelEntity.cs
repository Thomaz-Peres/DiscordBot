using System;

namespace FirstBotDiscord.Entities.Rpg.Player
{
    public class LevelEntity
    {
        public int Level { get; set; } = 1;
        public double CurrentExperience { get; set; }
        public double ExperienceForNextLevel { get; set; } = 50;

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
            ExperienceNextLevel(this.Level + 1);
        }

        public void ExperienceNextLevel(int level)
        {
            this.ExperienceForNextLevel = Math.Pow(Math.Round(level / 1.6, 0) / 0.05, 2);
        }
    }
}