using System;
using Microsoft.Xna.Framework;
namespace Shroomworld
{
    internal class Body
    {
		// ----- Properties -----
		public Vector2 Velocity => _velocity;
		public Vector2 Position { get => _position; internal set => _position = value; }
		public Sprite Sprite => _sprite;

		// ----- Fields -----
		// todo: add universal coefficient/constant of restitution
		// todo: add universal acceleration
		private readonly float _maxSpeed;

		private Sprite _sprite;

		private Vector2 _acceleration;
		private Vector2 _velocity; // in m/s
		private Vector2 _position; // in m

		// ----- Constructor(s) -----
		public Body()
		{

		}

		// ----- Methods -----
		private void ApplyPhysics()
		{
			// todo: implement max speed
			_velocity += _acceleration;
			_position += _velocity; // todo: sort out positions (which ones are positions in the world, and which are positions on screen? etc.)
		}
		/// <summary>
		/// Set acceleration but don't apply it
		/// </summary>
		/// <param name="direction"></param>
		/// <param name="magnitude">ignore this if <paramref name="direction"/> is not normalized</param>
		public static void SetAcceleration(Vector2 direction, float magnitude = 1)
		{
			_acceleration = direction * magnitude;
		}
    }
}
