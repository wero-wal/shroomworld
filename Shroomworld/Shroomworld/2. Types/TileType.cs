using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    public class TileType : XType
    {
        // ---------- Enums ----------
        // ---------- Properties ----------
        // ---------- Fields ----------
        // ---------- Constructors ----------
        // ---------- Methods ----------
        public MoveableEntity GenerateEntity()
        {
            return new MoveableEntity();
        }
    }
}
