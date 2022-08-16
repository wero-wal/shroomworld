using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PhysicsPrototype
{
    public class Physics
    {
        public enum Vertices
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
        }


        // - - - - - Properties - - - - -
        private const int
            _verticesCount = 4,
            A = 0, B = 1;

        private readonly float
            _movementForce, // N
            _mass; // kg

        private Vector2
            _velocity,
            _force,
            _weight;

        private bool _isTouchingGround;

        private Sprite _sprite;


        // - - - - - Constructor - - - - -
        public Physics(float movementForce, float mass, ref Sprite sprite)
        {
            _movementForce = movementForce;
            _mass = mass;
            _sprite = sprite;

            _weight = mass * MyGame.Gravity;

            _force = _weight;
            _velocity = Vector2.Zero;

            _isTouchingGround = CheckIfTouchingGround();
        }

        /// <summary>
        /// Checks if two objects intersect.
        /// </summary>
        /// <param name="entitiesVertices">The two <paramref name="entitiesVertices"/> to be tested for collision.</param>
        /// <returns></returns>
        public static bool Intersect(params Vector2[][] entitiesVertices) // This is the Separating Axis Theorem
        {
            float min, max;
            List<float> mins = new List<float>(entitiesVertices.Length);
            List<float> maxs = new List<float>(entitiesVertices.Length);

            Vector2 a, b, edge, axis = new Vector2();
            Vector2[] currentVertices;

            for (int s = 0; s < entitiesVertices.Length; s++)
            {
                currentVertices = entitiesVertices[s];
                for (int v = 0; v < _verticesCount; v++)
                {
                    // Obtain axis using edge
                    a = currentVertices[v];
                    b = currentVertices[(v + 1) % _verticesCount];
                    edge = b - a; // edge AB

                    axis.X = -edge.Y;
                    axis.Y = edge.X;

                    // Project vertices onto axis
                    foreach (var vertices in entitiesVertices)
                    {
                        ProjectVertices(vertices, axis, out min, out max);
                        mins.Add(min);
                        maxs.Add(max);
                    }

                    // Check for intersection on this axis
                    if (mins[A] >= maxs[B] || mins[B] >= maxs[A])
                    {
                        return false; // can't possibly intersect on this axis
                    }
                }
            }
            return true; // must intersect
        }
        private static void ProjectVertices(Vector2[] vertices, Vector2 axis, out float min, out float max)
        {
            float projection;
            min = float.MaxValue;
            max = float.MinValue;

            foreach (var vertex in vertices)
            {
                projection = Vector2.Dot(vertex, axis);
                if (projection < min)
                {
                    min = projection;
                }
                if (projection > max)
                {
                    max = projection;
                }
            }
        }

        // MOVE
        public void Move(Vector2 direction, float friction, float elapsedTimeInSeconds)
        {
            _isTouchingGround = CheckIfTouchingGround();

            // Calculate Force, Acceleration, Velocity
            _force = _weight + CalculateNormalForce() + CalculateMovementForce(direction) + CalculateFriction(friction);
            var acceleration = _force / _mass;
            _velocity += acceleration;// * elapsedTimeInSeconds;

            // Calculate Position
            var newPosition = _sprite.Position + _velocity * elapsedTimeInSeconds;
            _sprite.Position = Clamp(newPosition, MyGame.TopLeftOfScreen, MyGame.BottomRightOfScreen - _sprite.Size); // Prevent it from going outside the screen
        }
        private bool CheckIfTouchingGround()
        {
            return (_sprite.Position.Y + _sprite.Texture.Height) == (MyGame.BufferHeight);
        }
        private Vector2 CalculateNormalForce() // from ground
        {
            if (_isTouchingGround)
            {
                return (-Vector2.UnitY) * _force;
            }
            return Vector2.Zero;
        }
        private Vector2 CalculateMovementForce(Vector2 direction)
        {
            if (_isTouchingGround)
            {
                return _movementForce * direction;
            }
            else
            {
                return _movementForce * direction * Vector2.UnitX; // can't jump when in mid-air
            }
        }
        private Vector2 CalculateFriction(float friction)
        {
            if (_isTouchingGround)
            {
                return friction * _velocity * (-Vector2.UnitX);
            }
            return Vector2.Zero;
        }

        public static Vector2 Clamp(Vector2 vector, Vector2 min, Vector2 max)
        {
            if (vector.X < min.X)
            {
                vector.X = min.X;
            }
            if (vector.X > max.X)
            {
                vector.X = max.X;
            }
            if (vector.Y < min.Y)
            {
                vector.Y = min.Y;
            }
            if (vector.Y > max.Y)
            {
                vector.Y = max.Y;
            }
            return vector;
        }
    }
}
