using Shroomworld.Physics;

namespace Shroomworld {
    public class Enemy : Entity {

        // ----- Constructors -----
        public Enemy(EnemyType type, Sprite sprite, EntityHealthData healthData)
            : base(type, sprite, healthData, new Body(sprite, type.PhysicsData)) {
        }
    }
}
