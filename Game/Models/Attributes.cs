using System.Data.Common;

namespace Game.Models
{
    public class Attributes
    {
        public double Strength { get; set; }
        public double Sensitivity { get; set; }
        public double Dexterity { get; set; }
        public double Effort { get; set; }
        public double RecoverFactor { get; set; }
        public double HealFactor { get; set; }
        public double TakeoverTendency { get; set; }

        /// <summary>
        /// Sums two sets of attributes
        /// </summary>
        /// <param name="a">First operand</param>
        /// <param name="b">Second operand</param>
        /// <returns>A new attribute set with the sum of opponents attributes.</returns>
        public static Attributes operator +(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                Strength = a.Strength + b.Strength,
                Sensitivity = a.Sensitivity + b.Sensitivity,
                Dexterity = a.Dexterity + b.Dexterity,
                Effort = a.Effort + b.Effort,
                RecoverFactor = a.RecoverFactor + b.RecoverFactor,
                HealFactor = a.HealFactor + b.HealFactor,
                TakeoverTendency = a.TakeoverTendency + b.TakeoverTendency
            };
        }

        /// <summary>
        /// Scales two sets of attributes
        /// </summary>
        /// <param name="a">The operand</param>
        /// <param name="b">The operand</param>
        /// <returns>A new attribute set with the product of two operands</returns>
        public static Attributes operator *(Attributes a, Attributes b)
        {
            return new Attributes()
            {
                Strength = a.Strength * b.Strength,
                Sensitivity = a.Sensitivity * b.Sensitivity,
                Dexterity = a.Dexterity * b.Dexterity,
                Effort = a.Effort * b.Effort,
                RecoverFactor = a.RecoverFactor * b.RecoverFactor,
                HealFactor = a.HealFactor * b.HealFactor,
                TakeoverTendency = a.TakeoverTendency * b.TakeoverTendency
            };
        }

    }
}