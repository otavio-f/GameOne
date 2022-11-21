namespace Game.Models
{

    public class Character
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Attributes Attributes { get; set; }
        private Capacity _capacity;
        public Capacity Capacity
        {
            get => _capacity;
            set
            {
                _capacity = value;
                Skills = new Skillset(value.SkillCount);
                Items = new ItemSet(value.CarryCapacity);
            }
        }

        public Stats Stats { get; set; }
        public Skillset Skills { get; private set; }
        public ItemSet Items { get; private set; }

        public bool HasLost { get; private set;} = false;
        /// <summary>
        /// The character attributes boosted by the attributes of the item set
        /// </summary>
        public Attributes BoostedAttributes => Attributes + Items.AttributesSum;

        public bool CanUseSkill
        {
            get
            {
                var itemCost = Skills.InUse.Cost * BoostedAttributes.Effort;
                return Stats.CurrentStamina >= itemCost;
            }
        }

        /// <summary>
        /// The skill raw damage scaled by dexterity
        /// </summary>
        public double SkillDamage => BoostedAttributes.Dexterity * Skills.InUse.Power;

        /// <summary>
        /// Receives damage.
        /// The amount of received damage is scaled with the sensitivity
        /// Sets the lost flag if there is no health remaining
        /// </summary>
        /// <param name="rawValue"></param>
        public void ReceiveDamage(double rawValue)
        {
            var damage = rawValue * BoostedAttributes.Sensitivity;
            Stats.Damage += (int) Math.Round(damage);
            if (!HasLost)
                if (Stats.Health <= 0)
                    HasLost = true;
        }

        /// <summary>
        /// Applies the skill recoil
        /// </summary>
        public void ApplySkillRecoil()
        {
            ReceiveDamage(Skills.InUse.Recoil);
        }

        /// <summary>
        /// Applies the skill cost relative to the effort required
        /// </summary>
        public void ApplySkillCost()
        {
            var cost = Skills.InUse.Cost * BoostedAttributes.Effort;
            Stats.Fatigue += (int)Math.Round(cost);
        }

        /// <summary>
        /// Self-heals by reducing damage taken.
        /// Reduces damage until a minimum value of zero.
        /// </summary>
        public void Heal()
        {
            var heal = Stats.DamageThreshold * BoostedAttributes.HealFactor;
            Stats.Damage -= (int) Math.Round(heal);
            if (Stats.Damage < 0)
                Stats.Damage = 0;
        }
        /// <summary>
        /// Self-recovers stamina by reducing fatigue.
        /// Reduces fatigue until a minimum value of zero.
        /// </summary>
        public void Recover()
        {
            var recover = Stats.Stamina * BoostedAttributes.RecoverFactor;
            Stats.Fatigue -= (int) Math.Round(recover);
            if (Stats.Fatigue < 0)
                Stats.Fatigue = 0;
        }

        /// <summary>
        /// The takeover chance relative to an enemy
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns>the takeover chance, between 0 and infinity</returns>
        public double TurnOverChance(Character enemy)
        {
            const double antizero = 1.0 / 1_000_000_000;
            var strChance = (antizero + BoostedAttributes.Strength) / (antizero + enemy.BoostedAttributes.Strength);
            var staChance = (antizero + Stats.CurrentStamina) / (antizero + enemy.Stats.CurrentStamina);
            return strChance * staChance;
        }
    }
}
