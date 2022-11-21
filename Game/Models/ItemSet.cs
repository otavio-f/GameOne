using Game.Base;
using System.ComponentModel;

namespace Game.Models
{
    /// <summary>
    /// Holds items, has a maximum weight capacity 
    /// </summary>
    public class ItemSet: FixedLengthStash<Item>
    {
        /// <summary>
        /// The sum of the attributes of all items stored
        /// </summary>
        public Attributes AttributesSum
        {
            get
            {
                var result = new Attributes()
                {
                    Strength = 0.0,
                    Sensitivity = 0.0,
                    Dexterity = 0.0,
                    Effort = 0.0,
                    RecoverFactor = 0.0,
                    HealFactor = 0.0,
                    TakeoverTendency = 0.0
                };

                foreach (var item in Values)
                    result += item.Attributes;

                return result;
            }
        }

        public ItemSet(double capacity): base(capacity)
        {

        }

    }
}
