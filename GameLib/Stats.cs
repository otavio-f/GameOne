namespace GameLib
{
    public class Stats
    {
        public Bar Damage { get; set; }
        public Bar Fatigue { get; set; }

        public Stats(int damageThreshold, int fatigueThreshold)
        {
            Damage = Bar.Of(damageThreshold);
            Fatigue = Bar.Of(fatigueThreshold);
        }
    }
}