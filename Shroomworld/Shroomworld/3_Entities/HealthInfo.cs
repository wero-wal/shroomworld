using System;

namespace Shroomworld
{
    internal class HealthInfo
    {
        // ----- Properties -----
        public float PercentHealth { get => (float)_health / _maxHealth; }
        public int MaxHealth { get => _maxHealth; }
        public int Health { get => _health; }

        // -----Fields-----
        protected readonly int _maxHealth;
        protected readonly int _regenAmountPerSecond;

        protected int _health;

        // ----- Constructors -----
        public HealthInfo(string plainText)
        {
            string[] parts = plainText.Split(FileFormatter.Secondary);
            int i = 0;
            _health = Convert.ToInt32(parts[i++]);
            _maxHealth = Convert.ToInt32(parts[i++]);
            _regenAmountPerSecond = Convert.ToInt32(parts[i++]);
        }
        protected HealthInfo(int maxHealth, int regenAmountPerSecond)
        {
            _health = maxHealth;
            _maxHealth = maxHealth;
            _regenAmountPerSecond = regenAmountPerSecond;
        }

        // ----- Methods -----
        public static virtual HealthInfo CreateNew(int maxHealth, int regenAmountPerSecond)
        {
            return new HealthInfo(maxHealth, regenAmountPerSecond);
        }

        /// <summary>
        /// Decreases the health by the desired amount.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>true if health &lt;= 0</returns>
        public virtual void DecreaseHealth(int amount, out bool dead)
        {
            _health -= amount;
            dead = _health <= 0;
        }
        public virtual void ResetHealth()
        {
            _health = _maxHealth;
        }
        public virtual void RegenerateHealth(int fps)
        {
            if (_health < _maxHealth)
            {
                int healAmount = _regenAmountPerSecond / fps;
                AddHealth(healAmount);
            }
        }
        public virtual string ToString()
        {
            return FileFormatter.FormatAsPlainText(_health, _maxHealth, _regenAmountPerSecond, levelOfSeparator: FileFormatter.Secondary);
        }

        protected void AddHealth(int healthToAdd)
        {
            _health += healthToAdd;
        }
    }
}
