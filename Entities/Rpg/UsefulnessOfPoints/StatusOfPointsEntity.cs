namespace FirstBotDiscord.Entities.Rpg.UsefulnessOfPoints
{
    public class StatusOfPointsEntity
    {
        public StatusOfPointsEntity() { }
        public StatusOfPointsEntity(double currentValue, double maxValue) 
        {
            this.CurrentOrMinValuePoints = currentValue;
            this.MaxValuePoints = maxValue;
        }

        public double CurrentOrMinValuePoints { get; set; }
        public double MaxValuePoints { get; set; }
    }
}
