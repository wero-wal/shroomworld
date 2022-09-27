using System;

namespace QuestPrototype
{
    internal static class Settings
    {
        public int LeftBound = 0;
        public int TopBound = 0;
        public int RightBound => Console.WindowWidth - 1;
        public int BottomBound => Console.WindowHeight - 1;
        public Point CentreOfScreen => new Point((BottomBound - TopBound) / 2, (RightBound - LeftBound) / 2);

        public ConsoleColor BackgroundColour;
        public ConsoleColor TextColour;
        public ConsoleColor MenuBackgroundColour;
        public ConsoleColor MenuTextColour;

        public int InteractionRange = 5;
    }
}
