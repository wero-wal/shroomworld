namespace Shroomworld {
	/// <summary>
	/// Stores data about specific entities' physics.
	/// </summary>
	public class PhysicsData {

		// ----- Properties -----
		public float MaximumSpeed => _maximumSpeed;

		// ----- Fields -----
		private int _maximumSpeed;
		
		// ----- Constructors -----
		public PhysicsData(int maxSpeed) {
			_maximumSpeed = maxSpeed;
		}
	}
}
