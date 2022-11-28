using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Shroomworld
{
    internal class MovementData
    {
        private readonly Vector2 _velocity;
        private readonly Vector2 _position; // position of the entity WITHIN THE WORLD (i.e. using the world's coordinate system; NOT MonoGame's)

        public MovementData(Vector2 position)
        {
            _velocity = Vector2.Zero;
            _position = position;
        }
    }
}
