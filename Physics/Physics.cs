using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Shroomworld.Physics;

public class Physics {

	// ----- Properties -----
	public float Acceleration => _acceleration;
	public float Gravity => _gravity;

	// ----- Fields -----
	private readonly float _acceleration;
	private readonly float _gravity;

	// ----- Constructors -----
	public Physics(float acceleration, float gravity) {
		_acceleration = acceleration;
		_gravity = gravity;
	}