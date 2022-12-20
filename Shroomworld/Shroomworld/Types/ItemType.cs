using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class ItemType
    {
        private readonly IdentifyingData _idData;
        private readonly string[] _tags; // includes properties such as placeable, stackable

        public ItemType(IdentifyingData identifiers, params string[] tags)
        {
            _idData = identifiers;
            _tags = tags;
        }
    }
}
