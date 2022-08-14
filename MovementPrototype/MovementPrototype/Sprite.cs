using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MovementPrototype
{
    public class Sprite
    {
        public Texture2D Texture { get => _texture; set => _texture = value; }
        public Color Color { get => _colour; set => _colour = value; }
        public Vector2 Position { get => _position; set => _position = value; }


        private Texture2D _texture;
        private Color _colour;
        private Vector2 _position;
        private float _speed;


        public Sprite()
        {
        }
        public Sprite(Texture2D texture, Vector2 position, float speed, Color? colour = null)
        {
            _texture = texture;
            _colour = (colour == null) ? Color.White : Color;
            _position = position;
            _speed = speed;
        }


        public void MoveLeft()
        {
            if ((_position.X - _speed) > 0)
            {
                _position.X -= _speed;
            }
        }
        public void MoveRight()
        {
            if ((_position.X + _speed + _texture.Width) < MyGame.BufferWidth)
            {
                _position.X += _speed;
            }
        }
        public void MoveUp()
        {
            if ((_position.Y - _speed) > 0)
            {
                _position.Y -= _speed;
            }
        }
        public void MoveDown()
        {
            if ((_position.Y + _speed + _texture.Height) < MyGame.BufferHeight)
            {
                _position.Y += _speed;
            }
        }
        public void Centre()
        {
            _position.X -= _texture.Width / 2;
            _position.Y -= _texture.Height / 2;
        }
    }
}
