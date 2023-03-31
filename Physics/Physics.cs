using Microsoft.Xna.Framework;

namespace Shroomworld.Physics;

public class Physics {

	// ----- Properties -----
	public readonly Vector2 AccelerationUp;
	public readonly Vector2 AccelerationDown;
	public readonly Vector2 AccelerationLeft;
	public readonly Vector2 AccelerationRight;

	// ----- Constructors -----
	public Physics(float acceleration, float gravity) {
		AccelerationUp = Vector2.UnitY * -acceleration;
		AccelerationDown = Vector2.UnitY * acceleration;
		AccelerationLeft = Vector2.UnitX * -acceleration;
		AccelerationRight = Vector2.UnitX * acceleration;
	}
}