using Game.Base;

namespace Game.Models
{
    /// <summary>
    /// The set of skills learned by the character.
    /// The amount of skills is limited
    /// </summary>
    public class Skillset: Stash<Skill>
    {
        private int inUseIndex = 0;
        /// <summary>
        /// The active skill
        /// </summary>
        public Skill InUse => Values[inUseIndex];

        public Skillset(int capacity) : base(capacity)
        {

        }

        /// <summary>
        /// Switches into another skill
        /// </summary>
        /// <param name="index">The index of the skill</param>
        /// <exception cref="ArgumentOutOfRangeException">If the index of the skill is not valid</exception>
        public void SwitchTo(int index)
        {
            if (index >= Values.Count || index < 0)
                throw new ArgumentOutOfRangeException();
            inUseIndex = index;
        }
    }
}
