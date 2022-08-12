using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class EnemyType : Type
    {
        //---Enums---
        //---Constants---
        //---Accessors---
        public string Name { get => _name; }
        public string PluralName { get => _plural_name; }

        //---Variables---
        private readonly Texture _texture;
        private readonly string _name;
        private readonly string _plural_name;
        private int
            _attack_range,
            _attack_strength,
            _max_health,
            _max_speed;

        //- - - - - constructors - - - - -
        //- - - - - methods - - - - -
        // public override string GetKey()
        // {
        //     return $"enemy:{_id}"
        // }
    }
}
