using Game.Base;

namespace Game.Models
{
    public class Skill: IStorable
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int Power { get; set; }
        public int Cost { get; set; }
        public int Recoil { get; set; }
        public int Cooldown { get; set; }
        public double Weight { get; } = 1;
        
        private int _count = 0;
        public int Count => _count;
        public void IncreaseCount() => _count++;
        public void ResetCount() => _count = 0;

        public bool IsReady => _count >= Cooldown;
    }
}