using System;

namespace Shroomworld
{
    internal class HealthData
    {
        public int Health { get => _health; }

// todo: percentage health
        protected int _health;
        private ReadonlyHealthData _readonlyData;

        private DateTime _healed;


        public HealthData(int? health, ref ReadonlyHealthData readonlyHealthData)
        {
            _health = health ?? readonlyHealthData.MaxHealth;
            _readonlyData = readonlyHealthData;
        }

        /// <summary>
        /// Decreases the health by the desired amount.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>true if health &lt;= 0</returns>
        public bool DecreaseHealth(int amount)
        {
            _health -= amount;
            return _health <= 0; // true => dead
        }
        //public virtual void ResetHealth()
        //{
        //    _health = _maxHealth;
        //}
        public virtual void RegenerateHealth(int fps)
        {
            if((DateTime.Now - _healed).TotalSeconds >= 1)
            {
                AddHealth(_readonlyData.RegenAmountPerSecond);
                //or just divide by fps. idk.
                //or, change regen amount per second to regenCooldown
            }
        }
        public void AddHealth(int healthToAdd)
        {
            _health = Math.Min(_health + healthToAdd, _readonlyData.MaxHealth);
        }
        public override string ToString()
        {
            return _health.ToString();
        }
    }
}
