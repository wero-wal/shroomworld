namespace Shroomworld;
public class DefiniteDrop {
	// ----- Properties -----
	// ----- Fields -----
	private readonly ItemType _type;
	private readonly int _amount;

	// ----- Constructors -----
	private DefiniteDrop(ItemType type, int amount) {
		_type = type;
		_amount = amount;
	}
	
	// ----- Methods -----
	public InventoryItem Drop() {
		return new InventoryItem (_type, _amount);
	}
}