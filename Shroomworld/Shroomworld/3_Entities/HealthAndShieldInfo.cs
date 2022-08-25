using System;

namespace Shroomworld
{
    internal class HealthAndShieldInfo : HealthInfo
    {

        // ---------- Enums ----------


        // ---------- Properties ----------
        public float PercentShield { get => (float)_shieldHealth / _maxShieldHealth; }

        // ---------- Fields ----------
        private byte _maxShieldHealth;
        private byte _shieldHealth;

        // ---------- Constructors ----------


        // ---------- Methods ----------
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
        public override void RegenerateHealthNaturally(byte fps)
        {
            byte healAmount = (byte)(_regenAmountPerSecond / fps);

            if (_health < _maxHealth) // entity needs healing
            {
                Heal(healAmount, out byte remainder);

                if (remainder > 0)
                {
                    HealShield(remainder); // pass on the remainder to the shield
                }
            }
            else if(_shieldHealth < _maxShieldHealth) // shield needs healing
            {
                HealShield(healAmount);
            }
        }
        private void Heal(byte healthToAdd, out byte remainder)
        {
            _health += healthToAdd;
            remainder = (byte)(_maxHealth - _health);
        }
        private void HealShield(byte healthToAdd)
        {
            _shieldHealth = (byte)Math.Clamp(_shieldHealth + healthToAdd, _shieldHealth, _maxShieldHealth);
        }
        public void EnableShield(byte level, byte multiplier)
        {
            _maxShieldHealth = (byte)(level * multiplier);

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
    }
}
