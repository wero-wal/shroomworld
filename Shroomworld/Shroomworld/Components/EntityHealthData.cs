using System;
namespace Shroomworld {
    /// <summary>Stores data about and regenerates an entity's health.</summary>
    public class EntityHealthData {

        // ----- Properties -----
        public int Health { get => _currentHealth; }
        public int PercentageHealth => (int)(100 * _currentHealth / _typeHealthData.MaxHealth);


        // ----- Fields -----
        private readonly HealthData _typeHealthData;
        private int _currentHealth;
        private DateTime _lastHealed;


        // ----- Constructors -----
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeHealthData">The generic health data for this type of entity.</param>
        /// <param name="currentHealth">The entity's current health value.
        /// If it's for a new entity, this can be set to null (or left
        /// blank) and the current health will be set to max health.</param>
        public EntityHealthData(HealthData typeHealthData, int? currentHealth = null) {
            _currentHealth = currentHealth ?? typeHealthData.MaxHealth;
            _typeHealthData = typeHealthData;
        }


        // ----- Methods -----
        public override string ToString() {
            return _currentHealth.ToString();
        }
        /// <summary>
        /// Increase health by <paramref name="healthToAdd"/>, capping it at <see cref="ReadonlyHealthData.MaxHealth"/>.
        /// </summary>
        /// <param name="healthToAdd">How much to increase health by.</param>
        public void IncreaseHealth(int healthToAdd) {
            _currentHealth = Math.Min(_currentHealth + healthToAdd, _typeHealthData.MaxHealth);
        }
        /// <summary>
        /// Decrease health by <paramref name="amount"/>.
        /// </summary>
        /// <param name="amount">How much to decrease health by.</param>
        /// <returns><see langword="true"/> if <c>health &lt;= 0</c> (dead)</returns>
        public bool DecreaseHealth(int amount) {
            _currentHealth -= amount;
            return _currentHealth <= 0;
        }
        /// <summary>
        /// Adds a small amount of health (<see cref="ReadonlyHealthData.RegenerationPerSecond"/>) every second.
        /// </summary>
        public void RegenerateHealth() {
            if((DateTime.Now - _lastHealed).TotalSeconds >= 1) {
                IncreaseHealth(_typeHealthData.RegenerationPerSecond);
            }
        }
    }
}
