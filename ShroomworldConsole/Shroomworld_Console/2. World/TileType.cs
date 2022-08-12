using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    internal class TileType : Type
    {
        private readonly bool _is_solid;
        private readonly List<Drop> _drops;

        //---Constructor---
        public TileType(string fromFile)
        {

        }

        // public override string GetKey()
        // {
        //     return $"tile:{_id}";
        // }
    }
}
