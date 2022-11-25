using System.Collections.Immutable;

namespace Game.Base
{
    public abstract class Stash<T> where T:IStorable
    {
        private double capacity;
        private List<T> listing { get; set; } = new List<T>();
        public ImmutableList<T> Values => ImmutableList.CreateRange(listing);

        public double TotalCapacity => listing.Sum(k => k.Weight);

        public Stash(double capacity)
        {
            this.capacity = capacity;
        }

        public void Add(T item)
        {
            if (TotalCapacity + item.Weight > capacity)
                throw new InvalidOperationException("The capacity is going to be exceeded.");
            listing.Add(item);
        }

        public bool Remove(T item) => listing.Remove(item);
    }
}
