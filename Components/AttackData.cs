namespace Shroomworld {
    /// <summary>
    /// Contains information about an entity type's attack.
    /// </summary>
    public class AttackData {

        // ----- Properties -----
        /// <value>The number of health points of damage the entity will deal upon attacking another entity.</value>
		public int Strength => _strength;
        /// <value>The maximum distance (in tiles) from which the entity can attack.</value>
		public int Range => _range;
        /// <value>How many milliseconds until the entity can attack next.</value>
		public int Cooldown => _cooldown; // TODO: create a class to store this for specific entities?


        // ----- Fields -----
        private readonly int _strength;
        private readonly int _range;
        private readonly int _cooldown;


        // ----- Constructors -----
        public AttackData(int strength, int range, int cooldown) {
            _strength = strength;
            _range = range;
            _cooldown = cooldown;
        }
	}
}
