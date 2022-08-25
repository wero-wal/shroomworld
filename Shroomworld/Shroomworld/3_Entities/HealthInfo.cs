using System;

namespace Shroomworld
{
    internal class HealthInfo
    {
        // ---------- Enums ----------


        // ---------- Properties ----------
        public float PercentHealth { get => (float)_health / _maxHealth; }
        public byte MaxHealth { get => _maxHealth; }
        public byte Health { get => _health; }

        // ----------Fields----------
        protected readonly byte _maxHealth;
        protected readonly byte _regenAmountPerSecond;

        protected byte _health;

        // ---------- Constructors ----------


        // ---------- Methods ----------
        /// <summary>
        /// Decreases the health by the desired amount.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>true if health &lt;= 0</returns>
        public virtual void DecreaseHealth(byte amount, out bool dead)
        {
            _health -= amount;
            dead = _health <= 0;
        }
        public virtual void ResetHealth()
        {
            _health = _maxHealth;
        }
        public virtual void RegenerateHealthNaturally(byte fps)
        {
            if (_health < _maxHealth)
            {
                byte healAmount = (byte)(_regenAmountPerSecond / fps);
                _health = (byte)Math.Clamp(_health + healAmount, _health, _maxHealth);
            }
        }
    }
}
