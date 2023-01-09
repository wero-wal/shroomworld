using System;
namespace Shroomworld
{
	internal class Drop
	{
		// ----- Enums -----
		// ----- Properties -----
		public int Id => _itemId;

		// ----- Fields -----
		private readonly int _itemId;
		private readonly int _min;
		private readonly int _max;

		// ----- Constructors -----
		public Drop(int itemId, int minInclusive, int? maxExclusive = null)
		{
			_itemId = itemId;
			_min = minInclusive;
			_max = maxExclusive ?? minInclusive + 1;
		}
		internal Drop(params int[] properties)
		{
			_itemId = properties[0];
			_min = properties[1];
			_max = properties[2];
		}

        // ----- Methods -----
		public Item DropItem() => new Item(_itemId, new Random().Next(_min, _max));
    }
}