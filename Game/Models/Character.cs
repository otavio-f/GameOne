using System.ComponentModel.DataAnnotations.Schema;

namespace Game.Models
{

    public class Character
    {
        public int Id { get; set; }
        public Text Name { get; set; }
        public Text Title { get; set; }
        [NotMapped]
        public Stats Stats { get; set; }
        public Attributes Attributes { get; set; }
        public Hability Hability { get; set; }
        public Item Item { get; set; }

        private bool _hasLost = false;
        public bool HasLost => _hasLost;

        List<Text> Quotes { get; set; }

        public Attributes AttributesAfterItemUse => Attributes + Item.Attributes;

        public bool CanUseHability
        {
            get
            {
                var itemCost = Hability.Cost * AttributesAfterItemUse.Effort;
                return Stats.CurrentStamina >= itemCost;
            }
        }
        /// <summary>
        /// The hability raw damage scaled by dexterity
        /// </summary>
        public double HabilityDamage => AttributesAfterItemUse.Dexterity * Hability.Power;

        /// <summary>
        /// Receives damage.
        /// The amount of received damage is scaled with the sensityvity
        /// Sets the lost flag if there is no health remaining after
        /// </summary>
        /// <param name="rawValue"></param>
        public void ReceiveDamage(double rawValue)
        {
            var damage = rawValue * AttributesAfterItemUse.Sensitivity;
            Stats.Damage += (int) Math.Round(damage);
            if (!_hasLost)
                if (Stats.Health <= 0)
                    _hasLost = true;
        }

        /// <summary>
        /// Applies the hability recoil into the accumulated damage
        /// </summary>
        public void ApplyHabilityRecoil()
        {
            ReceiveDamage(Hability.Recoil);
        }

        /// <summary>
        /// Scales the hability cost with the effort required and adds into the fatigue
        /// </summary>
        public void ApplyHabilityCost()
        {
            var cost = Hability.Cost * AttributesAfterItemUse.Effort;
            Stats.Fatigue += (int)Math.Round(cost);
        }

        /// <summary>
        /// Self-heals by reducing damage taken.
        /// Reduces damage until a minimum value of zero.
        /// </summary>
        public void Heal()
        {
            var heal = Stats.Config.DamageThreshold * AttributesAfterItemUse.HealFactor;
            Stats.Damage -= (int) Math.Round(heal);
            if (Stats.Damage < 0)
                Stats.Damage = 0;
        }
        /// <summary>
        /// Self-recover of stamina by reducing fatigue.
        /// Reduces fatigue until a minimum value of zero.
        /// </summary>
        public void Recover()
        {
            var recover = Stats.Config.Stamina * AttributesAfterItemUse.RecoverFactor;
            Stats.Fatigue -= (int) Math.Round(recover);
            if (Stats.Fatigue < 0)
                Stats.Fatigue = 0;
        }

        /// <summary>
        /// The takeover chance to an enemy
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns>the takeover chance, between 0 and infinity</returns>
        public double TurnOverChance(Character enemy)
        {
            const double antizero = 1.0 / 1_000_000_000;
            var strChance = (antizero + AttributesAfterItemUse.Strength) / (antizero + enemy.AttributesAfterItemUse.Strength);
            var staChance = (antizero + Stats.CurrentStamina) / (antizero + enemy.Stats.CurrentStamina);
            return strChance * staChance;
        }
    }
}
