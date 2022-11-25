using System;

namespace GameLib
{
    public class Character
    {
        /// <summary>
        /// Used to prevent division by zero
        /// </summary>
        private const double antizero = 1.0/1_000_000;
        public string Name { get; set; }
        public string Description { get; set; }
        public Stats Stats { get; set; }
        public Attributes OriginalAttributes { get; set; }
        public ItemStash Items { get; set; }
        public HabilityStash Habilities { get; set; }

        public double RequiredHabilityStamina => OriginalAttributes.Effort * Habilities.InUse.Cost;
        public Attributes Attributes => Items.Sum + OriginalAttributes;
        public bool HasLost => !(Stats.Damage.Valid);
        public bool CanUseHability
        {
            get
            {
                if (Habilities.InUse.IsReady)
                {
                    if (RequiredHabilityStamina <= Stats.Fatigue.Remaining)
                        return true;
                }
                return false;
            }
        }

        public double SwitchChance(Character opponent)
        {
            var strength = (OriginalAttributes.Strength + antizero) / (opponent.OriginalAttributes.Strength + antizero);
            var stamina = (Stats.Fatigue.Remaining + antizero) / (opponent.Stats.Fatigue.Remaining + antizero);
            return strength * stamina;
        }

        public double HealthRegen => Attributes.HealthRegen * Stats.Damage.Max;

        public double StaminaRegen => Attributes.StaminaRegen* Stats.Fatigue.Max;

        public double HabilityCost
        {
            get
            {
                var result = Habilities.InUse.Cost * Attributes.Effort;
                return result;
            }
        }
        public double HabilityRecoil
        {
            get
            {
                var result = Habilities.InUse.Recoil * Attributes.Sensitivity;
                return result;
            }
        }

        public double HabilityDamage (Character opponent)
        {
            double damage = Habilities.InUse.Damage;
            var quirks = Habilities.InUse.Quirks;
            if (!quirks.HasFlag(HabilityQuirks.IGNORES_DEXTERITY))
                damage *= Attributes.Dexterity;
            if (!quirks.HasFlag(HabilityQuirks.IGNORES_SENSITIVITY))
                damage *= opponent.Attributes.Sensitivity;
            return damage;
        }
    }
}