using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class ActiveGame
    {
        public int TurnCount { get; private set; } = 0;
        public int Hp { get; private set; }
        public int Stamina { get; private set; }
        public Character Attacker { get; private set; }
        public Character Defender { get; private set; }
        
        public ActiveGame(int hp, int stamina, Character attacker, Character defender)
        {
            Hp = hp;
            Stamina = stamina;
            Attacker = attacker;
            Defender = defender;
        }

        private void InitializeCharacters()
        {
            Attacker.Stats = new Stats(Hp, Stamina);
            Defender.Stats = new Stats(Hp, Stamina);
        }

        private void Switch()
        {
            var temp = Attacker;
            Attacker = Defender;
            Defender = temp;
        }

        public bool HasEnded => Attacker.HasLost || Defender.HasLost;

        public Character? Winner
        {
            get
            {
                if (Attacker.HasLost && !Defender.HasLost)
                    return Defender;
                if (Defender.HasLost && !Attacker.HasLost)
                    return Attacker;
                return null;
            }
        }

        public void ExecuteTurn()
        {
            TurnCount++;
            if (Attacker.CanUseHability)
            {
                var damage = Attacker.HabilityDamage(Defender);
                Defender.Stats.Damage += damage;

                var recoil = Attacker.HabilityRecoil;
                Attacker.Stats.Damage += damage;

                var cost = Attacker.HabilityCost;
                Attacker.Stats.Fatigue += cost;

                Attacker.Habilities.InUse.ResetCounter();
            }
            else
            {
                var health = Defender.HealthRegen;
                Defender.Stats.Damage -= health;

                var stamina = Defender.StaminaRegen;
                Defender.Stats.Fatigue -= stamina;

                var hability = Attacker.Habilities.InUse;
                hability.IncreaseCounter();
            }
        }

    }
}
