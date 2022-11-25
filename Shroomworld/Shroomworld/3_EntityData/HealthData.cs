using System;

namespace Shroomworld
{
    internal class HealthData
    {
        public int Health { get => _health; }


        protected int _health;


        ///// <summary>
        ///// Decreases the health by the desired amount.
        ///// </summary>
        ///// <param name="amount"></param>
        ///// <returns>true if health &lt;= 0</returns>
        //public virtual void DecreaseHealth(int amount, out bool dead)
        //{
        //    _health -= amount;
        //    dead = _health <= 0;
        //}
        //public virtual void ResetHealth()
        //{
        //    _health = _maxHealth;
        //}
        //public virtual void RegenerateHealth(int fps)
        //{
        //    if (_health < _maxHealth)
        //    {
        //        int healAmount = _regenAmountPerSecond / fps;
        //        AddHealth(healAmount);
        //    }
        //}
        //public void AddHealth(int healthToAdd)
        //{
        //    _health += healthToAdd;
        //}
        //public virtual string ToString()
        //{
        //    return FileFormatter.FormatAsPlainText(_health, _maxHealth, _regenAmountPerSecond, levelOfSeparator: FileFormatter.Secondary);
        //}
    }
}
