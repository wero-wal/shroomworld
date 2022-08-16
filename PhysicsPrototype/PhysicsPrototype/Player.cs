using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PhysicsPrototype
{
    public class Player
    {
        // - - - - - Properties - - - - -
        public Sprite Sprite { get => _sprite; }

        private Sprite _sprite;
        private Physics _physics;


        // - - - - - Constructors - - - - -
        public Player(Sprite sprite, float movementForce, float mass)
        {
            _sprite = sprite;
            _physics = new Physics(movementForce, mass, ref sprite);
        }


        // - - - - - Methods - - - - -
        // Movement
        public void CentreOnOrigin()
        {
            _sprite.Position -= new Vector2(_sprite.Texture.Width / 2, _sprite.Texture.Height / 2);
        }
        public void Move(Vector2 direction, float friction, float elapsedTimeInSeconds)
        {
            _physics.Move(direction, friction, elapsedTimeInSeconds);
        }


        private float Clamp(float number, float min, float max)
        {
            if (number < min)
            {
                number = min;
            }
            if (number > max)
            {
                number = max;
            }
            return number;
        }
    }
}
