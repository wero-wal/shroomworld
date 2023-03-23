using Microsoft.Xna.Framework;

namespace Shroomworld.Physics;

public class Physics {

	// ----- Properties -----
	public float Acceleration => _acceleration;
	public float Gravity => _gravity;
	public readonly Vector2 AccelerationUp;
	public readonly Vector2 AccelerationDown;
	public readonly Vector2 AccelerationLeft;
	public readonly Vector2 AccelerationRight;

	// ----- Fields -----
	private readonly float _acceleration;
	private readonly float _gravity;

	// ----- Constructors -----
	public Physics(float acceleration, float gravity) {
		_acceleration = acceleration;
		AccelerationUp = Vector2.UnitY * -acceleration;
		AccelerationDown = Vector2.UnitY * acceleration;
		AccelerationLeft = Vector2.UnitX * -acceleration;
		AccelerationRight = Vector2.UnitX * acceleration;
		_gravity = gravity;
	}
}