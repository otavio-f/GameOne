namespace Game.Models
{
    public class Stats
    {
        public GameConfig Config { get; init; }
        public int Damage { get; set; } = 0;
        public int Fatigue { get; set; } = 0;
        /// <summary>
        /// Remaining stamina, difference between fatigue and original stamina values
        /// </summary>
        public int CurrentStamina => (Config.Stamina - Fatigue);
        /// <summary>
        /// Remaining health points, difference between Damage and the Damage Threshold
        /// </summary>
        public int Health => (Config.DamageThreshold - Damage);

        public Stats(GameConfig config)
        {
            Config = config;
        }
    }
}