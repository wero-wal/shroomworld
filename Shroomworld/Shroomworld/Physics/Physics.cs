using System;
namespace Shroomworld
{
    internal class Physics
    {
        public static bool CheckForCollision(Body a, Body b, out Vector direction, out float depth)
		{
			// todo: do the separating axis theorem for a.Shape and b.Shape
			direction = null;
			depth = 1;
			return false;
		}
		private static bool CheckUsingSepAxisTheorem(Vertices a, Vertices b, out Vector direction, out float depth)
		{
			// todo: paste code here
			direction = null;
			depth = 1;
			return false;
		}

		public static void ResolveCollision(Body a, Body b, Vector direction, float depth, bool share)
		{
			a.Velocity *= -a.E; // maybe?
			b.Velocity *= -b.E; // maybe?

			if (share) // or is one of them is static
			{
				a.Position -= 0.5f * direction * depth;
				b.Position += 0.5f * direction * depth;
			}
			else
			{
				a.Position -= direction * depth;
			}
		}
    }
}
