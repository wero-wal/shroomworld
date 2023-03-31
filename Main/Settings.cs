namespace Shroomworld;
public class Settings {

	// ----- Fields -----
	public readonly float Gravity;
	public readonly float Acceleration;
	public readonly int RegenSpeedPercent;
	public readonly int TileSize;
	
	// ----- Constructors -----
	public Settings(float gravity, float acceleration, int regenSpeedPercent, int tileSize) {
		Gravity = gravity;
		Acceleration = acceleration;
		RegenSpeedPercent = regenSpeedPercent;
		TileSize = tileSize;
	}
}