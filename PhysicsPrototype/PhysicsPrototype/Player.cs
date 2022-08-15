using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PhysicsPrototype
{
    public class Player
    {
        // - - - - - Properties - - - - -
        public Sprite Sprite { get => _sprite; }

        private readonly float
            _maxSpeed, // m/s
            _movementForce, // N
            _mass; // kg
        private Sprite _sprite;
        private Vector2
            _velocity,
            _acceleration,
            _force;


        // - - - - - Constructors - - - - -
        public Player(Sprite sprite)
        {
            _sprite = sprite;
            _maxSpeed = 5;
            _movementForce = 100;

            _mass = 50;
        }


        // - - - - - Methods - - - - -
        // Movement
        public void Move(Vector2 direction, float friction, bool forceBeingApplied)
        {
            CalculateForce(direction, friction, forceBeingApplied);
            CalculateAcceleration();
            CalculateVelocity(); // Accelerate
            CalculatePosition(); // Move
        }
        public void CentreOnOrigin()
        {
            _sprite.Position -= new Vector2(_sprite.Texture.Width / 2, _sprite.Texture.Height / 2);
        }

        private void CalculateForce(Vector2 direction, float friction, bool forceBeingApplied)
        {
            if (TouchingGround())
            {
                if (forceBeingApplied)
                {
                    _force.X += _movementForce * direction.X; // move horizontally
                    _force.Y += _movementForce * direction.Y; // Jump
                }
                ApplyFriction(friction);
            }
            else // in the air
            {
                _force += Physics.Gravity;
            }
        }
        private void CalculateAcceleration()
        {
            _acceleration = _force / _mass;
        }
        private void CalculateVelocity()
        {
            if ((_velocity + _acceleration).Length() > _maxSpeed) // gone over max speed
            {
                _velocity = Vector2.Normalize(_velocity + _acceleration) * _maxSpeed; // lower the velocity to max in the same direction
            }
            else
            {
                _velocity += _acceleration;
            }
        }
        private void CalculatePosition()
        {
            var min = new Vector2(0, 0);
            var max = new Vector2(MyGame.BufferWidth - _sprite.Texture.Width, MyGame.BufferHeight - _sprite.Texture.Height);
            _sprite.Position = Clamp(_sprite.Position + _velocity, min, max); // Prevent it from going outside the screen
        }
        private void ApplyFriction(float friction)
        {
            _force.X -= friction * _velocity.X;
        }
        private bool TouchingGround()
        {
            return _sprite.Position.Y == (MyGame.BufferHeight - _sprite.Texture.Height);
        }

        private Vector2 Clamp(Vector2 vector, Vector2 min, Vector2 max)
        {
            vector.X = Clamp(vector.X, min.X, max.X);
            vector.Y = Clamp(vector.Y, min.Y, max.Y);

            return vector;
        }
        private float Clamp(float number, float min, float max)
        {
            if (number < min)
            {
                number = min;
            }
            if (number > max)
            {
                number = max;
            }
            return number;
        }
    }
}
