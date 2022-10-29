using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhysics
{
	public class Engine
	{
		public static bool CheckForCollision(Body a, Body b, out Vector direction, out float depth)
		{
			// do the separating axis theorem for a.Shape and b.Shape
			direction = null;
			depth = 1;
			return false;
		}
		private static bool CheckUsingSepAxisTheorem(Shape a, Shape b, out Vector direction, out float depth)
		{
			// paste code here
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

		/// <summary>
		/// Applies a force to a <see cref="MyPhysics.Body"/> and adjusts all its other values accordingly.
		/// Overwrites the previous force.
		/// </summary>
		/// <param name="body"></param>
		/// <param name="direction"></param>
		/// <param name="magnitude">ignore this if <paramref name="direction"/> is not normalized</param>
		public static void ApplyForce(Body body, Vector direction, float magnitude = 1)
		{
			body.Force = direction * magnitude;
		}
		public static void ApplyForces(Body body, params Vector[] forces)
		{
			Vector resultantForce = Vector.Zero;
			foreach (Vector force in forces)
			{
				resultantForce += force;
			}
			body.Force = resultantForce;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="body"></param>
		/// <param name="direction"></param>
		/// <param name="magnitude">ignore this if <paramref name="direction"/> is not normalized</param>
		public static void AddForce(Body body, Vector direction, float magnitude = 1)
		{
			body.Force += direction * magnitude;
		}
	}
}
