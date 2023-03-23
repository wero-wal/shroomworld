using Microsoft.Xna.Framework;

namespace Shroomworld;
public static class PointExtensions {
	/// <summary>
	/// Multiplies a <see cref="Point"/> by a scalar value.
	/// </summary>
	/// <param name="p"></param>
	/// <param name="scaleFactor">The scalar value.</param>
	/// <returns></returns>
	public static Point ScaleBy(this Point p, int scalar) {
		p.X *= scalar;
		p.Y *= scalar;
		return p;
	}
	public static Point ScaleBy(this Point p, float scalar) {
		p.X = (int)((float)p.X * scalar);
		p.Y = (int)((float)p.Y * scalar);
		return p;
	}
}