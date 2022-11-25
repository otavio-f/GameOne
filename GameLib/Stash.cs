namespace GameLib
{
    public abstract class Stash<T> where T:IStorable
    {
        private List<T> items;
        public double Capacity { get; }
        public List<T> Items => new List<T>(items);
        public double TotalWeight
        {
            get
            {
                var value = 0.0;
                foreach(var item in items)
                {
                    value += item.Weight;
                }
                return value;
            }
        }

        public bool ExceedsCapacity => TotalWeight > Capacity;

        public Stash(double capacity)
        {
            Capacity = capacity;
            items = new List<T>();
        }

        public void Add(T item)
        {
            var temp = new List<T>(items);
            items.Add(item);
            if (ExceedsCapacity)
            {
                this.items = temp; //revert
                throw new InvalidOperationException("Maximum capacity was exceeded while adding the last item.");
            }
        }

        public bool Remove(T item)
        {
            return items.Remove(item);
        }
    }
}