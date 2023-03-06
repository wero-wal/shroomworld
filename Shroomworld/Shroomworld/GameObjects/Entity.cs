using Shroomworld.Physics;
namespace Shroomworld {
    public abstract class Entity {

        // ----- Properties -----
        public IType Type => _type;
        public Sprite Sprite => _sprite;
        public EntityHealthData HealthData => _healthData;
        public Body Body => _body;

        // ----- Fields -----
        protected readonly IType _type;
        protected readonly Sprite _sprite;
        protected readonly EntityHealthData _healthData;
        protected readonly Body _body;

        // ----- Constructors -----
        protected Entity(IType type, Sprite sprite, EntityHealthData healthData, Body body) {
            _type = type;
            _sprite = sprite;
            _healthData = healthData;
            _body = body;
        }
    }
}
