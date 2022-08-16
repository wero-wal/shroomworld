using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class PhysicalEntity
    {
        //---------- Enums ----------
        // ---------- Accessors ----------


        // ---------- Variables ----------
        protected readonly Texture Texture;
        protected readonly Point
            _gravity,
            jump,
            Move;
        protected readonly int
            Mass,
            ConstantOfRestitution;

        protected int
            MaxSpeed, // can be altered by power-ups
            Weight; // can be altered by gravity (e.g. in different dimensions)
        protected Point
            Position,
            Velocity,
            Acceleration,
            Forces;

        protected int Health;

        // ---------- Constructors ----------
        // ---------- Methods ----------
    }
}
