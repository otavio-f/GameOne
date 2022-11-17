using System.Diagnostics.Metrics;

namespace Game.Models
{
    public class ActiveGame
    {
        public Character Top => _top;
        public Character Bottom => _bottom;
        public int TurnCount { get; private set; } = 0;

        private Character _top;
        private Character _bottom;
        private Random _random = new Random();

        public double SwitchChance => Bottom.TurnOverChance(Top);

        public ActiveGame(Character p1, Character p2)
        {
            _top = p1;
            _bottom = p2;
        }

        public void Switch()
        {
            var temp = _top;
            _top = _bottom;
            _bottom = temp;
        }

        public void Round()
        {
            TurnCount++;
            if (!Top.CanUseHability)
                return;

            var hability = Top.Hability;
            hability.IncreaseCount();

            if (!hability.IsReady)
                return;

            TopAttacksBottom();
            hability.ResetCount();
        }

        /// <summary>
        /// Makes the top use it's hability against the bottom character
        /// </summary>
        private void TopAttacksBottom()
        {
            var damage = Top.HabilityDamage;
            Bottom.ReceiveDamage(damage);
            Top.ApplyHabilityRecoil();
            Top.ApplyHabilityCost();
        }

        /// <summary>
        /// Check if there's a winner and if the game ended
        /// </summary>
        /// <returns>true if the game ended, null if there is no winner or the character winner</returns>
        public (bool, Character?) CheckForWinner()
        {
            if (!Bottom.HasLost && !Top.HasLost) 
                return (false, null); //has not ended yet

            if (Bottom.HasLost && Top.HasLost)
                return (true, null);  //both lost at the same time

            var winner = Bottom.HasLost ? Top : Bottom; //one won, one lost
            return (true, winner);
        }
    }
}