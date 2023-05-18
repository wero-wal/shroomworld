using System;
using System.Collections.Generic;

namespace Shroomworld;
public class Quest {

    // ----- Properties -----

    // ----- Fields -----
    private readonly QuestType _type;
    private readonly int[] _progress;

    // ----- Constructors -----
    public Quest(QuestType type, int[] progress) {
        _type = type;
        _progress = progress;
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
