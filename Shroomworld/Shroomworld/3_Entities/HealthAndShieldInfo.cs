using System;

namespace Shroomworld
{
    internal class HealthAndShieldInfo : HealthInfo
    {
        // ---------- Properties ----------
        public float PercentShield { get => (float)_shieldHealth / _maxShieldHealth; }

        // ---------- Fields ----------
        private byte _maxShieldHealth;
        private byte _shieldHealth;

        // ---------- Constructors ----------
        public HealthAndShieldInfo(string healthPlainText, string shieldPlainText, byte maxShieldHealth) : base(healthPlainText)
        {
            _shieldHealth = Convert.ToByte(shieldPlainText);
            _maxShieldHealth = maxShieldHealth;
        }
        private HealthAndShieldInfo(byte maxHealth, byte regenAmountPerSecond) : base(maxHealth, regenAmountPerSecond)
        {
            _shieldHealth = 0;
            _maxShieldHealth = 0;
        }

        // ---------- Methods ----------
        public static override HealthAndShieldInfo CreateNew()
        {
            return new HealthAndShieldInfo();
        }
        public override void DecreaseHealth(byte amount, out bool dead)
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
        public override void RegenerateHealth(byte fps)
        {
            byte healAmount = (byte)(_regenAmountPerSecond / fps);

            if (_health < _maxHealth) // entity needs healing
            {
                AddHealth(healAmount, out byte remainder);

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
        protected void AddHealth(byte healthToAdd, out byte remainder)
        {
            _health += healthToAdd;
            remainder = (byte)(_maxHealth - _health);
        }
        private void AddHealthToShield(byte healthToAdd)
        {
            _shieldHealth = (byte)Math.Clamp(_shieldHealth + healthToAdd, _shieldHealth, _maxShieldHealth);
        }
        public void SetMaxShieldHealth(byte maxShieldHealth)
        {
            _maxShieldHealth = maxShieldHealth;

            // can only activate the shield if the player is at max health
            if (_health == _maxHealth)
            {
                _shieldHealth = _maxShieldHealth;
            }
            else
            {
                _shieldHealth = 0;
            }
        }
        public void DisableShield()
        {
            _shieldHealth = 0;
            _maxShieldHealth = 0;
        }
        public override string ToString()
        {
            return FileFormatter.FormatAsPlainText(base.ToString(), _shieldHealth.ToString(), FileFormatter.SecondarySeparator);
        }
    }
}
