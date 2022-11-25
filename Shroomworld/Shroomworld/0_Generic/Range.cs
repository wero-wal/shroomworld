using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class Range
    {
        // ----- Enums -----


        // ----- Properties -----


        // ----- Fields -----
        private byte _min;
        private byte _max;

        // ----- Constructors -----
        internal Range(byte min, byte max, bool includeMax)
        {
            _min = min;
            _max = (byte)((includeMax) ? max + 1 : max);
        }

        // ----- Methods -----
        public byte ClampToRange(byte value)
        {
            return (byte)Math.Clamp(value, _min, _max - 1);
        }
        public bool CheckIfInRange(byte value)
        {
            return _min <= value && _max < value;
        }
    }
}
