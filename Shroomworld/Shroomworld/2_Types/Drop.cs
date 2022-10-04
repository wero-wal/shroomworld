using System;

namespace Shroomworld
{
	internal class Drop
	{
		// ----- Enums -----
		// ----- Properties -----
		// ----- Fields -----
		private readonly int _id;
		private readonly int _min;
		private readonly int _max;

		// ----- Constructors -----
		public Drop(string plainText, int separatorLevel)
		{
			string[] parts = plainText.Split(File.Separators[separatorLevel]);
			int i = 0;
			_id = Convert.ToInt32(parts[i++]);
			_min = Convert.ToInt32(parts[i++]);
			_max = Convert.ToInt32(parts[i++]);
		}

		// ----- Methods -----
	}
}