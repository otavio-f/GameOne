namespace Game.Models
{
    /// <summary>
    /// The static capacity of the character
    /// </summary>
    public class Capacity
    {
        /// <summary>
        /// The maximum weight that the character is able to carry
        /// </summary>
        public double CarryCapacity { get; set; }
        /// <summary>
        /// The maximum number of skills that a character can have
        /// </summary>
        public int SkillCount { get; set; }
    }
}