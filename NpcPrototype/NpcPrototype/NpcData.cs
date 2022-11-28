using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcPrototype
{
    internal class NpcData : EventArgs
    {
        public bool Friendly { get => _friendly; }
        public int Health { get => _health; }
        public int MaxHealth { get => _maxHealth; }
        public int RegenSpeed { get => _regenSpeed; }
        public int? AttackStrength { get => _attackStrength; }


        private bool _friendly;

        private int _health;
        private int _maxHealth;
        private int _regenSpeed;

        private int? _attackStrength;

        
        public bool IncreaseHealth()
        {
            if (_friendly)
            {
                _health = Math.Min(_health + _regenSpeed, _maxHealth);
            }
            return _friendly;
        }
        public bool DecreaseHealthBy(int amount)
        {
            if (_health <= amount)
            {
                _health = 0;
                return false;
            }
            else
            {
                _health -= amount;
                return true;
            }
        }
    }
}
