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
        private const float _distanceBetweenEachMenuItem = 10;

        private readonly Button[] _items;

        private readonly Color _buttonColour;
        private readonly Color _textColour;
        private readonly Color _highlightedButtonColour;
        private readonly Color _highlightedTextColour;

        private readonly Vector2 _startPosition; // top left
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

            _startPosition = topLeftOfMenu;
            _buttonSize = buttonSize;
            
            SetButtonPositions();
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

        private void SetButtonPositions()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].Sprite.SetPosition(_startPosition + (_buttonSize * i) + (Vector2.UnitY * i * _distanceBetweenEachMenuItem));
            }
        }
    }
}
