using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Shroomworld.Physics;

/// <summary>
/// A physical body
/// </summary>
public class Body {

	public delegate bool CollisionChecker(Rectangle hitbox);

	// ----- Properties -----
	public Vector2 Velocity => _velocity;
	public Sprite Sprite => _sprite;
	public PhysicsData PhysicsData => _physicsData;

	
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
	/// Add acceleration of magnitude <see cref="Physics.Acceleration"/> in the given <paramref name="direction"/> (but don't apply it).
	/// </summary>
	/// <param name="direction">Direction of the acceleration.</param>
	public void AddAcceleration(Vector2 acceleration) {
		_acceleration += acceleration;
	}
	/// <summary>
	/// Change velocity based on acceleration and position based on velocity.
	/// <para>Note: velocity is limited by the body's max speed.</para>
	/// </summary>
	public void ApplyPhysics(float gravity, Func<Vector2, Point, IEnumerable<Point>> getIntersectingSolidTiles) {
		//AddGravity(gravity);
		ApplyAcceleration();
		ResetAcceleration();
		ApplyVelocity(getIntersectingSolidTiles);
	}
	/// <summary>
	/// Add gravity to the acceleration in the positive vertical direction.
	/// </summary>
	private void AddGravity(float gravity) {
		_acceleration.Y += gravity;
	}
	private void ApplyAcceleration() {
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
	}
	private void ApplyVelocity(Func<Vector2, Point, IEnumerable<Point>> getIntersectingSolidTiles) {
		_sprite.Position += _velocity;
		_sprite.Update();
		ResolveCollisions(getIntersectingSolidTiles(_sprite.Position, _sprite.Size));
	}
	private void ResolveCollisions(IEnumerable<Point> intersectingSolidTiles) {
		foreach (Point tile in intersectingSolidTiles) {
			if (_sprite.Hitbox.Contains(tile)) {

				// Find intersection.
				Vector2 intersection;
				if (_velocity.X < 0) {
					intersection.X = _sprite.Position.X - (tile.X + 1);
				}
				else {
					intersection.X = _sprite.Position.X - tile.X;
				}
				if (_velocity.Y < 0) {
					intersection.Y = _sprite.Position.Y - (tile.Y + 1);
				}
				else {
					intersection.Y = _sprite.Position.Y - tile.Y;
				}

				// Resolve collision.
				Vector2 v = Vector2.Normalize(_velocity);
				float scaleFactor;
				if (Math.Abs(intersection.X / intersection.Y) > 1) {
					scaleFactor = v.X / intersection.X;
				}
				else {
					scaleFactor = v.Y / intersection.Y;
				}
				v *= scaleFactor;
				_sprite.Position += v;
				_sprite.Update();
			}
		}
	}
	/// <summary>
	/// Set acceleration to zero.
	/// </summary>
	private void ResetAcceleration() {
		_acceleration = Vector2.Zero;
	}
}