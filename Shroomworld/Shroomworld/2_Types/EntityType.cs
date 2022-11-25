using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal abstract class EntityType : Type
    {
        // ----- Enums -----
        // ----- Properties -----
        // ----- Fields -----
        protected int _movementSpeed;
        protected float _constantOfRestitution;

        protected int _maxHealth;
        protected int _regenAmountPerSecond;

        // ----- Constructors -----
        // ----- Methods -----
        public HealthInfo GetHealthInfo()
        {
            return HealthInfo.CreateNew(_maxHealth, _regenAmountPerSecond);
        }

        protected void ParsePhysics(int index, string[] parts)
        {
            _movementSpeed = Convert.ToInt32(parts[index++]);
            _constantOfRestitution = Convert.ToSingle(parts[index++]);
        }
        protected void ParseHealth(int index, string[] parts)
        {
            _maxHealth = Convert.ToInt32(parts[index++]);
            _regenAmountPerSecond = Convert.ToInt32(parts[index++]);
        }
    }
}
