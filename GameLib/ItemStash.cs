namespace GameLib
{
    public class ItemStash : Stash<Item>
    {
        public Attributes Sum
        {
            get
            {
                var result = Attributes.Zero;
                foreach(var item in Items)
                {
                    result += item.Attributes;
                }
                return result;
            }
        }

        public Attributes Product
        {
            get
            {
                var result = Attributes.Neutral;
                foreach(var item in Items)
                {
                    result *= item.Attributes;
                }
                return result;
            }
        }

        public ItemStash(double capacity) : base(capacity)
        {
        }
    }
}