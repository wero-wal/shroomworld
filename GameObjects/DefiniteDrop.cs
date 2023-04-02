using System;

namespace Shroomworld.Drops;

public class DefiniteDrop : IDroppable {

	// ----- Properties -----
	// ----- Fields -----
	private readonly int _id;
	private readonly int _amount;

	// ----- Constructors -----
	private DefiniteDrop(int id, int amount) {
		_id = id;
		_amount = amount;
	}
	
	// ----- Methods -----
	public static DefiniteDrop Parse(string[] plaintext) {
		return new DefiniteDrop(
			id: Convert.ToInt32(plaintext[0]),
			amount: Convert.ToInt32(plaintext[1])
		);
	}
	public InventoryItem Drop() {
		return new InventoryItem (_id, _amount);
	}
}