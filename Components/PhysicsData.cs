namespace Shroomworld {
	/// <summary>
	/// Stores data about specific entities' physics.
	/// </summary>
	public class PhysicsData {

		// ----- Properties -----
		public float MaximumSpeed => _maximumSpeed;

		// ----- Fields -----
		private float _maximumSpeed;
		
		// ----- Constructors -----
		public PhysicsData(float maxSpeed) {
			_maximumSpeed = maxSpeed;
		}
	}
}
