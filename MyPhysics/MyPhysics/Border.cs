using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhysics
{
	public class Border
	{
		public readonly int Top;
		public readonly int Right;
		public readonly int Bottom;
		public readonly int Left;

		public Border(int top, int right, int bottom, int left)
		{
			Top = top;
			Right = right;
			Bottom = bottom;
			Left = left;
		}
	}
}
