using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Shroomworld
{
    internal class Menu
    {
        // ----- Fields -----
        private readonly Button[] _items;

        private readonly Color _buttonColour;
        private readonly Color _textColour;
        private readonly Color _highlightedButtonColour;
        private readonly Color _highlightedTextColour;

        private readonly Vector2 _topLeftOfMenu;
        private readonly Vector2 _buttonSize;

        private Action<string, Color, Color, Vector2, Vector2> _displayBox; // msg, bgCol, txtCol, startVector2, dimensions


        // ----- Constructors -----
        public Menu(Button[] items,
            Color buttonColour, Color textColour, Color? highlightedButtonColour, Color? highlightedTextColour,
            Vector2 topLeftOfMenu, Vector2 buttonSize)
        {
            _items = items;

            _buttonColour = buttonColour;
            _textColour = textColour;
            _highlightedButtonColour = highlightedButtonColour ?? textColour;
            _highlightedTextColour = highlightedTextColour  ?? buttonColour;

            _topLeftOfMenu = topLeftOfMenu;
            _buttonSize = buttonSize ?? topLeftOfMenu;
        }


        // ----- Methods -----
        public void DisplayMenu()
        {

        }
        public int UserSelectsButton()
        {
            // get input
            // check if cursor is in box
            // select that box
            while(true)
            {
                if(Shroomworld.GetCurrentMousePosition())
                {
                    // todo: divide mouse position in order to determine which button it's on
                    // todo: highlight if mouse is not down, select (return index of button) if it is
                }
            }
        }
    }
}
