using Shroomworld.Physics;
namespace Shroomworld {
    public abstract class Entity {

        // ----- Properties -----
        public int Id => _id;
        public Sprite Sprite => _sprite;
        public EntityHealthData HealthData => _healthData;
        public Body Body => _body;

        // ----- Fields -----
        protected readonly int _id;
        protected readonly Sprite _sprite;
        protected readonly EntityHealthData _healthData;
        protected readonly Body _body;

        // ----- Constructors -----
        protected Entity(int id, Sprite sprite, EntityHealthData healthData, Body body) {
            _id = id;
            _sprite = sprite;
            _healthData = healthData;
            _body = body;
        }

        // ----- Methods -----
        public virtual void Update() {
            _sprite.Update();
        }
    }
}
