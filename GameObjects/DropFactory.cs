using System;

namespace Shroomworld.Drops;

/// <summary>
/// Instantiates <see cref="IDroppable"/> objects.
/// </summary>
public static class DropFactory {
	
	/// <summary>
	/// Instantiates an <see cref="IDroppable"/> based on the number of properties passed.
	/// </summary>
	/// <param name="plaintext">The values with which to instantiate the drop.</param>
	/// <returns>
	/// 	A new <see cref="IDroppable"/> object of the appropriate kind based on the number of
	/// 	properties passed.
	/// </returns>
	public static IDroppable Parse(string[] plaintext) {

		const int NumberOfDefiniteDropProperties = 2;
		const int NumberOfRandomDropProperties = 3;
		const string WrongNumberOfPropertiesErrorMessage = "Can't create a drop with this amount of properties.";

		// Create and return the appropriate drop type based on the number of properties given.
		switch (plaintext.Length) {
			case NumberOfDefiniteDropProperties:
				return DefiniteDrop.Parse(plaintext);
			case NumberOfRandomDropProperties:
				return RandomDrop.Parse(plaintext);
			default:
				throw new ArgumentException(WrongNumberOfPropertiesErrorMessage);
		}
	}
}