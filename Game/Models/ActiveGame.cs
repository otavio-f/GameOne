using System.Diagnostics.Metrics;

namespace Game.Models
{
    /// <summary>
    /// Game class.
    /// Stores the game state.
    /// </summary>
    public class ActiveGame
    {
        /// <summary>
        /// The character attacking
        /// </summary>
        public Character Attacker => _attacker;
        /// <summary>
        /// The defending character
        /// </summary>
        public Character Defender => _defender;
        /// <summary>
        /// How much turns have passed
        /// </summary>
        public int TurnCount { get; private set; } = 0;

        private Character _attacker;
        private Character _defender;
        private Random _random = new Random();
        /// <summary>
        /// The chance of the defender character switching position
        /// </summary>
        public double SwitchChance => Defender.TurnOverChance(Attacker);

        /// <summary>
        /// If anyone has lost, the game is over
        /// </summary>
        public bool HasEnded => (Defender.HasLost || Attacker.HasLost);
        /// <summary>
        /// The winner of this game
        /// </summary>
        public Character? Winner
        {
            get
            {
                if (Defender.HasLost && !Attacker.HasLost)
                    return Attacker;
                if (Attacker.HasLost && !Defender.HasLost)
                    return Defender;
                return null;
            }
        }

        public ActiveGame(Character p1, Character p2)
        {
            _attacker = p1;
            _defender = p2;
        }

        public void Switch()
        {
            var temp = _attacker;
            _attacker = _defender;
            _defender = temp;
        }

        /// <summary>
        /// Executes a game turn
        /// </summary>
        public void Turn()
        {
            TurnCount++;
            if (!Attacker.CanUseSkill)
                return;

            var skill = Attacker.Skills.InUse;
            skill.IncreaseCount();

            if (!skill.IsReady)
                return;

            AttackerAttacks();
            skill.ResetCount();
        }

        /// <summary>
        /// Makes the top use it's hability against the bottom character
        /// </summary>
        private void AttackerAttacks()
        {
            var damage = Attacker.SkillDamage;
            Defender.ReceiveDamage(damage);
            Attacker.ApplySkillRecoil();
            Attacker.ApplySkillCost();
        }
    }
}