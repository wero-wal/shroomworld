using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    /// <summary>
    /// Contains information about an entity's attack.
    /// </summary>
    internal class ReadonlyAttackData
    {
        /// <summary>
        /// The number of health points of damage the entity will deal upon attacking another entity.
        /// </summary>
		public int Strength => _strength;
        /// <summary>
        /// The maximum distance (in tiles) from which the entity can attack.
        /// </summary>
		public int Range => _range;
        /// <summary>
        /// How many milliseconds until the entity can attack next.
        /// </summary>
		public int Cooldown => _cooldown;


        private readonly int _strength;
        private readonly int _range;
        private readonly int _cooldown;


        public ReadonlyAttackData(int strength, int range, int cooldown)
        {
            _strength = strength;
            _range = range;
            _cooldown = cooldown;
        }
	}
}
