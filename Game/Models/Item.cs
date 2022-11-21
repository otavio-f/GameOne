using Game.Base;

namespace Game.Models
{
    public class Item : IStorable
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double Weight { get; set; }
        public Attributes Attributes { get; set; }

    }
}