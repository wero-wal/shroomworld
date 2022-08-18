using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public class MoveableEntity : Entity
        // Players
        // Enemies
        // NPCs
    {
        // ---------- Enums ----------
        public enum Vertices
        {
            TopLeft = 0,
            TopRight = 1,
            BottomLeft = 2,
            BottomRight = 3,
        }

        // ---------- Properties ----------

        // ---------- Fields ----------
        protected readonly int _id;
        protected readonly float
            _mass,
            _maxHealth;
        protected readonly Vector2 _weight;

        protected float _health;
        protected Vector2 _velocity;

        // ---------- Constructors ----------
        public MoveableEntity()
        {
            _mass = 0f;
            _weight = Vector2.Zero;
            _sprite = new Entity();
            _velocity = Vector2.Zero;
        }
        public MoveableEntity(Entity sprite)
        {
            _mass = 0f;
            _weight = _mass * MyGame.Gravity;
            _sprite = sprite;
            _velocity = Vector2.Zero;
        }

        // ---------- Methods ----------
        public void Move()
        {

        }
        public bool HasCollidedWith(Entity entity)
        {
            return false;
        }
    }
}
