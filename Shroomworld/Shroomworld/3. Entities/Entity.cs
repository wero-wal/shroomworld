using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public class Entity
    {
        // ---------- Enums ----------

        // ---------- Properties ----------
        public Texture2D Texture { get => _texture; }
        public Color Colour { get => _colour; }
        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 Size { get => _size; }

        // ---------- Fields ----------
        protected Texture2D _texture;
        protected Color _colour = Color.White;
        protected Vector2
            _position,
            _size;

        // ---------- Constructors ----------
        public Entity()
        {
            _texture = null;
            _position = Vector2.Zero;
            _size = Vector2.Zero;
        }
        public Entity(Texture2D texture)
        {
            _texture = texture;
            _position = Vector2.Zero;
            _size = new Vector2(_texture.Width, _texture.Height);
        }
        public Entity(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
            _size = new Vector2(_texture.Width, _texture.Height);
        }

        // ---------- Methods ----------
        protected Vector2[] GetVertices()
        {
            return new Vector2[]
            {
                _position, // Top Left
                _position + _size * Vector2.UnitX, // Top Right
                _position + _size * Vector2.UnitY, // Bottom Left
                _position + _size, // Bottom Right
            };
        }
    }
}
