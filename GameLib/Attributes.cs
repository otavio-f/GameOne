namespace GameLib
{
    public class Attributes
    {
        public double Strength { get; set; }
        public double Dexterity { get; set; }
        public double Sensitivity { get; set; }
        public double Effort { get; set; }
        public double StaminaRegen { get; set; }
        public double HealthRegen { get; set; }

        public static Attributes operator +(Attributes a, Attributes b)
        {
            var str = a.Strength + b.Strength;
            var dex = a.Dexterity + b.Dexterity;
            var ses = a.Sensitivity + b.Sensitivity;
            var eff = a.Effort + b.Effort;
            var srg = a.StaminaRegen + b.StaminaRegen;
            var hrg = a.HealthRegen + b.HealthRegen;

            return new Attributes()
            {
                Strength = str,
                Dexterity = dex,
                Sensitivity = ses,
                Effort = eff,
                StaminaRegen = srg,
                HealthRegen = hrg
            };
        }

        public static Attributes operator *(Attributes a, Attributes b)
        {
            var str = a.Strength * b.Strength;
            var dex = a.Dexterity * b.Dexterity;
            var ses = a.Sensitivity * b.Sensitivity;
            var eff = a.Effort * b.Effort;
            var srg = a.StaminaRegen * b.StaminaRegen;
            var hrg = a.HealthRegen * b.HealthRegen;

            return new Attributes()
            {
                Strength = str,
                Dexterity = dex,
                Sensitivity = ses,
                Effort = eff,
                StaminaRegen = srg,
                HealthRegen = hrg
            };
        }

        public static Attributes Neutral => new Attributes()
        {
            Strength = 1,
            Dexterity = 1,
            Sensitivity = 1,
            Effort = 1,
            StaminaRegen = 1,
            HealthRegen = 1
        };
        public static Attributes Zero => new Attributes()
        {
            Strength = 0,
            Dexterity = 0,
            Sensitivity = 0,
            Effort = 0,
            StaminaRegen = 0,
            HealthRegen = 0
        };
    }
}