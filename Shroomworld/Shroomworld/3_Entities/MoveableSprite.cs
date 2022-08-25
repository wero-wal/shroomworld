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
        // ---------- Enums ----------

        // ---------- Properties ----------
        public float MovementForce { get => _movementForce; }
        public float JumpForce { get => _jumpForce; }
        public float ConstantOfRestitution { get => _constantOfRestitution; }
        public Vector2 Velocity { get => _velocity; }

        // ---------- Fields ----------
        private readonly float _movementForce;
        private readonly float _jumpForce;
        private readonly float _constantOfRestitution;
        private readonly bool _clampToEdgeOfScreen;

        private Vector2 _velocity;

        // ---------- Constructors ----------
        public MoveableSprite(Texture2D texture, float movementForce, float jumpForce, float constantOfRestitution) : base(texture)
        {
            _movementForce = movementForce;
            _jumpForce = jumpForce;
            _constantOfRestitution = constantOfRestitution;
            _velocity = Vector2.Zero;
        }
        public MoveableSprite(Texture2D texture, Vector2 position, float movementForce, float jumpForce, float constantOfRestitution) : base(texture, position)
        {
            _movementForce = movementForce;
            _jumpForce = jumpForce;
            _constantOfRestitution = constantOfRestitution;
            _velocity = Vector2.Zero;
        }

        // ---------- Methods ----------
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
