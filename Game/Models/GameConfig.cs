using System.Globalization;

namespace Game.Models
{
    public class GameConfig
    {
        public int DamageThreshold { get; set; }
        public int Stamina { get; set; }

        public Stats GenerateStats() => new Stats(Stamina, DamageThreshold);

    }
}