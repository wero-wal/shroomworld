using Microsoft.Xna.Framework;
namespace Shroomworld.Physics;

public class Physics {
    public static bool CheckForCollision(Body a, Body b, out Vector2 direction, out float depth) {
		// todo: do the separating axis theorem for a.Shape and b.Shape
		direction = Vector2.Zero;
		depth = 1;
		return false;
	}
	private static bool CheckUsingSepAxisTheorem(Vertices a, Vertices b, out Vector2 direction, out float depth)
	{
		// todo: paste code here
		direction = Vector2.Zero;
		depth = 1;
		return false;
	}

	public static void ResolveCollision(Body a, Body b, Vector2 direction, float depth, bool share)
	{
		//a.Velocity *= -a.E; // maybe?
		//b.Velocity *= -b.E; // maybe?

		if (share) // or is one of them is static
		{
			a.ResolveCollision(direction, depth / 2);
			b.ResolveCollision(direction, depth / 2);
		}
		else
		{
			a.ResolveCollision(direction, depth);
		}
	}
    }
