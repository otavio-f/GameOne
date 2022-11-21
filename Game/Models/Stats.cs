namespace Game.Models
{
    public class Stats
    {
        public int Stamina { get; private init; }
        public int DamageThreshold { get; private init; }
        public int Damage { get; set; } = 0;
        public int Fatigue { get; set; } = 0;
        /// <summary>
        /// Remaining stamina, difference between fatigue and original stamina values
        /// </summary>
        public int CurrentStamina => (Stamina - Fatigue);
        /// <summary>
        /// Remaining health points, difference between Damage and the Damage Threshold
        /// </summary>
        public int Health => (DamageThreshold - Damage);

        public Stats(int stamina, int damageThreshold)
        {
            Stamina = stamina;
            DamageThreshold = damageThreshold;
        }
    }
}