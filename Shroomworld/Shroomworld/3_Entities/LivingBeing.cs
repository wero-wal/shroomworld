using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Shroomworld
{
    internal abstract class LivingBeing : IMoveable, IDamageable
    {
        // ----- Enums -----


        // ----- Properties -----


        // ----- Fields -----
        protected HealthInfo _healthInfo;
        protected Sprite _sprite;

        // ----- Constructors -----


        // ----- Methods -----
        public void Move(Vector2 direction)
        {

        }
        public void Collide(Vector2 collisionDirection, float collisionDepth)
        {

        }
        public void RegenerateHealth()
        {
            byte fps = 60;
            _healthInfo.RegenerateHealthNaturally(fps);
        }
        public void TakeDamage(byte amount)
        {
            _healthInfo.DecreaseHealth(amount, out bool dead);
            if (dead)
            {
                Die();
            }
        }
        public void Die()
        {
            _healthInfo.ResetHealth();
            // do something
        }
    }
}
