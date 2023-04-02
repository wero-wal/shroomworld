using System;
namespace Shroomworld;
public class RandomDrop : IDroppable {
	// ----- Enums -----
	// ----- Properties -----
	public int Id => _itemId;

	// ----- Fields -----
	private readonly int _itemId;
	private readonly int _min;
	private readonly int _max;

	// ----- Constructors -----
	/// <summary>
	/// 
	/// </summary>
	/// <param name="itemId">ID of the item that will be dropped.</param>
	/// <param name="min">Minimum (inclusive) amount of the item that could be dropped.</param>
	/// <param name="max">Maximum (inclusive) amount of the item that could be dropped.</param>
	public RandomDrop(int itemId, int min, int max) {
		_itemId = itemId;
		_min = min;
		_max = max + 1;
	}

	// ----- Methods -----
	public static RandomDrop Parse(string[] plaintext) {
		return new RandomDrop(
			itemId: Convert.ToInt32(plaintext[0]),
			min: Convert.ToInt32(plaintext[0]),
			max: Convert.ToInt32(plaintext[0])
		);
	}
	public InventoryItem Drop() {
		return new InventoryItem(_itemId, new Random().Next(_min, _max));
	}
}