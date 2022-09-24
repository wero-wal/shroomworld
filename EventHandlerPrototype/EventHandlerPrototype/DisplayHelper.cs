using System;
using System.Collections.Generic;
using System.Text;

namespace EventHandlerPrototype
{
    internal class DisplayHelper
    {
        private static (int left, int top) _cursorPos;
        private static ConsoleColor _fgColour;
        private static ConsoleColor _bgColour;


        public static void SaveCurrentCursorPosition()
        {
             _cursorPos = (Console.CursorLeft, Console.CursorTop);
        }
        public static void ResetCursorPosition()
        {
            Console.SetCursorPosition(_cursorPos.left, _cursorPos.top);
        }

        public static void SaveCurrentColours()
        {
            _fgColour = Console.ForegroundColor;
            _bgColour = Console.BackgroundColor;
        }
        public static void ResetColours()
        {
            Console.ForegroundColor = _fgColour;
            Console.BackgroundColor = _bgColour;
        }
    }
}
