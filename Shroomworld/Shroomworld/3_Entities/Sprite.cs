using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public class Sprite
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
        public Texture2D Texture { get => _texture; }
        public Color Colour { get => _colour; }
        public Vector2 Position { get => _position; }
        public Vector2 Size { get => _size; }

        // ---------- Fields ----------
        protected Texture2D _texture;
        protected Color _colour;
        protected Vector2 _position;
        protected Vector2 _size;

        // ---------- Constructors ----------
        public Sprite(Texture2D texture)
        {
            _texture = texture;
            _position = Vector2.Zero;
            _size = new Vector2(_texture.Width, _texture.Height);
        }
        protected Sprite(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
            _size = new Vector2(_texture.Width, _texture.Height);
        }

        // ---------- Methods ----------
        public static Sprite CreateNew(Texture2D texture, Vector2 position)
        {
            return new Sprite(texture, position);
        }

        public Vector2[] GetVertices()
        {
            return new Vector2[]
            {
                _position, // Top Left
                _position + _size * Vector2.UnitX, // Top Right
                _position + _size * Vector2.UnitY, // Bottom Left
                _position + _size, // Bottom Right
            };
        }
        public virtual void SetPosition(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }
        public virtual void SetPosition(Vector2 vector)
        {
            _position = vector;
        }
    }
}
