using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class Point
    {
        public enum Direction
        {
            Up, Down, Left, Right
        }

        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // operators
        public static Point operator +(Point a, Point b)
        {
            return new Point(b.X + a.X, a.Y + b.Y);
        }
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
        public static Point operator /(Point a, int b)
        {
            return new Point(a.X / b, a.Y / b);
        }
        public static Point operator *(Point a, int b)
        {
            return new Point(a.X * b, a.Y * b);
        }

        //---Methods---
        public int Dist_To(Point a)
        {
            Point dist = new Point(Math.Abs(a.X - X), Math.Abs(a.Y - Y));
            return (int)Math.Sqrt(Math.Pow(dist.X, 2) + Math.Pow(dist.Y, 2));
        }
        public int Mod()
        {
            return (int)Math.Sqrt(X * X + Y * Y);
        }
    }
}
