namespace Shroomworld {
	/// <summary>
	/// Stores data about specific entities' physics as well as general physics.
	/// </summary>
	public class PhysicsData {

		// ----- Properties -----
        public static float Acceleration => s_acceleration;
        public static float Gravity => s_gravity;

		public float MaximumSpeed => _maximumSpeed;


		// ----- Fields -----
        private static readonly float s_acceleration;
        private static readonly float s_gravity;      

		private int _maximumSpeed;

		
		// ----- Constructors -----
		public PhysicsData(int maxSpeed) {
			_maximumSpeed = maxSpeed;
		}

		// ----- Methods -----
		public static void SetAcceleration(float acceleration) {
			s_acceleration = acceleration;
		}
		public static void SetGravity(float gravity) {
			s_gravity = gravity;
		}
	}
}
