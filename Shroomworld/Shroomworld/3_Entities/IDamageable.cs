using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal interface IDamageable
    {
        void TakeDamage();
        void RegenerateHealth();
        void Die();
    }
}
