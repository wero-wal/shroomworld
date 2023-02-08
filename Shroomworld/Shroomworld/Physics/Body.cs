using System;
using Microsoft.Xna.Framework;
namespace Shroomworld
{
    internal class Body
    {
		// ----- Properties -----
		public Sprite Sprite => _sprite;

		// ----- Fields -----
		// todo: add universal coefficient/constant of restitution
		// todo: add universal acceleration
		private const float DefaultMagnitude = 1f;
		private readonly float _maxSpeed;
		private Sprite _sprite;
		private Vector2 _acceleration;
		private Vector2 _velocity; // in m/s

		// ----- Constructor(s) -----
		/// <summary>
		/// Creates an instance of a body with a velocity and an acceleration of 0.
		/// </summary>
		public Body(Sprite sprite, float maxSpeed)
		{
			_sprite = sprite;
			_maxSpeed = maxSpeed;
			_velocity = Vector2.Zero;
			_acceleration = Vector2.Zero;
		}

		// ----- Methods -----
		/// <summary>
		/// Change velocity based on acceleration and position based on velocity.
		/// <para>Note: velocity is limited by the body's max speed.</para>
		/// </summary>
		public void ApplyPhysics()
		{
			Vector2 newVelocity = _velocity + _acceleration;
			float newSpeed = newVelocity.Modulus();
			if (newSpeed > _maxSpeed)
			{
				_velocity = newVelocity.Normalize() * _maxSpeed;
			}
			else
			{
				_velocity = newVelocity;
			}
			_position += _velocity;
		}
		/// <summary>
		/// Sets acceleration but doesn't apply it (i.e. doesn't change any other values to reflect this).
		/// </summary>
		/// <param name="direction">
		/// 	<para>Direction vector of the acceleration.</para>
		/// 	<para>Can be normalised or unnormalised. If normalised, remember to set a <paramref name="magnitude"/>.</para>
		/// </param>
		/// <param name="magnitude">
		/// 	<para>Magnitude of the acceleration.</para>
		/// 	<para>Default value: <see cref="DefaultMagnitude"/></para>
		/// </param>
		public void SetAcceleration(Vector2 direction, float magnitude = DefaultMagnitude)
		{
			_acceleration = direction * magnitude;
		}
		/// <summary>
		/// Changes the position of the body to move it out of the hitbox of the tile with which it has collided.
		/// </summary>
		/// <param name="direction">The (normalised) angle / direction at which the body has intersected with the tile.</param>
		/// <param name="depth">The depth of the collision.</param>
		public void ResolveCollision(Vector2 direction, float depth)
		{
			_sprite.Position -= direction * depth;
		}
	}
}
