using System;
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

	// ----- Methods -----
	/// <summary>
	/// Check for and resolve all collisions for a specific <paramref name="entity"/> for a given set of <paramref name="tiles"/>.
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="tiles">Tiles which may potentially intersect the entity.
	/// (All the tiles in the area from the top left corner of the entity's hitbox to the bottom right.)</param>
	public void ResolveCollisions(Body entity, Rectangle[] tiles) {
		Vector2 resolutionVector;
		foreach(Rectangle tile in tiles) {
			if (CheckIfIntersect(entity, tile, out resolutionVector)) {
				entity.ResolveCollision(resolutionVector);
			}
		}
	}
	/// <summary>
	/// Use the Separating Axis Theorem to check whether or not two tiles intersect.
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="tile"></param>
	/// <param name="direction"></param>
	/// <param name="depth"></param>
	/// <returns></returns>
	private bool CheckIfIntersect (Body entity, Rectangle tile, out Vector2 resolutionVector) {
		resolutionVector = Vector2.Zero;
		bool intersects = false;

		// Check if the ... of the entity's hitbox is within the tile.
		// - left
		if ((tile.Left < entity.Sprite.Hitbox.Left)
		&& (entity.Sprite.Hitbox.Left < tile.Right)) {
			resolutionVector.X += tile.Right - entity.Sprite.Hitbox.Left;
			intersects = true;
		}
		// - right
		else if ((tile.Left < entity.Sprite.Hitbox.Right)
		&& (entity.Sprite.Hitbox.Right < tile.Right)) {
			resolutionVector.X += tile.Left - entity.Sprite.Hitbox.Right;
			intersects = true;
		}
		// - top
		if ((tile.Top < entity.Sprite.Hitbox.Top)
		&& (entity.Sprite.Hitbox.Top < tile.Bottom)) {
			resolutionVector.Y += entity.Sprite.Hitbox.Top - tile.Top;
			intersects = true;
		}
		// - bottom
		else if ((tile.Top < entity.Sprite.Hitbox.Bottom)
		&& (entity.Sprite.Hitbox.Bottom < tile.Bottom)) {
			resolutionVector.Y += tile.Top - entity.Sprite.Hitbox.Bottom;
			intersects = true;
		}
		return intersects;
	}
}