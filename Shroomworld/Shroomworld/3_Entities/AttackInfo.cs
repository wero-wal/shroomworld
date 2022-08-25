using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class AttackInfo : IAggressive
    {
        // ---------- Enums ----------


        // ---------- Properties ----------


        // ---------- Fields ----------
        private readonly byte _strength;
        private readonly byte _range;
        private readonly byte _speed;
        private readonly byte _cooldown;

        // ---------- Constructors ----------


        // ---------- Methods ----------
        public void Attack(out byte strength)
        {
            strength = _strength;
            // do animation or something
            return;
        }
    }
}
