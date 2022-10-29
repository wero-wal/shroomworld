using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhysics
{
	public class Vector
	{
		public static Vector Zero = new Vector(0,0);
		public static Vector I = new Vector(1,0);
		public static Vector J = new Vector(0,1);
		
		public float X, Y;

		//private event EventHandler Changed;

		public Vector()
		{
			X = 0;
			Y = 0;
		}
		public Vector(float x, float y)
		{
			X = x;
			Y = y;
		}

		public static Vector operator +(Vector a, Vector b)
		{
			return new Vector(a.X + b.X, a.Y + b.Y);
		}
		public static Vector operator -(Vector a, Vector b)
		{
			return new Vector(a.X - b.X, a.Y - b.Y);
		}
		public static Vector operator -(Vector a)
		{
			return new Vector(-a.X, -a.Y);
		}
		public static Vector operator *(float a, Vector b)
		{
			return new Vector (a * b.X, a * b.Y);
		}
		public static Vector operator *(Vector a, float b)
		{
			return new Vector (b * a.X, b * a.Y);
		}

		public float Magnitude()
		{
			return MathF.Sqrt(X * X + Y * Y);
		}
	}
}
