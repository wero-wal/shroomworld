using System;
using System.Collections.Generic;

namespace Shroomworld;

public class Quest {

    // ----- Properties -----
    public string Description => _description;
    public int NumberOfRequirements => _requiredItems.Length;

    // ----- Fields -----
    private readonly string _description;
    private readonly InventoryItem[] _requiredItems;
    private readonly int[] _progress;

    // ----- Constructors -----
    public Quest(string description, InventoryItem[] requiredItems) {
        _description = description;
        _requiredItems = requiredItems;
        _progress = new int[requiredItems.Length];
    }

    // ----- Methods -----
    public void Update(Inventory inventory) {
        for (int i = 0; i < _requiredItems.Length; i++) {
            if (inventory.FindItemWithId(_requiredItems[i].Id).TryGetValue(out InventoryItem inventoryItem)) {
                _progress[i] = Math.Min(inventoryItem.Amount, _requiredItems[i].Amount);
            }
        }
    }
    public override string ToString() {
        string questInfo = string.Empty;
		questInfo += $"{_description}{Environment.NewLine}{string.Join(Environment.NewLine, GetProgressReport())}{Environment.NewLine}";
        return questInfo;
    }
    private IEnumerable<string> GetProgressReport() {
        for (int i = 0; i < _requiredItems.Length; i++) {
            yield return $"{_progress[i]} / {_requiredItems[i].Amount} {World.ItemTypes[_requiredItems[i].Id].IdData.PluralName}";
        }
    }
}
