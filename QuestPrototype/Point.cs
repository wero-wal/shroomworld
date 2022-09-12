using System;

namespace QuestPrototype
{
    internal class Point
    {
        //----- Enums -----
        //----- Properties -----
        //----- Fields -----
        public int X;
        public int Y;

        //----- Constructors -----
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point()
        {
            X = 0;
            Y = 0;
        }

        //----- Methods -----
        internal void SetCursorToPoint()
        {
            Console.SetCursorPosition(X, Y);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
        public static Point operator -(Point point)
        {
            return new Point(0 - point.X, 0 - point.Y);
        }
        public static Point operator ==(Point a, Point b)
        {
            return (a.X == b.X) && (a.Y == b.Y);
        }
        public static Point operator !=(Point a, Point b)
        {
            return (a.X != b.X) || (a.Y != b.Y);
        }
    }
}
