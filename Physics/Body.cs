using Microsoft.Xna.Framework;

namespace Shroomworld.Physics;

/// <summary>
/// A physical body
/// </summary>
public class Body {

	// ----- Properties -----
	public Vector2 Velocity => _velocity;
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
	public void ApplyKinematics() {
		Vector2 newVelocity = _velocity + _acceleration;
		float newSpeed = newVelocity.Length();
		if (newSpeed > _physicsData.MaximumSpeed) {
			_velocity =  Vector2.Normalize(newVelocity) * _physicsData.MaximumSpeed;
		}
		else {
			_velocity = newVelocity;
		}
		// Decelerate
		if (_acceleration.Equals(Vector2.Zero)) {
			_velocity *= 0.95f;
		}
		_sprite.ChangePosition(_velocity);
	}
	/// <summary>
	/// Add gravity to the acceleration in the positive vertical direction.
	/// </summary>
	public void AddGravity(float gravity) {
		_acceleration.Y += gravity;
	}
	/// <summary>
	/// Add acceleration of magnitude <see cref="PhysicsData.Acceleration"/> in the given <paramref name="direction"/> (but don't apply it).
	/// </summary>
	/// <param name="direction">Direction of the acceleration.</param>
	public void AddAcceleration(Vector2 acceleration) {
		_acceleration += acceleration;
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
	/// <param name="resolutionVector">
	/// The vector by which the body should be moved in order to resolve the collision.
	/// </param>
	public void ResolveCollision(Vector2 resolutionVector) {
		_sprite.ChangePosition(resolutionVector);
	}
}