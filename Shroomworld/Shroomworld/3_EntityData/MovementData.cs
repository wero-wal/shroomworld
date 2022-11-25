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
        private readonly float _movementForce;
        private readonly float _mass;
        private Vector2 _velocity;
        private Vector2 _position;

        public MovementData(float movementForce, float mass, Vector2 velocity, Vector2 position)
        {
            _movementForce = movementForce;
            _mass = mass;
            _velocity = velocity;
            _position = position;
        }
    }
}
