namespace Shroomworld;
public class QuestType {
	// ----- Enums -----
	// ----- Properties -----
	// ----- Fields -----
	public readonly int Id;
	public readonly string Description;

	private readonly InventoryItem[] _requiredItems;

	// ----- Constructors -----
	private QuestType(int id, string description, InventoryItem[] requiredItems) {
		Id = id;
		Description = description;
		_requiredItems = requiredItems;
	}

	// ----- Methods -----
	public InventoryItem GetItem(int index) {
		return _requiredItems[index];
	}
	public Quest CreateNew() {
		return new Quest(
			type: this,
			progress: new int[_requiredItems.Length]
		);
	}
}