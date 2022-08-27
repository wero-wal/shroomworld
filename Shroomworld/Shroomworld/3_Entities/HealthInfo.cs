using System;

namespace Shroomworld
{
    internal class HealthInfo
    {
        // ---------- Properties ----------
        public float PercentHealth { get => (float)_health / maxHealth; }
        public byte MaxHealth { get => maxHealth; }
        public byte Health { get => _health; }

        // ----------Fields----------
        protected const char _separator = ' ';

        protected readonly byte maxHealth;
        protected readonly byte _regenAmountPerSecond;

        protected byte _health;

        // ---------- Constructors ----------
        public HealthInfo(string plainText)
        {
            string[] split = plainText.Split(_separator);
            byte i = 0;
            _health = Convert.ToByte(split[i++]);
            _maxHealth = Convert.ToByte(split[i++]);
            _regenAmountPerSecond = Convert.ToByte(split[i++]);
        }
        protected HealthInfo(byte maxHealth, byte regenAmountPerSecond)
        {
            _health = maxHealth;
            _maxHealth = maxHealth;
            _regenAmountPerSecond = regenAmountPerSecond;
        }

        // ---------- Methods ----------
        public static virtual HealthInfo CreateNew(byte maxHealth, byte regenAmountPerSecond)
        {
            return new HealthInfo(maxHealth, regenAmountPerSecond);
        }

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
        public virtual void RegenerateHealth(byte fps)
        {
            if (_health < _maxHealth)
            {
                byte healAmount = (byte)(_regenAmountPerSecond / fps);
                AddHealth(healAmount);
            }
        }
        public virtual string ToString()
        {
            return FileFormatter.FormatAsPlainText(_health, _maxHealth, _regenAmountPerSecond, FileFormatter.SecondarySeparator);
        }

        protected void AddHealth(byte healthToAdd)
        {
            _health += healthToAdd;
        }

    }
}
