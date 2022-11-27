using System;

namespace Shroomworld
{
	internal class Drop
	{
		// ----- Enums -----
		// ----- Properties -----
		public int Id => _id;

		// ----- Fields -----
		private readonly int _id;
		private readonly int _min;
		private readonly int _max;

		// ----- Constructors -----
		public Drop(int id, int minInclusive, int maxExclusive)
		{
			_id = id;
			_min = minInclusive;
			_max = maxExclusive;
		}
		internal Drop(params int[] properties)
		{
			_id = properties[0];
			_min = properties[1];
			_max = properties[2];
		}

        // ----- Methods -----
        public int GetAmount() => (int)Math.Floor((decimal)new Random().Next(_min, _max));
    }
}