using System;
using System.Collections.Generic;

namespace Shroomworld;
public class Inventory {
	
	// ----- Properties -----
	/// <value>
	/// The maximum possible amount of piles of items that can be stored in the inventory.
	/// </value>
	public int Capacity = Width * Height;
	/// <value>
	/// The amount of different items there currently are in the inventory.
	/// </value>
	public int Volume => _items.Length;

	public Maybe<InventoryItem> this[int x, int y] {
		get => _items[x, y];
	}
	public InventoryItem SelectedItem => _items[_selected, HotbarRow];
	public int SelectedSlot => _selected;
	
	// ----- Fields -----
	public const int Width = 5;
	public const int Height = 4;
	public const int HotbarRow = 0;
	
	private readonly InventoryItem[,] _items;
	private int _selected = 0; // The index of the slot in the hotbar row which is currently selected (i.e. which the player is holding).
	private (int x, int y) _endPointer = (0, 0);

	// ----- Constructors -----
	public Inventory() {
		_items = new InventoryItem[Width, Height];
	}
	public Inventory(IEnumerable<InventoryItem> items) {
		_items = new InventoryItem[Width, Height];
		foreach (InventoryItem item in items) {
			AddNewItem(item);
		}
	}

	// ----- Methods -----
	/// <summary>
	/// 	Adds the desired <paramref name="amount"/> to the appropriate item slot.
	/// 	If no slot exists for this item type, a new slot will be created for it.
	/// </summary>
	/// <param name="itemId">The id of the item you wish to add to / increase the amount of in the inventory.</param>
	/// <param name="amount">The amount of the specified item which you would like to add to the inventory.</param>
	/// <exceptions>
	/// 	An exception will be thrown if the inventory is full and the
	/// 	caller is attempting to add items which required a new slot.
	/// </exceptions>
	public void Add(InventoryItem inventoryItem) {
		if (!FindLocationOfItemWithId(inventoryItem.Id).TryGetValue(out (int x, int y) location)) {
			if (IsFull()) {
				throw new Exception("The inventory is full.");
			}
			AddNewItem(inventoryItem);
			return;
		}
		GetItemAt(location).IncreaseAmountBy(inventoryItem.Amount);
	}
	/// <summary>
	/// 	Attempts to decrease the amount of the item with the specified
	/// 	<paramref name="itemId"/> by the specified <paramref name="amount"/>
	/// </summary>
	/// <param name="itemId">The id of the item whose amount you want to decrease.</param>
	/// <param name="amount">The amount by which you want to decreasethe amount of
	/// the item in the inventory. Should be a positive value.</param>
	/// <exceptions>
	/// <see cref="ArgumentException"/>: thrown if the <paramref name="amount"/> passed was either
	/// <c>&lt;0</c> or <c>&gt;</c> the amount of that item currently in the inventory.
	/// <See cref="KeyNotFoundException"/>
	/// </exceptions>
	public void Remove(InventoryItem inventoryItem) {
		if (!FindLocationOfItemWithId(inventoryItem.Id).TryGetValue(out (int x, int y) location)) {
			throw new KeyNotFoundException();
		}
		// Remove the item from the inventory if there is none of it left.
		if (GetItemAt(location).Amount == inventoryItem.Amount) {
			RemoveItem(inventoryItem, location);
			return;
		}
		GetItemAt(location).DecreaseAmountBy(inventoryItem.Amount);
	}
	public void SelectSlot(int slot) {
        if ((0 <= slot) && (slot < Width)) {
			_selected = slot;
        }
    }
	public Maybe<InventoryItem> FindItemWithId(int id) {
		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
				if ((_items[x, y] is not null) && (_items[x, y].Id == id)) {
					return _items[x, y];
				}
			}
		}
		return Maybe.None;
	}

	private void AddNewItem(InventoryItem inventoryItem) {
		_items[_endPointer.x, _endPointer.y] = inventoryItem;
		MoveEndPointerForward();
		if (!IsFull()) {
			MoveEndPointerToEmptySlot();
		}
	}
	private void RemoveItem(InventoryItem inventoryItem, (int x, int y) location) {
		_items[location.x, location.y] = null;
		_endPointer = location;
	}
	private void MoveEndPointerToEmptySlot() {
		if (GetItemAt(_endPointer) is null) {
			return;
		}
		_endPointer = (0, 0);
		while ((GetItemAt(_endPointer) is not null) && (!IsFull())) {
			MoveEndPointerForward();
		}
	}
	private void MoveEndPointerForward() {
		if ((++_endPointer.x) == Width) {
			_endPointer.x = 0;
			_endPointer.y++;
		}
	}
	private InventoryItem GetItemAt((int x, int y) location) {
		return _items[location.x, location.y];
	}
	private Maybe<(int x, int y)> FindLocationOfItemWithId(int id) {
		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
				if ((_items[x, y] is not null) && (_items[x, y].Id == id)) {
					return (x, y);
				}
			}
		}
		return Maybe.None;
	}
	private bool IsFull() => _endPointer.y == Height;
}