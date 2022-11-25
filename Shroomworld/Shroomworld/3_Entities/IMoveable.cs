using System;
using Microsoft.Xna.Framework;

namespace Shroomworld
{
    internal interface IMoveable
    {
        void Move(Vector2 direction);
        void Collide(Vector2 direction, float depth);
    }
}
