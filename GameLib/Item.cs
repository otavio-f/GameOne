namespace GameLib
{
    public class Item : IStorable
    {
        public String Name { get; }
        public String Description { get; }
        public Attributes Attributes { get; }
        public double Weight { get; }

        public Item(string name, string description, Attributes attributes, double weight)
        {
            Name = name;
            Description = description;
            Attributes = attributes;
            Weight = weight;
        }
    }
}