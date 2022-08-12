using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class ItemType
    {
        //---Enums---

        //---Constants---
        private string NormalPlural { get { return _name + "s"; } }

        //---Accessors---
        public string Name { get => _name; }
        public string PluralName { get => _plural_name; }

        //---Variables---
        private readonly Texture _texture;
        private readonly string
            _name,
            _plural_name;

        //---Constructor---
        public ItemType(string plural)
        {
            if (plural == NormalPlural)
            {
                _plural_name = _name + "s";
            }
            else
            {
                _plural_name = plural;
            }
        }

        //---Methods---

    }
}
