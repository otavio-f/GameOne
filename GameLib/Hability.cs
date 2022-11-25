namespace GameLib
{
    public class Hability : IStorable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
        public int Recoil { get; set; }
        public int Cost { get; set; }
        public int Speed { get; set; }
        public HabilityQuirks Quirks { get; set; }
        public double Weight => 1;
        
        private int _counter = 0;
        public bool IsReady => _counter > Speed;
        public void IncreaseCounter() => _counter++;
        public void ResetCounter() => _counter = 0;
    }
}