using System.Collections.Generic;
using Shroomworld.Physics;

namespace Shroomworld {
    public class Enemy : Entity {

        // ----- Constructors -----
        public Enemy(int id, Sprite sprite, EntityHealthData healthData, Body body)
            : base(id, sprite, healthData, body) {
        }
    }
}
