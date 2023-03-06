using Microsoft.Xna.Framework;
namespace Shroomworld.Physics {
	/// <summary>
	/// A physical body
	/// </summary>
    public class Body {

		// ----- Properties -----
		public Vector2 Velocity => _velocity;
		public Vector2 Position => _sprite.Position;
		public Sprite Sprite => _sprite;

		
		// ----- Fields -----
		private readonly Sprite _sprite;
		private readonly PhysicsData _physicsData;
		private Vector2 _acceleration;
		private Vector2 _velocity; // in m/s

		
		// ----- Constructors -----
		/// <summary>
		/// Creates an instance of a body with a velocity and an acceleration of 0.
		/// </summary>
		public Body(Sprite sprite, PhysicsData physicsData) {
			_sprite = sprite;
			_velocity = Vector2.Zero;
			_acceleration = Vector2.Zero;
			_physicsData = physicsData;
		}

		// ----- Methods -----
		/// <summary>
		/// Change velocity based on acceleration and position based on velocity.
		/// <para>Note: velocity is limited by the body's max speed.</para>
		/// </summary>
		public void ApplyPhysics() {
			Vector2 newVelocity = _velocity + _acceleration;
			float newSpeed = newVelocity.Length();
			if (newSpeed > _physicsData.MaximumSpeed) {
				_velocity =  Vector2.Normalize(newVelocity) * _physicsData.MaximumSpeed;
			}
			else {
				_velocity = newVelocity;
			}
			_sprite.SetPosition(_sprite.Position + _velocity);
		}
		/// <summary>
		/// Add gravity to the acceleration in the positive vertical direction.
		/// </summary>
		public void AddGravity() {
			_acceleration.Y += PhysicsData.Gravity;
		}
		/// <summary>
		/// Add acceleration of magnitude <see cref="PhysicsData.Acceleration"/> in the given <paramref name="direction"/> (but don't apply it).
		/// </summary>
		/// <param name="direction">Direction of the acceleration.</param>
		public void AddAcceleration(Vector2 direction) {
			_acceleration += direction * PhysicsData.Acceleration;
		}
		/// <summary>
		/// Set acceleration to zero.
		/// </summary>
		public void ResetAcceleration() {
			_acceleration = Vector2.Zero;
		}
		/// <summary>
		/// Changes the position of the body to move it out of the hitbox of the tile with which it has collided.
		/// </summary>
		/// <param name="direction">The (normalised) angle / direction at which the body has intersected with the tile.</param>
		/// <param name="depth">The depth of the collision.</param>
		public void ResolveCollision(Vector2 direction, float depth) {
			_sprite.SetPosition(_sprite.Position - direction * depth);
		}
	}
}
