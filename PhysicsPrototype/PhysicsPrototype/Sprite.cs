using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhysicsPrototype
{
    public class Sprite
    {
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public Vector2 Size { get => _textureSize; }
        public Color Colour { get => _colour; set => _colour = value; }
        public Vector2 Position { get => _position; set => _position = value; }


        private Texture2D _texture;
        private Vector2 _textureSize;
        private Color _colour;
        private Vector2 _position;


        public Sprite()
        {
        }
        public Sprite(Texture2D texture, Vector2 position, Color colour)
        {
            _texture = texture;
            _textureSize = new Vector2(texture.Width, texture.Height);
            _colour = colour;
            _position = position;
        }
    }
}
