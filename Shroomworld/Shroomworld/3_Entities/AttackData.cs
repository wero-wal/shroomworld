using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class AttackData
    {
        public int _strength;
        public int _range;
        public int _speed;

        public AttackData(int strength, int range, int speed)
        {
            _strength = strength;
            _range = range;
            _speed = speed;
        }
    }
}
