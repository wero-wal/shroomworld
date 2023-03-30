using System;

namespace Shroomworld;
public class InventoryItem {

    // ----- Properties -----
    public int Id => _id;
    public int Amount => _amount;

    // ----- Fields -----
    private readonly int _id;
    private int _amount;

    // ----- Constructors -----
    public InventoryItem(int id, int amount) {
        _id = id;
        _amount = amount;
    }

    // ----- Methods -----
    public void IncreaseAmountBy(int amount) {
        if (amount < 0) {
            throw new ArgumentException("Can't increase amount by a negative amount.");
        }
        _amount += amount;
    }
    public void DecreaseAmountBy(int amount) {
        if (amount < 0) {
            throw new ArgumentException("Can't decrease amount by a negative number.");
        }
        if (_amount < amount) {
            throw new ArgumentException("Can't decrease by this amount as there is not enough of this item.");
        }
        _amount -= amount;
    }
}