namespace GameLib
{
    public class HabilityStash : Stash<Hability>
    {
        private int inuse = 0;
        public int InUseIndex
        {
            set
            {
                if (value >= Items.Count)
                    throw new ArgumentOutOfRangeException("Hability number is not valid.");
                inuse = value;
            }
        }

        public Hability InUse
        {
            get => Items[inuse];
            set 
            {
                var index = Items.IndexOf(value);
                if (index == -1)
                    throw new ArgumentOutOfRangeException("Hability is not in hability set.");
                inuse = index;
            }
        }
        public HabilityStash(double capacity) : base(capacity)
        {

        }
    }
}