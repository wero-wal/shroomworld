using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class Entity
    {
        //---Enums---
        //---Constants---
        //---Accessors---
        //---Variables---
        protected readonly Texture Texture;
        protected readonly Point
            Gravity,
            Jump,
            Move;
        protected readonly int
            Mass,
            ConstantOfRestitution;

        protected int
            MaxSpeed, // can be altered by power-ups
            Weight; // can be altered by gravity (e.g. in different dimensions)
        protected Point
            Position,
            Velocity,
            Acceleration,
            Forces;

        protected int Health;

        //---Constructors---
        //---Methods---
        private void Apply_Jump_Force()
        {
            Forces += Jump;
        }
        private void Apply_Movement_Force(int direction) // -1 or 1
        {
            Forces.X += Move.X * direction;
        }
        private void Apply_Gravity()
        {
            Forces += Gravity;
        }
        private void Apply_Normal_Force(bool touchingGround) // or do all the checks on the o u t s i d e
        {
            if (touchingGround)
            {
                Forces.Y = 0;
            }
            else
            {
                Forces.Y = Mass * Gravity.Y;
            }
            // maybe do this for the sides as well?
        }
        private void Update_Acceleration()
        {
            // a = F / m
            Acceleration = Forces / Mass;
        }
        private void Update_Velocity()
        {
            Velocity += Acceleration; // each second. How do?
        }
        private void Update_Position(Point closestThing, Point.Direction direction)
        {
            if ((direction == Point.Direction.Left) && (closestThing.X > (Position + Velocity).X)
                || (direction == Point.Direction.Right) && (closestThing.X < (Position + Velocity).X)
                || (direction == Point.Direction.Up) && (closestThing.Y > (Position + Velocity).Y)
                || (direction == Point.Direction.Down) && (closestThing.Y < (Position + Velocity).Y))
            {
                Do_A_Collision();
            }
            Position += Velocity;
        }
        private void Do_A_Collision()
        {
            // e = (v2 - v1) / (u1 - u2)
            // since v2 and u2 = 0 because tiles can't move,
            // e = -v1 / u1
            // e(u1) = -v1
            // => v1 = -e(u1)
            Velocity *= -ConstantOfRestitution;
        }
    }
}
