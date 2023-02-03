using System;

namespace MyPhysics
{
	/// <summary>
	/// Contains a bunch of constants
	/// </summary>
	public static class WorldSettings // todo: move into shroomworld project
	{
		public static float Gravity;
		public static Border WorldBorder;
		public static Border? MovementBoundary;
		public static float PixelsPerMetre;
		public static bool AllowBreakingLawsOfPhysics;
	}
}
