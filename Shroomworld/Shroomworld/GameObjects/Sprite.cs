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
        // ----- Properties -----
        public Texture2D Texture => _texture;
        public Color Colour => _colour;
        public Vector2 Position => _position;
        public Vector2 Size => _size;
        public Vertices Vertices => _vertices;

        // ----- Fields -----
        private static Action<Texture2, Vector2, Color> _draw;

        protected Texture2D _texture;
        protected Color _colour;
        protected Vector2 _position;
        protected Vector2 _size;
        protected Vertices _vertices;

        // ----- Constructors -----
        public Sprite(Texture2D texture)
        {
            _texture = texture;
            _size = new Vector2(_texture.Width, _texture.Height);
        }

        // ----- Methods -----
        public static void InjectDependencies(Action<Texture2, Vector2, Color> drawFunction)
        {
            _draw = drawFunction;
        }

        public void UpdateVertices()
        {
            _vertices.Update(_position, _size);
        }
        public void Draw()
        {
            _draw.(_texture, _position, _colour);
        }
        public void Draw(Vector2 position)
        {
            _draw(_texture, position, _colour);
        }
        public void Draw(Color colour)
        {
            _draw(_texture, _position, colour);
        }
        public bool SetPosition(Vector2 position)
        {
            if ((position.X >= Shroomworld.TopLeftOfScreen.X) && (position.Y >= Shroomworld.TopLeftOfScreen.Y)
            && ((position.X + _size.X) <= Shroomworld.BottomRightOfScreen.X) && ((position.Y + _size.Y) <= Shroomworld.BottomRightOfScreen.Y))
            {
                _position = position;
                return true;
            }
            return false;
        }
    }
}
