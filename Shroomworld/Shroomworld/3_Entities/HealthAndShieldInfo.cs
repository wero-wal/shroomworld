using System;

namespace Shroomworld
{
    internal class HealthAndShieldInfo : HealthInfo
    {
        // ----- Properties -----
        public float PercentShield { get => (float)_shieldHealth / _maxShieldHealth; }

        // ----- Fields -----
        private int _maxShieldHealth;
        private int _shieldHealth;

        // ----- Constructors -----
        /// summary: maxShieldHealth should be taken from the powerup
        public HealthAndShieldInfo(string healthPlainText, string shieldPlainText, int maxShieldHealth) : base(healthPlainText)
        {
            _shieldHealth = Convert.ToInt32(shieldPlainText);
            _maxShieldHealth = maxShieldHealth;
        }
        private HealthAndShieldInfo(int maxHealth, int regenAmountPerSecond) : base(maxHealth, regenAmountPerSecond)
        {
            _shieldHealth = 0;
            _maxShieldHealth = 0;
        }

        // ----- Methods -----
        // static
        public static override HealthAndShieldInfo CreateNew()
        {
            return new HealthAndShieldInfo();
        }
        
        // public
        public override void DecreaseHealth(int amount, out bool dead)
        {
            if (_shieldHealth > 0) // shield is active
            {
                _shieldHealth -= amount;
                if (_shieldHealth < 0) // there is more damage to be done
                {
                    _health += _shieldHealth; // do any remaining damage to the health
                    _shieldHealth = 0;
                }
            }
            else // shield is not active
            {
                _health -= amount;
            }
            dead = _health <= 0; // entity should be dead
        }
        public override void RegenerateHealth(int fps)
        {
            int healAmount = _regenAmountPerSecond / fps;

            if (_health < _maxHealth) // entity needs healing
            {
                AddHealth(healAmount, out int remainder);

                if (remainder > 0)
                {
                    AddHealthToShield(remainder); // pass on the remainder to the shield
                }
            }
            else if(_shieldHealth < _maxShieldHealth) // shield needs healing
            {
                AddHealthToShield(healAmount);
            }
        }
        public override string ToString()
        {
            return FileFormatter.FormatAsPlainText(base.ToString(), _shieldHealth, levelOfSeparator: FileFormatter.Secondary);
        }
        
        public void ChangeMaxShieldHealth(int newMaxShieldHealth)
        {
            _maxShieldHealth = newMaxShieldHealth;
            // no need to change _shieldHealth, as health will regenerate naturally to the max.
        }
        public void DisableShield()
        {
            _shieldHealth = 0;
            _maxShieldHealth = 0;
        }
        
        // private
        private void AddHealth(int healthToAdd, out int remainder)
        {
            _health += healthToAdd;
            remainder = _maxHealth - _health;
        }
        private void AddHealthToShield(int healthToAdd)
        {
            _shieldHealth = Math.Clamp(_shieldHealth + healthToAdd, _shieldHealth, _maxShieldHealth);
        }
    }
}
