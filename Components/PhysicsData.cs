namespace Shroomworld {
	/// <summary>
	/// Stores data about specific entities' physics.
	/// </summary>
	public class PhysicsData {

		// ----- Properties -----
		public float MaximumSpeed => _maximumSpeed;
		public float Range => _range;

		// ----- Fields -----
		private readonly float _maximumSpeed;
		private readonly float _range;
		
		// ----- Constructors -----
		public PhysicsData(float maxSpeed, float range) {
			_maximumSpeed = maxSpeed;
			_range = range;
		}
	}
}
