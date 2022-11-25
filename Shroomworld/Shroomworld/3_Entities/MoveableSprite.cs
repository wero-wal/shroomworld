using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public class MoveableSprite : Sprite
    {
        // ----- Enums -----

        // ----- Properties -----
        public float MovementForce { get => _movementForce; }
        public float JumpForce { get => _jumpForce; }
        public float ConstantOfRestitution { get => _constantOfRestitution; }
        public Vector2 Velocity { get => _velocity; }

        // ----- Fields -----
        private const int _jumpMultiplier = 10;

        private readonly float _movementForce;
        private readonly float _jumpForce;
        private readonly float _constantOfRestitution;
        private readonly bool _clampToEdgeOfScreen;

        private Vector2 _velocity;

        // ----- Constructors -----
        public MoveableSprite(Texture2D texture, float movementForce, float constantOfRestitution) : base(texture)
        {
            _movementForce = movementForce;
            _jumpForce = movementForce * _jumpMultiplier;
            _constantOfRestitution = constantOfRestitution;
            _velocity = Vector2.Zero;
        }
        private MoveableSprite(Texture2D texture, Vector2 position, float movementForce, float constantOfRestitution) : base(texture, position)
        {
            _movementForce = movementForce;
            _jumpForce = movementForce * _jumpMultiplier;
            _constantOfRestitution = constantOfRestitution;
            _velocity = Vector2.Zero;
        }

        // ----- Methods -----
        public static MoveableSprite CreateNew(Texture2D texture, Vector2 position, float movementForce, float constantOfRestitution)
        {
            return new MoveableSprite(texture, position, movementForce, constantOfRestitution);
        }

        public override void SetPosition(float x, float y)
        {
            if (_clampToEdgeOfScreen)
            {
                _position = MyGame.ClampToScreen(x, y);
            }
            else
            {
                _position = new Vector2(x, y);
            }
        }
        public override void SetPosition(Vector2 vector)
        {
            if (_clampToEdgeOfScreen)
            {
                _position = MyGame.ClampToScreen(vector);
            }
            else
            {
                _position = vector;
            }
        }

        public void SetVelocity(float x, float y)
        {
            _velocity = new Vector2(x, y);
        }
        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }
    }
}
