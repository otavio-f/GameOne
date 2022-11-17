namespace Game.Models
{
    public class Hability
    {
        public int Id { get; set; }
        public Text Name { get; set; }
        public Text Description { get; set; }
        public int Power { get; set; }
        public int Cost { get; set; }
        public int Recoil { get; set; }
        public int Cooldown { get; set; }
        
        private int _count = 0;
        public int Count => _count;
        public void IncreaseCount() => _count++;
        public void ResetCount() => _count = 0;

        public bool IsReady => _count >= Cooldown;
    }
}